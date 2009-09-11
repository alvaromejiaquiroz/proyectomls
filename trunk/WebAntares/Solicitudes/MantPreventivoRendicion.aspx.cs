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
            //btnPopUp.Style.Add("display", "none");
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

            pnlMantenimientoPreventivoRendicion.Visible = false;

            ucMantenimientoPreventivoRendicion.Numero = r.Id.ToString();
            ucMantenimientoPreventivoRendicion.SolicitudInicial = Sol_Original.Id_Solicitud.ToString();
            ucMantenimientoPreventivoRendicion.Titulo = Sol_Original.Descripcion;
            ucMantenimientoPreventivoRendicion.Estado = Sol_Original.Status;
            ucMantenimientoPreventivoRendicion.Sitio = litSitio.Text;
            ucMantenimientoPreventivoRendicion.Tareas = SolicitudTareas.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoPreventivoRendicion.Personal = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoPreventivoRendicion.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoPreventivoRendicion.Cliente = cmbClientes.SelectedItem.Text;
            ucMantenimientoPreventivoRendicion.ContactoCliente = Sol_Original.Contacto;
            ucMantenimientoPreventivoRendicion.NroOrden = Sol_Original.NroOrdenCte;
            ucMantenimientoPreventivoRendicion.TelefonoContacto = Sol_Original.ContactoTel;
            ucMantenimientoPreventivoRendicion.MailContacto = Sol_Original.ContactoMail;
            ucMantenimientoPreventivoRendicion.Adjuntos = Sol_Original.GetAdjuntos();
            ucMantenimientoPreventivoRendicion.Monto = r.Presupuesto;

            ucMantenimientoPreventivoRendicion.Visible = true;
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

    protected void btnHorasPersonalGuardar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int idPersona = int.Parse(hfHorasPersonalPersona.Value);
            int idSolicitud = int.Parse(hfHorasPersonalSolicitud.Value);
            DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);

            SolicitudRendicionPersonalHoras ph = SolicitudRendicionPersonalHoras.FindFirst(
                Expression.Eq("IdSolicitud", idSolicitud),
                Expression.Eq("IdPersona", idPersona),
                Expression.Eq("Fecha", fecha));
            
            if (ph == null)
            {
                ph = new SolicitudRendicionPersonalHoras();
            }

            ph.IdPersona = idPersona;
            ph.IdSolicitud = idSolicitud;
            ph.Fecha = fecha;
            ph.Horas = decimal.Parse(ddlHorasPersonalHoras.SelectedValue);
            ph.Descripcion = txtHorasPersonalDescripcion.Text;
            ph.SaveAndFlush();
            FillHorasPersonalGrid();
            mpeHorasPersonal.Show();
        }
    }

    protected void gvPersonas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        txtHorasPersonalDia.Text = string.Empty;
        ddlHorasPersonalHoras.SelectedIndex = 0;
        txtHorasPersonalDescripcion.Text = string.Empty;
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.NewEditIndex].Value.ToString())));
        hfHorasPersonalSolicitud.Value = p.IdSolicitud.ToString();
        hfHorasPersonalPersona.Value = p.IdEmpleado.ToString();
        Personal personal = Personal.GetById(p.IdEmpleado.ToString());
        litHorasPersonalPersona.Text = personal.Apellido + ", " + personal.Nombres;
        Solicitud sol = Solicitud.GetById(p.IdSolicitud);
        DateTime fechaInicio = sol.FechaCreacion;
        switch (sol.Tipo.Descripcion)
        {
            case "Mantenimiento Correctivo":
                SolicitudCorrectivo sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = sol_Cor.FechanotificacionCliente;
                break;
            case "Mantenimiento Preventivo":
                SolicitudPreventivo sol_Pre = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = DateTime.Parse(sol_Pre.FechaInicio);
                break;
            case "Obras e Instalaciones":
                SolicitudObra sol_Obr = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = DateTime.Parse(sol_Obr.FechaInicio);
                break;
        }
        cvHorasPersonal.ValueToCompare = fechaInicio.ToShortDateString();
        FillHorasPersonalGrid();
        mpeHorasPersonal.Show();
    }

    protected void gvHorasPersonal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRendicionPersonalHoras R = SolicitudRendicionPersonalHoras.FindFirst(Expression.Eq("Id", int.Parse(gvHorasPersonal.DataKeys[e.RowIndex].Value.ToString())));
        R.Delete();
        FillHorasPersonalGrid();
        mpeHorasPersonal.Show();
    }

    private void FillHorasPersonalGrid()
    {
        gvHorasPersonal.DataSource = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(int.Parse(hfHorasPersonalSolicitud.Value), int.Parse(hfHorasPersonalPersona.Value));
        gvHorasPersonal.DataKeyNames = new string[] { "Id" };
        gvHorasPersonal.DataBind();
    }

    protected void gvSolicitudVehiculos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        txtHorasVehiculosDia.Text = string.Empty;
        ddlHorasVehiculosHoras.SelectedIndex = 0;
        txtHorasVehiculosKilometros.Text = string.Empty;
        txtHorasVehiculosDescripcion.Text = string.Empty;
        SolicitudRecursosVehiculos s = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.NewEditIndex].Value.ToString())));
        hfHorasVehiculosVehiculo.Value = s.IdVehiculo.ToString();
        hfHorasVehiculosSolicitud.Value = s.IdSolicitud.ToString();
        Vehiculos p = Vehiculos.GetById(s.IdVehiculo.ToString());
        litHorasVehiculosVehiculo.Text = p.Patente + " - " + p.Marca + ", " + p.Modelo;
        Solicitud sol = Solicitud.GetById(s.IdSolicitud);
        DateTime fechaInicio = sol.FechaCreacion;
        switch (sol.Tipo.Descripcion)
        {
            case "Mantenimiento Correctivo":
                SolicitudCorrectivo sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = sol_Cor.FechanotificacionCliente;
                break;
            case "Mantenimiento Preventivo":
                SolicitudPreventivo sol_Pre = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = DateTime.Parse(sol_Pre.FechaInicio);
                break;
            case "Obras e Instalaciones":
                SolicitudObra sol_Obr = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = DateTime.Parse(sol_Obr.FechaInicio);
                break;
        }
        cvHorasVehiculos.ValueToCompare = fechaInicio.ToShortDateString();
        FillHorasVehiculosGrid();
        mpeHorasVehiculos.Show();
    }

    protected void btnHorasVehiculosGuardar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int idVehiculo = int.Parse(hfHorasVehiculosVehiculo.Value);
            int idSolicitud = int.Parse(hfHorasVehiculosSolicitud.Value);
            DateTime fecha = DateTime.Parse(txtHorasVehiculosDia.Text);
            
            SolicitudRendicionVehiculosHoras ph = SolicitudRendicionVehiculosHoras.FindFirst(
            Expression.Eq("IdSolicitud", idSolicitud),
            Expression.Eq("IdVehiculo", idVehiculo),
            Expression.Eq("Fecha", fecha));
            
            if (ph == null)
            {
                ph = new SolicitudRendicionVehiculosHoras();
            }

            ph.IdVehiculo = idVehiculo;
            ph.IdSolicitud = idSolicitud;
            ph.Fecha = fecha;
            ph.Horas = decimal.Parse(ddlHorasVehiculosHoras.SelectedValue);
            ph.Descripcion = txtHorasVehiculosDescripcion.Text;
            ph.Kilometros = txtHorasVehiculosKilometros.Text;
            ph.SaveAndFlush();
            FillHorasVehiculosGrid();
            mpeHorasVehiculos.Show();
        }
    }

    protected void gvHorasVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRendicionVehiculosHoras R = SolicitudRendicionVehiculosHoras.FindFirst(Expression.Eq("Id", int.Parse(gvHorasVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        R.Delete();
        FillHorasVehiculosGrid();
        mpeHorasVehiculos.Show();
    }

    private void FillHorasVehiculosGrid()
    {
        gvHorasVehiculos.DataSource = SolicitudRendicionVehiculosHoras.GetVehiculosKm_Detalle_EnSolicitud(int.Parse(hfHorasVehiculosSolicitud.Value), int.Parse(hfHorasVehiculosVehiculo.Value));
        gvHorasVehiculos.DataKeyNames = new string[] { "Id" };
        gvHorasVehiculos.DataBind();
    }
}
