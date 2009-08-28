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

public partial class Solicitudes_MantPreventivoRendicion : System.Web.UI.Page
{
    protected override void OnInitComplete(EventArgs e)
    {
        ucAdjuntos.sol = BiFactory.Sol;
        base.OnInitComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            FillTareas();
            FillSolicitudEmpleados();
            FillSolicitudVehiculos();
            FillDatosClientes();
        }
    }

    protected void gvSolicitudVehiculos_rowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos v = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        v.Delete();
        FillSolicitudVehiculos();
    }

    private void FillDatosClientes()
    {
        Solicitud sol = BiFactory.Sol;
        SolicitudPreventivo Sp = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        cmbClientes.SelectedValue = BiFactory.Sol.IdCliente.ToString();
        txtContactoCliente.Text = BiFactory.Sol.Contacto;
        txtNroOrdenCliente.Text = BiFactory.Sol.NroOrdenCte;
        txtMailContacto.Text = BiFactory.Sol.ContactoMail;
        txtTelefonoContacto.Text = BiFactory.Sol.ContactoTel;
        txtPresupuesto.Text = Sp.Presupuesto;

        if (Sp!= null)
        {
            Sitios unSitio = Sitios.FindOne(Expression.Eq("IdSitio", Sp.IdSitio));
            hdnSitio.Value = unSitio.IdSitio.ToString();
            litSitio.Text = unSitio.Descripcion;
            ucAdjuntos.ListaAdjuntos(sol.Id_Solicitud.ToString());
        }
    }

    protected void btnAgregarTarea_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int id_Sol = BiFactory.Sol.Id_Solicitud;
            int id_Tarea;

            for (int i = 0; i < lstTareas.Items.Count; i++)
            {
                if (lstTareas.Items[i].Selected)
                {
                    id_Tarea = int.Parse(lstTareas.Items[i].Value);

                    if (!SolicitudTareas.ExisteTareaEnSolicitud(id_Sol, id_Tarea))
                    {
                        SolicitudTareas t = new SolicitudTareas();
                        t.IdTarea = id_Tarea;
                        t.FechaInicio = DateTime.Parse(txtDesde.Text);
                        t.FechaFin = DateTime.Parse(txtHasta.Text);
                        t.IdSolicitud = id_Sol;
                        t.Save();
                    }
                }
            }
            FillTareas();
        }
    }

    
    private void FillTareas()
    {
        gvTareas.DataSource = SolicitudTareas.GetReader(BiFactory.Sol.Id_Solicitud);
        gvTareas.DataKeyNames = new string[] { "Id" };
        gvTareas.DataBind();
    }

    public void CargaComboPersonal()
    {
        string sql;
        cmbResponsable.Items.Clear();
        cmbResponsable.Items.Add(new ListItem("Seleccione...", "-1"));
        sql = " Id_Empleados not in (select Id_Empleado from dbo.Solicitud_Recursos_Empleados where id_solicitud = " + BiFactory.Sol.Id_Solicitud.ToString() + ")";

        foreach (Antares.model.Personal p in Antares.model.Personal.FindAll(Expression.Sql(sql)))
        {
            cmbResponsable.Items.Add(new ListItem(p.Apellido + ", " + p.Nombres, p.IdEmpleados.ToString()));
        }
    }

    public void CargaComboVehiculos()
    {
        string sql;
        lstVehiculos.Items.Clear();
        sql = " Id_Vehiculos not in (select Id_Vehiculo from dbo.Solicitud_Recursos_Vehiculos where id_solicitud = " + BiFactory.Sol.Id_Solicitud.ToString() + ")";

        foreach (Antares.model.Vehiculos v in Antares.model.Vehiculos.FindAll(Expression.Sql(sql)))
        {
            lstVehiculos.Items.Add(new ListItem(v.Marca + " " + v.Modelo + " " + v.Patente, v.IdVehiculos.ToString()));
        }
    }
        
    public void CargarCombos()
    {
        CargaComboPersonal();
        CargaComboVehiculos();

        foreach (Antares.model.Tareas t in Antares.model.Tareas.FindAll())
        {
            lstTareas.Items.Add(new ListItem(t.Tarea, t.Id.ToString()));
        }
        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre + " (" + emp.Localidad + ")", emp.IdEmpresa.ToString()));
        }
    }

    protected void gvTareas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudTareas t;
        t = SolicitudTareas.FindFirst(Expression.Eq("Id", int.Parse(gvTareas.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        t = null;

        t = SolicitudTareas.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
                
        FillTareas();
    }
        
    protected void btnAsignaEmpleadoSolicitud_Click(object sender, EventArgs e)
    {
        int id_sol = BiFactory.Sol.Id_Solicitud;
        int id_empleado;

        if (cmbResponsable.SelectedIndex >= 0)
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
        FillSolicitudEmpleados();
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
                        t.Save();
                    }
                }
            }
            FillSolicitudVehiculos();
        }
    }
   
    private void FillSolicitudEmpleados()
    {
        gvSolicitudPersonas.DataSource = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
        gvSolicitudPersonas.DataKeyNames = new string[] { "Id" };
        gvSolicitudPersonas.DataBind();
    }

    private void FillSolicitudVehiculos()
    {
        gvSolicitudVehiculos.DataSource = SolicitudRecursosVehiculos.GetVehiculosEnSolicitud(BiFactory.Sol.Id_Solicitud);
        gvSolicitudVehiculos.DataKeyNames = new string[] { "Id" };
        gvSolicitudVehiculos.DataBind();
    }

    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        if (IsValid && EsSolicitudValida())
        {
            //Solicitud Sol_Original = Solicitud.FindFirst(Expression.Eq("IdSolicitudInicial",BiFactory.Sol.Id_Solicitud));
            Solicitud Sol_Original = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            Sol_Original.Status = eEstados.Realizado.ToString();
            Sol_Original.Update();

            //Solicitud Reporte = Solicitud.GetById(BiFactory.Sol.IdSolicitudInicial);
            Solicitud Reporte = Solicitud.FindFirst(Expression.Eq("IdSolicitudInicial", Sol_Original.Id_Solicitud));
            SolicitudPreventivo preventivo = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", Reporte.Id_Solicitud));

            SolicitudPreventivo r = new SolicitudPreventivo();
            r.IdSolicitud = preventivo.IdSolicitud;
            r.Presupuesto = txtPresupuesto.Text; ;
            r.FechaInicio = txtDesde.Text;
            r.FechaFin = txtHasta.Text;
            r.IdSitio = int.Parse(hdnSitio.Value);
            //r.fechaactualizacion = DateTime.Now;

            //Agregar la fecha de solicitud a la solicitud del tipo preventivo, asi tambien a correctivo
            Reporte.Status = eEstados.Realizado.ToString();
            r.Save();

        }
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

    protected void CargaListaEmpleados(int IdDelResponsable)
    {
        //foreach (Antares.model.Personal persona in Antares.model.Personal.FindAll(Expression.Sql(" Id_Empleados <> " + IdDelResponsable.ToString())))
        foreach (Antares.model.Personal persona in Antares.model.Personal.FindAll())
        {
            if (persona.IdEmpleados != IdDelResponsable)
            {
                lstEmpleadosSolicitud.Items.Add(new ListItem(persona.Apellido + ", " + persona.Nombres, persona.IdEmpleados.ToString()));
            }
        }
    }

    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos t = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        FillSolicitudVehiculos();
    }
     
    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.RowIndex].Value.ToString())));
        p.Delete();
        FillSolicitudEmpleados();

    }

    protected bool EsSolicitudValida()
    {
        bool esValida = true;
        List<string> errores = new List<string>();
        string idSol = BiFactory.Sol.Id_Solicitud.ToString();
        if (gvTareas.Rows.Count == 0)
        {
            esValida = false;
            errores.Add("Debe seleccionar al menos una tarea.");
        }
        if (!Solicitud.TieneResponsable(idSol))
        {
            esValida = false;
            errores.Add("Debe asignar al menos un responsable.");
        }
        if (gvSolicitudVehiculos.Rows.Count == 0)
        {
            esValida = false;
            errores.Add("Debe asignar al menos un vehículo.");
        }
        if (!esValida)
        {
            blErrores.DataSource = errores;
            blErrores.DataBind();
        }
        return esValida;
    }

    protected void cvTareas_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = lstTareas.SelectedIndex >= 0;
    }

    protected void cvVehiculos_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = lstVehiculos.SelectedIndex >= 0;
    }
}
