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

public partial class Solicitudes_Reporte_Obras : System.Web.UI.Page
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
            FillCamposObra();
            FillSolicitudVehiculos(0);
            FillSolicitudEmpleados(0);
        }
    }

    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos t = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        FillSolicitudVehiculos(0);
    }

    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos t = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        FillSolicitudVehiculos(0);
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

    private void FillCamposObra()
    {
        SolicitudObra o = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        cmbCliente.SelectedValue = BiFactory.Sol.IdCliente.ToString();
        txtContacto.Text = BiFactory.Sol.Contacto;
        txtOrdenCompra.Text = BiFactory.Sol.NroOrdenCte;
        txtMail.Text = BiFactory.Sol.ContactoMail;
        txtTelefonoContacto.Text = BiFactory.Sol.ContactoTel;
        txtDescripcionTareas.Text = o.DescripcionTareas;
        txtPresupuesto.Text = o.Presupuesto;
        //txtInicio.Text = o.FechaInicio.Substring(0, 10);
        //txtEntrega.Text = o.FechaFin.Substring(0, 10);
        txtInicio.Text = o.FechaInicio.ToString("dd/MM/yyyy");
        txtEntrega.Text = o.FechaFin.ToString("dd/MM/yyyy");
        txtRequisitosAprovacion.Text = o.RequisitosAprovacion;
        txtRequisitoIngreso.Text = o.RequisitosIngreso;
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

    private void CargarCombos()
    {
        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.GetVehiculosActivos())
        {
            lstVehiculos.Items.Add(new ListItem(vehi.NUnidad + "-" + vehi.Marca + " " + vehi.Modelo + " - " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }
        foreach (Antares.model.Personal p in Antares.model.Personal.FindAll())
        {
            lstEmpleadosSolicitud.Items.Add(new ListItem(p.Apellido + ", " + p.Nombres, p.IdEmpleados.ToString()));
        }
        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbCliente.Items.Add(new ListItem(emp.Nombre, emp.IdEmpresa.ToString()));
        }

        cmbResponsable.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal responsable in Antares.model.Personal.GetPersonalActivo())
        {
            cmbResponsable.Items.Add(new ListItem(responsable.Apellido + ", " + responsable.Nombres, responsable.IdEmpleados.ToString()));
        }
    }
    
    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            Solicitud Sol_Original = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            Sol_Original.Status = eEstados.Realizado.ToString();
            Sol_Original.Update();

            Solicitud Reporte = Solicitud.FindFirst(Expression.Eq("IdSolicitudInicial", Sol_Original.Id_Solicitud));

            SolicitudObra Obra = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

            Reporte.Status = eEstados.Realizado.ToString();
            Reporte.DescripcionReporte = txtDescripcionTrabajo.Text;
            Reporte.Save();

            pnlReporteObras.Visible = false;

            ucObrasRendicion.Numero = Obra.IdSolicitud.ToString();
            ucObrasRendicion.SolicitudInicial = Sol_Original.Id_Solicitud.ToString();
            ucObrasRendicion.Titulo = Sol_Original.Descripcion;
            ucObrasRendicion.Estado = Sol_Original.Status;
            ucObrasRendicion.Cliente = cmbCliente.SelectedItem.Text;
            ucObrasRendicion.NroOrden = Sol_Original.NroOrdenCte;
            ucObrasRendicion.Contacto = Sol_Original.Contacto;
            ucObrasRendicion.MailContacto = Sol_Original.ContactoMail;
            ucObrasRendicion.TelefonoContacto = Sol_Original.ContactoTel;
            ucObrasRendicion.DescripcionTareas = Obra.DescripcionTareas;
            ucObrasRendicion.FechaInicio = Obra.FechaInicio.ToString("dd/MM/yyyy");
            ucObrasRendicion.FechaEntrega = Obra.FechaFin.ToString("dd/MM/yyyy");
            ucObrasRendicion.RequisitosAprobacion = Obra.RequisitosAprovacion;
            ucObrasRendicion.RequisitosIngreso = Obra.RequisitosIngreso;
            ucObrasRendicion.Personal = SolicitudRecursosEmpleados.GetPersonaHoras_Detalle_EnSolicitud(BiFactory.Sol.Id_Solicitud);
            ucObrasRendicion.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
            ucObrasRendicion.Monto = Obra.Presupuesto;
            ucObrasRendicion.Adjuntos = Sol_Original.GetAdjuntos();
            ucObrasRendicion.Descripcion_TrabajoRealizado = txtDescripcionTrabajo.Text;
            ucObrasRendicion.Responsable = Solicitud.GetResponsable(BiFactory.Sol.Id_Solicitud.ToString());
            ucObrasRendicion.Visible = true;
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
        cvHorasPersonal.ValueToCompare = fechaInicio.ToShortDateString();
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

    
    protected void btnAceptarGastos_Click(object sender, ImageClickEventArgs e)
    {
        lblGastos.Visible = true;
        lblGastos.Text = "$" + txtPresupuesto.Text;
        txtPresupuesto.Text = "";
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
        gvHorasPersonal.PageIndex = pageIndex;
        //gvHorasPersonal.Sort("Fecha", SortDirection.Ascending);
        gvHorasPersonal.DataBind();


    }

    protected void gvHorasPersonal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRendicionPersonalHoras R = SolicitudRendicionPersonalHoras.FindFirst(Expression.Eq("Id", int.Parse(gvHorasPersonal.DataKeys[e.RowIndex].Value.ToString())));
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
            //SolicitudRendicionPersonalHoras ph;
            //SolicitudRendicionPersonalHoras ph = SolicitudRendicionPersonalHoras.FindFirst(
            //    Expression.Eq("IdSolicitud", idSolicitud),
            //    Expression.Eq("IdPersona", idPersona),
            //    Expression.Eq("Fecha", fecha));

            //if (ph == null)
            //{
            //    ph = new SolicitudRendicionPersonalHoras();
            //}

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
        //args.Value
        int idPersona = int.Parse(hfHorasPersonalPersona.Value);

        CultureInfo nfo = new CultureInfo("es-ES");
        DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text, nfo);
        //DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);
        Personal P = Personal.GetById(idPersona.ToString());

        decimal HorasACargar = decimal.Parse(ddlHorasPersonalHoras.SelectedValue);
        decimal HorasCargadas_A_TalFecha = Personal.GetHorasCargadas_X_Dia(idPersona, fecha);
        decimal HorasXSemana = decimal.Parse(AntaresHelper.Get_Config_HorasPersonaSemana());
        decimal HorasXDia = decimal.Parse(AntaresHelper.Get_Config_HorasPersonaDia());

        args.IsValid = false;

        cvCheckHorasRestantes.ErrorMessage = P.Apellido + ',' + P.Nombres
            + " ya tiene cargadas " + HorasCargadas_A_TalFecha.ToString("N2") + " Hs al " + fecha.ToShortDateString()
            + ", Solo se pueden cargar hasta " + HorasXDia.ToString() + " Horas Diarias";
        if ((HorasCargadas_A_TalFecha + HorasACargar) <= HorasXDia)
        {
            args.IsValid = true;
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
    
}


