using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Antares.model;
using WebAntares;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using System.Data.Common;
using System.Globalization;

public partial class Solicitudes_Obras : System.Web.UI.Page
{
    protected override void OnInitComplete(EventArgs e)
    {
        ucAdjuntos.sol = BiFactory.Sol;
        ucSolicitudGastos.Sol = BiFactory.Sol;
        base.OnInitComplete(e);
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            FillSolicitudEmpleados(0);
            FillSolicitudVehiculos(0);
            LoadDataComplementaria();
            CargaFechas();
        }
    }

    public void CargaFechas()
    {
        txtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }
    
    public void CargarCombos()
    {
        txtInicio.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtEntrega.Text = AntaresHelper.UltimoDiaSemana(DateTime.Today).ToString("dd/MM/yyyy");
   
        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.GetVehiculosActivos())
        {
            lstVehiculos.Items.Add(new ListItem(vehi.NUnidad + "-" + vehi.Marca + " " + vehi.Modelo + " - " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }
                
        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbCliente.Items.Add(new ListItem(emp.Nombre , emp.IdEmpresa.ToString()));
        }

        cmbResponsable.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal responsable in Antares.model.Personal.GetPersonalActivo())
        {
            cmbResponsable.Items.Add(new ListItem(responsable.Apellido + ", " + responsable.Nombres, responsable.IdEmpleados.ToString()));
        }
    }

    private void LoadDataComplementaria()
    {

        SolicitudObra obr = SolicitudObra.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        if (obr != null)
        {
            txtNro.Text = obr.NroObra;
            txtContacto.Text = BiFactory.Sol.Contacto;
            txtOrdenCompra.Text = BiFactory.Sol.NroOrdenCte;
            cmbCliente.SelectedValue = BiFactory.Sol.IdCliente.ToString();
            txtMail.Text = BiFactory.Sol.ContactoMail;
            txtTelefonoContacto.Text = BiFactory.Sol.ContactoTel;
            txtDescripcionTareas.Text = obr.DescripcionTareas;
            
            txtInicio.Text = obr.FechaInicio.ToString("dd/MM/yyyy");
            txtEntrega.Text = obr.FechaFin.ToString("dd/MM/yyyy");
            txtInicio.Enabled = false;
            txtEntrega.Enabled = false;
            imgInicio.Visible = false;

            txtRequisitosAprovacion.Text = obr.RequisitosAprovacion;
            txtRequisitoIngreso.Text = obr.RequisitosIngreso;

            ucSolicitudGastos.Deshabilita_Gastos();
            
        }
    }

    protected void btnAsignaEmpleadoSolicitud_Click(object sender, EventArgs e)
    {
        int id_sol = BiFactory.Sol.Id_Solicitud;
        int id_empleado;

        if (cmbResponsable.SelectedIndex > 0)
        {
            id_empleado = int.Parse(cmbResponsable.SelectedValue);
            
            if (!SolicitudRecursosEmpleados.ExisteEmpleadoEnSolicitud(id_sol, id_empleado))
            {
                SolicitudRecursosEmpleados.QuitaResponsableSolicitud(id_sol);
                SolicitudRecursosEmpleados t = new SolicitudRecursosEmpleados();

                t.IdEmpleado = id_empleado;
                t.IdSolicitud = id_sol;
                t.Responsable = true;
                t.Save();
            }
        }

        for (int i = 0; i < lstEmpleadosSolicitud.Items.Count; i++)
        {
            if (lstEmpleadosSolicitud.Items[i].Selected)
            {
                id_empleado = Int32.Parse(lstEmpleadosSolicitud.Items[i].Value);

                if (!SolicitudRecursosEmpleados.ExisteEmpleadoEnSolicitud(id_sol, id_empleado))
                {
                    SolicitudRecursosEmpleados t = new SolicitudRecursosEmpleados();

                    t.IdEmpleado = id_empleado;
                    t.IdSolicitud = id_sol;
                    t.Responsable = false;
                    t.Save();
                }
            }
        }
        FillSolicitudEmpleados(0);
    }

    protected void btnAsignaVehiculoSolicitud_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int id_Sol = BiFactory.Sol.Id_Solicitud;
            int id_Vehiculo;

            for (int i = 0; i < lstVehiculos.Items.Count; i++)
            {
                if (lstVehiculos.Items[i].Selected)
                {
                    id_Vehiculo = int.Parse(lstVehiculos.Items[i].Value);

                    if (!SolicitudRecursosVehiculos.ExisteVehiculoEnSolicitud(id_Sol, id_Vehiculo))
                    {
                        SolicitudRecursosVehiculos t = new SolicitudRecursosVehiculos();
                        t.IdVehiculo = int.Parse(lstVehiculos.Items[i].Value.ToString());
                        t.IdSolicitud = BiFactory.Sol.Id_Solicitud;
                        t.Fecha = DateTime.Parse("01/01/1900");
                        
                        t.Save();
                    }
                }
            }
            FillSolicitudVehiculos(0);
        }
    }

    private void FillSolicitudEmpleados(int pageIndex)
    {

        DbDataReader reader = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
        DataTable table = new DataTable();
        table.Load(reader);

        gvSolicitudPersonas.DataSource = table;
        gvSolicitudPersonas.PageIndex = pageIndex;
        gvSolicitudPersonas.DataKeyNames = new string[] { "Id" };
        gvSolicitudPersonas.DataBind();


    }

    private void FillSolicitudVehiculos(int pageIndex)
    {
        DbDataReader reader = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
        DataTable table = new DataTable();
        table.Load(reader);

        gvSolicitudVehiculos.DataSource = table;
        gvSolicitudVehiculos.PageIndex = pageIndex;
        gvSolicitudVehiculos.DataKeyNames = new string[] { "Id" };
        gvSolicitudVehiculos.DataBind();
    }
    
    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        if (IsValid && EsSolicitudValida())
        {
            sol.IdCliente = int.Parse(cmbCliente.SelectedValue);
            sol.Contacto = txtContacto.Text;
            sol.NroOrdenCte = txtOrdenCompra.Text;
            sol.Status = eEstados.Pendiente.ToString();
            sol.ContactoMail = txtMail.Text;
            sol.ContactoTel = txtTelefonoContacto.Text;

            SolicitudObra Sol_Ob = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));

            if (Sol_Ob == null)
            {
                Sol_Ob = new SolicitudObra();
                Sol_Ob.IdSolicitud = sol.Id_Solicitud;
            }
            Sol_Ob.Presupuesto = "0";
            Sol_Ob.NroObra = txtNro.Text;
            Sol_Ob.IdSolicitud = sol.Id_Solicitud;
            Sol_Ob.DescripcionTareas = txtDescripcionTareas.Text;
            Sol_Ob.FechaInicio = DateTime.Parse(txtInicio.Text);
            Sol_Ob.FechaFin = DateTime.Parse(txtEntrega.Text);
            Sol_Ob.RequisitosAprovacion = txtRequisitosAprovacion.Text;
            Sol_Ob.RequisitosIngreso = txtRequisitoIngreso.Text;
            Sol_Ob.Presupuesto = "0";
           
            sol.Save();
            Sol_Ob.Save();

            pnlObras.Visible = false;

            ucObras.Numero = Sol_Ob.IdSolicitud.ToString();
            ucObras.Titulo = sol.Descripcion;
            ucObras.Estado = sol.Status;
            ucObras.CodigoObra = Sol_Ob.NroObra;
            ucObras.Cliente = cmbCliente.SelectedItem.Text;
            ucObras.NroOrden = sol.NroOrdenCte;
            ucObras.Contacto = sol.Contacto;
            ucObras.MailContacto = sol.ContactoMail;
            ucObras.TelefonoContacto = sol.ContactoTel;
            ucObras.DescripcionTareas = Sol_Ob.DescripcionTareas;
            ucObras.FechaInicio = Sol_Ob.FechaInicio.ToString("dd/MM/yyyy");
            ucObras.FechaEntrega = Sol_Ob.FechaFin.ToString("dd/MM/yyyy");
            ucObras.RequisitosAprobacion = Sol_Ob.RequisitosAprovacion;
            ucObras.RequisitosIngreso = Sol_Ob.RequisitosIngreso;
            ucObras.Personal = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
            ucObras.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);

            decimal gastos = Solicitud.Valida_Gastos_Ingresados_Solicitud(BiFactory.Sol.Id_Solicitud);
            ucObras.Monto = gastos.ToString();

            ucObras.Adjuntos = sol.GetAdjuntos();
            ucObras.Visible = true;
            WebAntares.AntaresHelper.NotificaSolicitud(sol.Id_Solicitud);
        }


        //{
        //    sol.IdCliente = int.Parse(cmbCliente.SelectedValue);
        //    sol.Contacto = txtContacto.Text;
        //    sol.NroOrdenCte = txtOrdenCompra.Text;
        //    sol.Status = eEstados.Pendiente.ToString();
        //    sol.ContactoMail = txtMail.Text;
        //    sol.ContactoTel = txtTelefonoContacto.Text;
         
        //if (IsValid && EsSolicitudValida() && !Solicitud.ExisteObra(sol.Id_Solicitud.ToString()))
        //{
        //    sol.IdCliente = int.Parse(cmbCliente.SelectedValue);
        //    sol.Contacto = txtContacto.Text;
        //    sol.NroOrdenCte = txtOrdenCompra.Text;
        //    sol.Status = eEstados.Pendiente.ToString();
        //    sol.ContactoMail = txtMail.Text;
        //    sol.ContactoTel = txtTelefonoContacto.Text;
            
        //    SolicitudObra Sol_Ob = new SolicitudObra();
        //    Sol_Ob.IdSolicitud = sol.Id_Solicitud;
        //    Sol_Ob.DescripcionTareas  = txtDescripcionTareas.Text;
        //    Sol_Ob.FechaInicio = DateTime.Parse(txtInicio.Text);
        //    Sol_Ob.FechaFin = DateTime.Parse(txtEntrega.Text);
        //    Sol_Ob.RequisitosAprovacion = txtRequisitosAprovacion.Text;
        //    Sol_Ob.RequisitosIngreso = txtRequisitoIngreso.Text;

        //    Sol_Ob.Presupuesto = "0";
            
        //    sol.Save();
        //    Sol_Ob.Save();

        //    pnlObras.Visible = false;

        //    ucObras.Numero = Sol_Ob.IdSolicitud.ToString();
        //    ucObras.Titulo = sol.Descripcion;
        //    ucObras.Estado = sol.Status;
        //    ucObras.Cliente = cmbCliente.SelectedItem.Text;
        //    ucObras.NroOrden = sol.NroOrdenCte;
        //    ucObras.Contacto = sol.Contacto;
        //    ucObras.MailContacto = sol.ContactoMail;
        //    ucObras.TelefonoContacto = sol.ContactoTel;
        //    ucObras.DescripcionTareas = Sol_Ob.DescripcionTareas;
        //    ucObras.FechaInicio = Sol_Ob.FechaInicio.ToString("dd/MM/yyyy");
        //    ucObras.FechaEntrega = Sol_Ob.FechaFin.ToString("dd/MM/yyyy");
        //    ucObras.RequisitosAprobacion = Sol_Ob.RequisitosAprovacion;
        //    ucObras.RequisitosIngreso = Sol_Ob.RequisitosIngreso;
        //    ucObras.Personal = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
        //    ucObras.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);

        //    decimal gastos = Solicitud.Valida_Gastos_Ingresados_Solicitud(BiFactory.Sol.Id_Solicitud);
        //    ucObras.Monto = gastos.ToString();

        //    ucObras.Adjuntos = sol.GetAdjuntos();
        //    ucObras.Visible = true;
        //    WebAntares.AntaresHelper.NotificaSolicitud(sol.Id_Solicitud);
        //}
    }

    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos v = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        v.Delete();
        FillSolicitudVehiculos(0);
    }

    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.RowIndex].Value.ToString())));
        p.Delete();
        FillSolicitudEmpleados(0);
    }

    protected void cmbResponsable_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idResponsable = int.Parse(cmbResponsable.SelectedValue);
        if (idResponsable == -1)
        {
            litPersonal.Visible = false;
            lstEmpleadosSolicitud.Visible = false;
            btnAsignaEmpleadoSolicitud.Visible = false;
        }
        else
        {
            lstEmpleadosSolicitud.Items.Clear();
            CargaListaEmpleados(idResponsable);
            litPersonal.Visible = true;
            lstEmpleadosSolicitud.Visible = true;
            btnAsignaEmpleadoSolicitud.Visible = true;
        }
    }

    protected void cvVehiculos_ServerValidate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = lstVehiculos.SelectedIndex >= 0;
    }

    protected void CargaListaEmpleados(int IdDelResponsable)
    {
        foreach (Antares.model.Personal persona in Antares.model.Personal.FindAll())
        {
            if (persona.IdEmpleados != IdDelResponsable)
            {
                lstEmpleadosSolicitud.Items.Add(new ListItem(persona.Apellido + ", " + persona.Nombres, persona.IdEmpleados.ToString()));
            }
        }
    }
        
    protected bool EsSolicitudValida()
    {
        bool esValida = true;
        List<string> errores = new List<string>();
        string idSol = BiFactory.Sol.Id_Solicitud.ToString();
        if (!Solicitud.TieneResponsable(idSol))
        {
            esValida = false;
            errores.Add("Debe asignar al menos un responsable.");
        }
       
        if (!esValida)
        {
            blErrores.DataSource = errores;
            blErrores.DataBind();
        }
        return esValida;
    }

    protected void gvSolicitudPersonas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillSolicitudEmpleados(e.NewPageIndex);

    }

    protected void gvSolicitudVehiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillSolicitudVehiculos(e.NewPageIndex);

    }

    protected void cvGastosEnSolicitud_ServerValidate(object source, ServerValidateEventArgs args)
    {

        decimal gastos = Solicitud.Valida_Gastos_Ingresados_Solicitud(BiFactory.Sol.Id_Solicitud);
        args.IsValid = false;
        if (gastos == 0)
        {
            args.IsValid = false;

            cvGastosEnSolicitud.ErrorMessage = "No se ha cargado ningun Gasto a la Solicitud";


        }
        else { args.IsValid = true; }


    }

    protected void txtInicio_TextChanged(object sender, EventArgs e)
    {
        txtEntrega.Text = AntaresHelper.UltimoDiaSemana(DateTime.Parse(txtInicio.Text)).ToString("dd/MM/yyyy");


    }

}
