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

/// <summary>
/// Mantenimiento CORRECTIVO
/// </summary>
public partial class Solicitudes_MantCorrectivo : System.Web.UI.Page
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
            FillServicios();
            FillSolicitudEmpleados();
            FillSolicitudVehiculos();
            LoadDataComplementaria();
            FillCorrectiva();
        }
    }

    private void FillCorrectiva()
    {
        Solicitud sol = BiFactory.Sol;
        SolicitudCorrectivo Sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
        if (Sol_Cor != null)
        {
            cmbPlazoAtencion.SelectedValue = Sol_Cor.IdPlazoAtencion.ToString();
            txtReportoFalla.Text = Sol_Cor.PersonaReportoFalla;
            txtFalla.Text = Sol_Cor.FallaReportada;
            txtCausa.Text = Sol_Cor.CausaPosible;
            txtFechaReporte.Text = Sol_Cor.FechanotificacionCliente.ToShortDateString();
            ddlHoraReporte.SelectedValue = Sol_Cor.FechanotificacionCliente.Hour.ToString();
            ddlMinutosReporte.SelectedValue = Sol_Cor.FechanotificacionCliente.Minute.ToString();
            cmbPlazoAtencion.SelectedValue = Sol_Cor.IdPlazoAtencion.ToString();
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
            txtPresupuesto.Text = sol_cor.Presupuesto;
            ucAdjuntos.ListaAdjuntos(sol.Id_Solicitud.ToString());
        }
    }

    private void FillServicios()
    {
        gvServicios.DataSource = SolicitudServiciosAfectados.GetServiciosAfectados (BiFactory.Sol.Id_Solicitud);
        gvServicios.DataKeyNames = new string[] { "Id" };
        gvServicios.DataBind();
    }
    
    public void CargarCombos()
    {
        foreach (Antares.model.Servicios serv in Antares.model.Servicios.FindAll())
        {
            lstServiciosAfectados.Items.Add(new ListItem(serv.Descripcion, serv.Id.ToString()));
        }
        foreach (Antares.model.PlazoRealizacion plazo in Antares.model.PlazoRealizacion.FindAll())
        {
            cmbPlazoAtencion.Items.Add(new ListItem(plazo.Descripcion, plazo.Id.ToString()));
        }
        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.FindAll())
        {
            lstVehiculos.Items.Add(new ListItem(vehi.Marca + " " + vehi.Modelo + " " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }
        
        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre + " (" + emp.Localidad + ")", emp.IdEmpresa.ToString()));
        }
        
        cmbResponsable.Items.Clear();
        cmbResponsable.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal responsable in Antares.model.Personal.FindAll())
        {
            cmbResponsable.Items.Add(new ListItem(responsable.Apellido + ", " + responsable.Nombres, responsable.IdEmpleados.ToString()));
        }

    }

    protected void btnAsignaServicio_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int id_Sol = BiFactory.Sol.Id_Solicitud;
            int idServicio;
            for (int i = 0; i < lstServiciosAfectados.Items.Count; i++)
            {
                if (lstServiciosAfectados.Items[i].Selected)
                {
                    idServicio = int.Parse(lstServiciosAfectados.Items[i].Value);
                    if (SolicitudServiciosAfectados.FindAll(Expression.And(Expression.Eq("IdSolicitud", id_Sol), Expression.Eq("IdServicioAfectado", idServicio))).Length == 0)
                    {
                        SolicitudServiciosAfectados t = new SolicitudServiciosAfectados();
                        t.IdServicioAfectado = int.Parse(lstServiciosAfectados.Items[i].Value.ToString());
                        t.IdSolicitud = BiFactory.Sol.Id_Solicitud;
                        t.Save();
                    }
                }
            }
            FillServicios();
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
        DataTable dt = new DataTable();
        gvSolicitudPersonas.DataSource = dt;
        gvSolicitudPersonas.DataBind();

        gvSolicitudPersonas.DataSource = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
        gvSolicitudPersonas.DataKeyNames = new string[] { "Id" };
        gvSolicitudPersonas.DataBind();
    }

    private void FillSolicitudVehiculos()
    {
        gvSolicitudVehiculos.DataSource = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
        gvSolicitudVehiculos.DataKeyNames = new string[] { "Id" };
        gvSolicitudVehiculos.DataBind();
    }

    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        if (IsValid && EsSolicitudValida())
        {
            Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            
            for (int i = 0; i < lstServiciosAfectados.Items.Count; i++)
            {
                if (lstServiciosAfectados.Items[i].Selected)
                {
                    SolicitudServiciosAfectados t = new SolicitudServiciosAfectados();
                    t.IdServicioAfectado = int.Parse(lstServiciosAfectados.Items[i].Value.ToString());
                    t.IdSolicitud = BiFactory.Sol.Id_Solicitud;
                    t.Save();
                }
            }
            
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
            Sol_Cor.Presupuesto = txtPresupuesto.Text;
            
            sol.Save();
            Sol_Cor.Save();

            pnlMantenimientoCorrectivo.Visible = false;

            ucMantenimientoCorrectivo.Numero = Sol_Cor.IdSolicitud.ToString();
            ucMantenimientoCorrectivo.Titulo = sol.Descripcion;
            ucMantenimientoCorrectivo.ReportoFalla = Sol_Cor.PersonaReportoFalla;
            ucMantenimientoCorrectivo.CausaProbable = Sol_Cor.CausaPosible;
            ucMantenimientoCorrectivo.FechaReporte = Sol_Cor.FechanotificacionCliente.ToString("dd/MM/yyyy HH:mm");
            ucMantenimientoCorrectivo.Falla = Sol_Cor.FallaReportada;
            ucMantenimientoCorrectivo.Servicios = SolicitudServiciosAfectados.GetServiciosAfectados(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivo.Plazo = cmbPlazoAtencion.SelectedItem.Text;
            ucMantenimientoCorrectivo.Personal = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivo.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivo.Cliente = cmbClientes.SelectedItem.Text;
            ucMantenimientoCorrectivo.ContactoCliente = sol.Contacto;
            ucMantenimientoCorrectivo.NroOrden = sol.NroOrdenCte;
            ucMantenimientoCorrectivo.TelefonoContacto = sol.ContactoTel;
            ucMantenimientoCorrectivo.MailContacto = sol.ContactoMail;
            ucMantenimientoCorrectivo.Adjuntos = sol.GetAdjuntos();
            ucMantenimientoCorrectivo.Monto = Sol_Cor.Presupuesto;
            ucMantenimientoCorrectivo.Visible = true;
        }
    }

    protected void cvVehiculos_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = lstVehiculos.SelectedIndex >= 0;
    }

    protected void cvServiciosAfectados_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = lstServiciosAfectados.SelectedIndex >= 0;
    }

    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos v = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        v.Delete();
        FillSolicitudVehiculos();
    }

    protected void gvServicios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudServiciosAfectados s = SolicitudServiciosAfectados.FindFirst(Expression.Eq("Id", int.Parse(gvServicios.DataKeys[e.RowIndex].Value.ToString())));
        s.Delete();
        FillServicios();
    }

    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.RowIndex].Value.ToString())));
        p.Delete();
        FillSolicitudEmpleados();
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
        if (!Solicitud.TieneServiciosAfectados(idSol) && gvServicios.Rows.Count == 0)
        {
            esValida = false;
            errores.Add("Debe seleccionar al menos un servicio.");
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
}
