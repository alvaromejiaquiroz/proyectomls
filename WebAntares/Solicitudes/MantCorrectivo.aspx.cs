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


/// <summary>
/// Mantenimiento CORRECTIVO
/// </summary>
public partial class Solicitudes_MantCorrectivo : System.Web.UI.Page
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
            CargaComboSitios();
            FillSolicitudEmpleados(0);
            FillSolicitudVehiculos(0);
            LoadDataComplementaria();
            FillCorrectiva();
            RegistraJS();

        }
    }

    private void FillCorrectiva()
    {
        Solicitud sol = BiFactory.Sol;
        SolicitudCorrectivo Sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
        if (Sol_Cor != null)
        {
            txtReportoFalla.Enabled = false;
            txtCausa.Enabled = false;
            txtFechaReporte.Enabled = false;
            imgFechaReporte.Enabled = false;
            ddlHoraReporte.Enabled = false;
            ddlMinutosReporte.Enabled = false;
            txtFalla.Enabled = false;

            cmbPlazoAtencion.SelectedValue = Sol_Cor.IdPlazoAtencion.ToString();
            txtReportoFalla.Text = Sol_Cor.PersonaReportoFalla;
            txtFalla.Text = Sol_Cor.FallaReportada;
            txtCausa.Text = Sol_Cor.CausaPosible;
            txtFechaReporte.Text = Sol_Cor.FechanotificacionCliente.ToShortDateString();
            ddlHoraReporte.SelectedValue = Sol_Cor.FechanotificacionCliente.Hour.ToString();
            ddlMinutosReporte.SelectedValue = Sol_Cor.FechanotificacionCliente.Minute.ToString();
            cmbPlazoAtencion.SelectedValue = Sol_Cor.IdPlazoAtencion.ToString();


            if (Sol_Cor.Penaliza)
            { rdbPlazo.SelectedIndex = 1; }
            else { rdbPlazo.SelectedIndex =0; }

            Sitios unSitio = Sitios.FindOne(Expression.Eq("IdSitio", Sol_Cor.IdSitio));
            if (unSitio != null)
            {
                ListItem l = cboSitios.Items.FindByValue(unSitio.IdSitio.ToString());
                l.Selected = true;
                l.Text = unSitio.Nombre;
            }
        }
	}

    private void LoadDataComplementaria()
    {
        Solicitud sol = BiFactory.Sol;
        SolicitudCorrectivo sol_cor = SolicitudCorrectivo.FindOne(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
        
        if (sol_cor != null)
        {
            txtContactoCliente.Text = sol.Contacto;
            txtNroOrdenCliente.Text = sol.NroOrdenCte;
            cmbClientes.SelectedValue = sol.IdCliente.ToString();
            txtMailContacto.Text = sol.ContactoMail;
            txtTelefonoContacto.Text = sol.ContactoTel;
            cmbPlazoAtencion.SelectedValue = sol_cor.IdPlazoAtencion.ToString();
            cmbClientes.SelectedValue = sol.IdCliente.ToString();
            ucAdjuntos.ListaAdjuntos(sol.Id_Solicitud.ToString());

        }
    }

    public void CargaComboSitios()
    {
        cboSitios.Enabled = true;
        cboSitios.Items.Clear();
        cboSitios.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Sitios sitio in Antares.model.Sitios.FindAll(Expression.Eq("Activo", true)))
        {
            cboSitios.Items.Add(new ListItem(sitio.Nombre, sitio.IdSitio.ToString()));
        }

    }

    public void CargarCombos()
    {
        txtFechaReporte.Text = DateTime.Today.ToString("dd/MM/yyyy");
        
        foreach (Antares.model.PlazoRealizacion plazo in Antares.model.PlazoRealizacion.FindAll())
        {
            cmbPlazoAtencion.Items.Add(new ListItem(plazo.Descripcion, plazo.Id.ToString()));
            if (plazo.Descripcion == "72 Horas")
            {
                cmbPlazoAtencion.SelectedIndex = cmbPlazoAtencion.Items.Count -1 ;
            }
        }

        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.GetVehiculosActivos())
        {
            lstVehiculos.Items.Add(new ListItem(vehi.NUnidad + "-" + vehi.Marca + " " + vehi.Modelo + " - " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }
        
        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre , emp.IdEmpresa.ToString()));
        }
        
        cmbResponsable.Items.Clear();
        cmbResponsable.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal responsable in Antares.model.Personal.GetPersonalActivo())
        {
            cmbResponsable.Items.Add(new ListItem(responsable.Apellido + ", " + responsable.Nombres, responsable.IdEmpleados.ToString()));
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
        if (IsValid && EsSolicitudValida())
        {
            Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            
            sol.IdCliente = int.Parse(cmbClientes.SelectedValue);
            sol.Contacto = txtContactoCliente.Text;
            sol.NroOrdenCte = txtNroOrdenCliente.Text;
            sol.Status = eEstados.Pendiente.ToString();
            sol.ContactoMail = txtMailContacto.Text;
            sol.ContactoTel = txtTelefonoContacto.Text;
            
            SolicitudCorrectivo Sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));

            if (Sol_Cor == null)
            {
                Sol_Cor = new SolicitudCorrectivo();
                Sol_Cor.IdSolicitud = sol.Id_Solicitud;
            }
            Sol_Cor.IdPlazoAtencion = int.Parse(cmbPlazoAtencion.SelectedValue);
            Sol_Cor.PersonaReportoFalla = txtReportoFalla.Text;
            Sol_Cor.FallaReportada = txtFalla.Text;
            Sol_Cor.FechanotificacionCliente = DateTime.Parse(txtFechaReporte.Text).AddHours(double.Parse(ddlHoraReporte.SelectedValue)).AddMinutes(double.Parse(ddlMinutosReporte.SelectedValue));
            Sol_Cor.CausaPosible = txtCausa.Text;
            Sol_Cor.FechaResolucion = DateTime.Parse("1900-01-01");

            Sol_Cor.Penaliza = bool.Parse(rdbPlazo.SelectedValue);



            if (cboSitios.SelectedIndex > 0)
            {
                Sol_Cor.IdSitio = int.Parse(cboSitios.SelectedValue);
            }
            
            sol.Save();
            Sol_Cor.Save();

            pnlMantenimientoCorrectivo.Visible = false;

            ucMantenimientoCorrectivo.Numero = Sol_Cor.IdSolicitud.ToString();
            ucMantenimientoCorrectivo.Titulo = sol.Descripcion;
            ucMantenimientoCorrectivo.Estado = sol.Status;
            ucMantenimientoCorrectivo.ReportoFalla = Sol_Cor.PersonaReportoFalla;
            ucMantenimientoCorrectivo.CausaProbable = Sol_Cor.CausaPosible;
            ucMantenimientoCorrectivo.FechaReporte = Sol_Cor.FechanotificacionCliente.ToString("dd/MM/yyyy HH:mm");
            ucMantenimientoCorrectivo.Falla = Sol_Cor.FallaReportada;
            if (Sol_Cor.IdSitio != 0)
            {
                ucMantenimientoCorrectivo.Sitio = Sitios.FindFirst(Expression.IdEq(Sol_Cor.IdSitio)).Nombre.ToString();
            }
            ucMantenimientoCorrectivo.Plazo = cmbPlazoAtencion.SelectedItem.Text;
            string penaliza;
            if (Sol_Cor.Penaliza) { penaliza = "Genera Penalización"; } else { penaliza = "No Genera Penalizacion"; }
            ucMantenimientoCorrectivo.Penaliza = penaliza;
            ucMantenimientoCorrectivo.Personal = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivo.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivo.Cliente = cmbClientes.SelectedItem.Text;
            ucMantenimientoCorrectivo.ContactoCliente = sol.Contacto;
            ucMantenimientoCorrectivo.NroOrden = sol.NroOrdenCte;
            ucMantenimientoCorrectivo.TelefonoContacto = sol.ContactoTel;
            ucMantenimientoCorrectivo.MailContacto = sol.ContactoMail;
            ucMantenimientoCorrectivo.Adjuntos = sol.GetAdjuntos();
            
            decimal gastos = Solicitud.Valida_Gastos_Ingresados_Solicitud(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivo.Monto = gastos.ToString();
            ucMantenimientoCorrectivo.Gastos = SolicitudGastos.FindAll(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud)); 
            ucMantenimientoCorrectivo.Visible = true;

            WebAntares.AntaresHelper.NotificaSolicitud(sol.Id_Solicitud);
        }
    }

    protected void gvSolicitudVehiculos_rowDeleting(object sender, GridViewDeleteEventArgs e)
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

    protected void CargaListaEmpleados(int IdDelResponsable)
    {
        foreach (Antares.model.Personal persona in Antares.model.Personal.GetPersonalActivo())
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
        //if (!Solicitud.TieneServiciosAfectados(idSol) && gvServicios.Rows.Count == 0)
        //{
        //    esValida = false;
        //    errores.Add("Debe seleccionar al menos un servicio.");
        //}
        if (!Solicitud.TieneResponsable(idSol))
        {
            esValida = false;
            errores.Add("Debe asignar al menos un responsable.");
        }
        //if (gvSolicitudVehiculos.Rows.Count == 0)
        //{
        //    esValida = false;
        //    errores.Add("Debe asignar al menos un vehículo.");
        //}
        if (!esValida)
        {
            blErrores.DataSource = errores;
            blErrores.DataBind();
        }
        return esValida;
    }

    protected void RegistraJS()
    {
        //string popupScript = "<script language='JavaScript'>" +
        //"window.open('PopUp.aspx', 'CustomPopUp', " +
        //"'width=200, height=200, menubar=yes, resizable=no')" +
        //"</script>";


        string funcionJS = "<script type=\"text/javascript\"> function CopiaValor()" +
        "{ document.getElementById(\"ctl00_PageContainer_tcMantenimientoPreventivo_tpComplemento_txtContactoCliente\").value " +
        " = document.getElementById(\"ctl00_PageContainer_tcMantenimientoPreventivo_tpFalla_txtReportoFalla\").value ;} </script>";

        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "CopiaValor", funcionJS);


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
        if (gastos == 0 )
        {
            args.IsValid = false;

            cvGastosEnSolicitud.ErrorMessage = "No se ha cargado ningun Gasto  a la Solicitud";


        }
        else { args.IsValid = true; }


    }

}
