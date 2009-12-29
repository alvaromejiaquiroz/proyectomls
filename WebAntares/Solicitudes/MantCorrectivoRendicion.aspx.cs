﻿using System;
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
public partial class Solicitudes_MantPreventivo : System.Web.UI.Page
{
    protected override void OnInitComplete(EventArgs e)
    {
        ucAdjuntos.sol = BiFactory.Sol;
        ucSolicitudGastos.Sol = BiFactory.Sol;
        base.OnInitComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SolicitudCorrectivo sol_c = SolicitudCorrectivo.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
            if (sol_c != null)
            {
                DateTime fechafin = sol_c.FechanotificacionCliente.AddDays(7);
                if (!AntaresHelper.EsCorrecta_Fecha_a_Cargar(fechafin))
                //if (!AntaresHelper.PuedeGenerarReporte(sol_p.FechaFin))
                {
                    Session["mensaje"] = "Se ha vencido el plazo para generar el reporte de la solicitud " + sol_c.IdSolicitud.ToString();
                    Response.Redirect("~/default.aspx");

                }
            }
            CargarCombos();
            FillSolicitudEmpleados(0);
            FillSolicitudVehiculos(0);
            LoadDataComplementaria();
            FillCorrectiva();
        }
    }

    private void FillCorrectiva()
    {
        txtReportoFalla.Enabled = false;
        txtCausa.Enabled = false;
        txtFechaReporte.Enabled = false;
        imgFechaReporte.Enabled = false;
        ddlHoraReporte.Enabled = false;
        ddlMinutosReporte.Enabled = false;
        txtFalla.Enabled = false;


        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        SolicitudCorrectivo Sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
        if (Sol_Cor != null)
        {
            txtReportoFalla.Text = Sol_Cor.PersonaReportoFalla;
            txtFalla.Text = Sol_Cor.FallaReportada;
            txtCausa.Text = Sol_Cor.CausaPosible;
            txtFechaReporte.Text = Sol_Cor.FechanotificacionCliente.ToShortDateString();
            ddlHoraReporte.SelectedValue = Sol_Cor.FechanotificacionCliente.Hour.ToString();
            ddlMinutosReporte.SelectedValue = Sol_Cor.FechanotificacionCliente.Minute.ToString();
           
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
        txtContactoCliente.Text = sol.Contacto;
        txtNroOrdenCliente.Text = sol.NroOrdenCte;
        cmbClientes.SelectedValue = sol.IdCliente.ToString();
        txtMailContacto.Text = sol.ContactoMail;
        txtTelefonoContacto.Text = sol.ContactoTel;
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
        lstVehiculos.Items.Clear();
        string sql = " Id_Vehiculos not in (select Id_Vehiculo from dbo.Solicitud_Recursos_Vehiculos where id_solicitud = " + BiFactory.Sol.Id_Solicitud.ToString() + ")";
        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.FindAll(Expression.Sql(sql)))
        {
            lstVehiculos.Items.Add(new ListItem(vehi.NUnidad + "-" + vehi.Marca + " " + vehi.Modelo + " - " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }
    }

    public void CargarCombos()
    {
        CargaComboSitios();
        CargaComboPersonal();
        CargaComboVehiculos();

        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre , emp.IdEmpresa.ToString()));
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

    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos v = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        v.Delete();
        FillSolicitudVehiculos(0);
    }

    protected void gvServicios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
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
                        t.Fecha = AntaresHelper.FechaNula();
                        t.Save();
                    }
                }
            }
            FillSolicitudVehiculos(0);
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

    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        if (IsValid && EsSolicitudValida())
        {
            Solicitud Sol_Original = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            Sol_Original.Status = eEstados.Realizado.ToString();
            Sol_Original.Update();

            Solicitud Reporte = Solicitud.FindFirst(Expression.Eq("IdSolicitudInicial", Sol_Original.Id_Solicitud));
            Reporte.DescripcionReporte = txtDescripcionTrabajo.Text;
            SolicitudCorrectivo Corr = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", Reporte.Id_Solicitud));

            if (Corr != null)
            {
                //if (txtPresupuesto.Text == "")
                //{
                //    Corr.Presupuesto = lblGastos.Text.Replace("$", "");
                //}
                //else
                //{
                //    Corr.Presupuesto = txtPresupuesto.Text;
                //}

                Corr.FechaResolucion = DateTime.Now;
                Corr.ContactoConformidadCliente = txtContactoConformidad.Text;
                Corr.IdSitio = int.Parse(cboSitios.SelectedValue);
                Corr.Save();
            }
            //Agregar la fecha de solicitud a la solicitud del tipo preventivo, asi tambien a correctivo
            Reporte.Status = eEstados.Realizado.ToString();
            Reporte.DescripcionReporte = txtDescripcionTrabajo.Text;
            Reporte.Save();
            

            
            pnlMantenimientoCorrectivo.Visible = false;

            //ucMantenimientoCorrectivoRendicion.Numero = Corr.IdSolicitud.ToString();
            ucMantenimientoCorrectivoRendicion.Numero = Reporte.IdSolicitudInicial.ToString();
            ucMantenimientoCorrectivoRendicion.SolicitudInicial = Reporte.IdSolicitudInicial.ToString();
            ucMantenimientoCorrectivoRendicion.Titulo = Sol_Original.Descripcion;
            ucMantenimientoCorrectivoRendicion.Estado = Sol_Original.Status;
            ucMantenimientoCorrectivoRendicion.ReportoFalla = Corr.PersonaReportoFalla;
            ucMantenimientoCorrectivoRendicion.CausaProbable = Corr.CausaPosible;
            ucMantenimientoCorrectivoRendicion.FechaReporte = Corr.FechanotificacionCliente.ToString("dd/MM/yyyy HH:mm");
            ucMantenimientoCorrectivoRendicion.Falla = Corr.FallaReportada;
            ucMantenimientoCorrectivoRendicion.Descripcion_TrabajoRealizado = txtDescripcionTrabajo.Text;
            ucMantenimientoCorrectivoRendicion.Conformidad_Cliente = txtContactoConformidad.Text;
            ucMantenimientoCorrectivoRendicion.Fecha_Cierre_Mantenimiento = Corr.FechaResolucion.ToString();
            ucMantenimientoCorrectivoRendicion.Sitio = Sitios.FindFirst(Expression.IdEq(Corr.IdSitio)).Nombre.ToString();
            ucMantenimientoCorrectivoRendicion.Personal = SolicitudRecursosEmpleados.GetPersonaHoras_Detalle_EnSolicitud(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivoRendicion.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivoRendicion.Cliente = cmbClientes.SelectedItem.Text;
            ucMantenimientoCorrectivoRendicion.ContactoCliente = Sol_Original.Contacto;
            ucMantenimientoCorrectivoRendicion.NroOrden = Sol_Original.NroOrdenCte;
            ucMantenimientoCorrectivoRendicion.TelefonoContacto = Sol_Original.ContactoTel;
            ucMantenimientoCorrectivoRendicion.MailContacto = Sol_Original.ContactoMail;
            ucMantenimientoCorrectivoRendicion.Adjuntos = Sol_Original.GetAdjuntos();
            
            decimal gastos = Solicitud.Valida_Gastos_Ingresados_Solicitud(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoCorrectivoRendicion.Monto= gastos.ToString();

            ucMantenimientoCorrectivoRendicion.Responsable = Solicitud.GetResponsable(BiFactory.Sol.Id_Solicitud.ToString());
            ucMantenimientoCorrectivoRendicion.Visible = true;
        }        
    }

    protected void cvVehiculos_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = lstVehiculos.SelectedIndex >= 0;
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
                fechaInicio = sol_Pre.FechaInicio;
                break;
            case "Obras e Instalaciones":
                SolicitudObra sol_Obr = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = sol_Obr.FechaInicio;
                break;
        }
        
        rngHorasPersonalValidator.MinimumValue = fechaInicio.ToShortDateString();
        //pongo como tope 10 dias , pero tengo que ir a buscar el viernes de la semana que viene.
        rngHorasPersonalValidator.MinimumValue = fechaInicio.AddDays(10).ToShortDateString();
        rngHorasPersonalValidator.MinimumValue = AntaresHelper.UltimoDiaSemana(fechaInicio.AddDays(7)).ToString("dd/MM/yyyy");

        FillHorasPersonalGrid(0);
        mpeHorasPersonal.Show();
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
                fechaInicio = sol_Pre.FechaInicio;
                break;
            case "Obras e Instalaciones":
                SolicitudObra sol_Obr = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                fechaInicio = sol_Obr.FechaInicio;
                break;
        }
        cvHorasVehiculos.ValueToCompare = fechaInicio.ToShortDateString();
        FillHorasVehiculosGrid(0);
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
            FillHorasVehiculosGrid(0);
            mpeHorasVehiculos.Show();
        }
    }

    protected void gvHorasVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRendicionVehiculosHoras R = SolicitudRendicionVehiculosHoras.FindFirst(Expression.Eq("Id", int.Parse(gvHorasVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        R.Delete();
        FillHorasVehiculosGrid(0);
        mpeHorasVehiculos.Show();
    }

   

    protected void gvSolicitudPersonas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillSolicitudEmpleados(e.NewPageIndex);

    }

    protected void gvSolicitudVehiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillSolicitudVehiculos(e.NewPageIndex);

    }



    #region Horas del PErsonal

    protected void LimpiaFormulario()
    {
        txtHorasPersonalDia.Text = string.Empty;
        txtHorasPersonalDescripcion.Text = string.Empty;
        ddlHorasPersonalHoras.SelectedIndex = 0;

    }

    protected void gvHorasPersonal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillHorasPersonalGrid(e.NewPageIndex);
        mpeHorasPersonal.Show();
    }

    private void FillHorasPersonalGrid(int pageIndex)
    {
        DbDataReader reader = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(int.Parse(hfHorasPersonalSolicitud.Value), int.Parse(hfHorasPersonalPersona.Value));

        DataTable table = new DataTable();
        table.Load(reader);
        gvHorasPersonal.DataSource = table;
        gvHorasPersonal.DataKeyNames = new string[] { "Id" };
        gvHorasPersonal.PageIndex = pageIndex;
        gvHorasPersonal.DataBind();


    }

    protected void gvHorasPersonal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int i = int.Parse(gvHorasPersonal.DataKeys[e.RowIndex].Value.ToString());
        SolicitudRendicionPersonalHoras R = SolicitudRendicionPersonalHoras.FindFirst(Expression.Eq("Id", i));
        R.Delete();
        FillHorasPersonalGrid(0);
        mpeHorasPersonal.Show();
    }

    protected void btnHorasPersonalGuardar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int idPersona = int.Parse(hfHorasPersonalPersona.Value);
            int idSolicitud = int.Parse(hfHorasPersonalSolicitud.Value);
            DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);
     
            SolicitudRendicionPersonalHoras ph = new SolicitudRendicionPersonalHoras();
            ph.IdPersona = idPersona;
            ph.IdSolicitud = idSolicitud;
            ph.Fecha = fecha;
            ph.Horas = decimal.Parse(ddlHorasPersonalHoras.SelectedValue);
            ph.Descripcion = txtHorasPersonalDescripcion.Text;
            ph.SaveAndFlush();
            LimpiaFormulario();
            FillHorasPersonalGrid(0);

        }
        mpeHorasPersonal.Show();
    }

    protected void cvCheckHorasRestantes_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CultureInfo nfo = new CultureInfo("es-ES");
        DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text, nfo);
        //DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);
        Personal Persona = Personal.GetById(hfHorasPersonalPersona.Value.ToString());
        int idPersona = Persona.IdEmpleados;

        decimal HorasACargar = decimal.Parse(ddlHorasPersonalHoras.SelectedValue);

        //Me traigo de la base las horas que tiene cargadas en un dia en particular
        //en este caso el dia que selecciono en el calendario

        decimal HorasCargadas_A_TalFecha = Personal.GetHorasCargadas_X_Dia(idPersona, fecha);

        //Me traigo de la base las horas x semana que se pueden cargar a tal fecha
        decimal HorasHabiles_Semana = AntaresHelper.Get_Config_HorasPersonaSemana_Fecha(fecha);

        //Me traigo de la base las horas x semana que se podian  cargar la semana pasada
        decimal HorasHabiles_de_la_SemanaPasada = AntaresHelper.Get_Config_HorasPersonaSemana_Fecha(fecha.AddDays(-7));

        //Horas que se pueden cargar por dia, esto esta definido en el webconfig segun Daniela
        decimal HorasXDia = decimal.Parse(AntaresHelper.Get_Config_HorasPersonaDia());

        //Horas que cargaron realmente la semana pasada a la fecha seleccionada
        decimal Horas_Cargadas_Semana_Pasada = Personal.GetHorasCargadas_Semana_Pasada(idPersona, fecha);

        //Horas que llevan cargadas la semana de la fecha en cuestion
        decimal Horas_Cargadas_Semana = Personal.GetHorasCargadas_Semana(idPersona, fecha);

        args.IsValid = false;

        cvCheckHorasRestantes.ErrorMessage = Persona.Apellido + ',' + Persona.Nombres
            + " ya tiene cargadas " + HorasCargadas_A_TalFecha.ToString("N2") + " Hs al " + fecha.ToShortDateString()
            + ", Solo se pueden cargar hasta " + HorasXDia.ToString() + " Horas Diarias";

        decimal HorasHabilitadasParaCargar = 0;

        if ((HorasCargadas_A_TalFecha + HorasACargar) <= HorasXDia)
        {

            args.IsValid = true;

            if (Horas_Cargadas_Semana_Pasada > HorasHabiles_de_la_SemanaPasada)
            {
                // calculo la diferencia entre las hroas q me pase del estandar por semana, porque esa diferencia 
                // la voy a restar a las que pueden
                // cargar en la semana actual
                HorasHabilitadasParaCargar = HorasHabiles_Semana - (Horas_Cargadas_Semana_Pasada - HorasHabiles_de_la_SemanaPasada);

            }
            else
            {
                HorasHabilitadasParaCargar = HorasHabiles_Semana + 9;
            }

            //Si las horas cargadas esta semana son menores a las que me quedan habilitadas para cargar
            //Entonces OK
            //Sino me tendria que avisar que puedo cargar las HorasHabilitadasParaCargar

            if (HorasHabilitadasParaCargar >= 0 && (Horas_Cargadas_Semana + HorasACargar) <= HorasHabilitadasParaCargar)
            {
                args.IsValid = true;

            }
            else
            {
                if (HorasHabiles_de_la_SemanaPasada > Horas_Cargadas_Semana_Pasada)
                {
                    cvCheckHorasRestantes.ErrorMessage = "Está intentando cargar " + (Horas_Cargadas_Semana + HorasACargar).ToString() + " Hs. en una Semana - El Maximo por semana es " + (HorasHabilitadasParaCargar).ToString() + " Hs.";
                }

                else
                {
                    cvCheckHorasRestantes.ErrorMessage = Persona.Apellido + ',' + Persona.Nombres +
                        " Tiene " + Horas_Cargadas_Semana_Pasada.ToString() + "Hs." +
                        " cargadas la semana pasada  , esta semana solo puede cargar " + (HorasHabilitadasParaCargar).ToString() + " Hs." +
                        " - Solo puede cargar  " + (HorasHabilitadasParaCargar - Horas_Cargadas_Semana).ToString() + " Hs.";
                }
                args.IsValid = false;

            }

        }


    }

    #endregion
    #region KM Vehiculos

    protected void gvHorasVehiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillHorasVehiculosGrid(e.NewPageIndex);
        mpeHorasVehiculos.Show();
    }

    private void FillHorasVehiculosGrid(int pageIndex)
    {
        DbDataReader reader = SolicitudRendicionVehiculosHoras.GetVehiculosKm_Detalle_EnSolicitud(int.Parse(hfHorasVehiculosSolicitud.Value), int.Parse(hfHorasVehiculosVehiculo.Value));

        DataTable table = new DataTable();
        table.Load(reader);
        gvHorasVehiculos.DataSource = table;
        gvHorasVehiculos.PageIndex = pageIndex;
        gvHorasVehiculos.DataKeyNames = new string[] { "Id" };
        gvHorasVehiculos.DataBind();

    }
    #endregion


    protected void cvPersonalIngresoHoras_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //args.Value
        string personas = Solicitud.PeronalSinHorasCargadasPersonal_En_Solicitud(BiFactory.Sol.Id_Solicitud);
        args.IsValid = false;
        if (personas != string.Empty)
        {
            args.IsValid = false;

            cvPersonalIngresoHoras.ErrorMessage = "No se les ha cargado horas a las siguientes personas : " + personas;


        }
        else { args.IsValid = true; }


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
  
}

