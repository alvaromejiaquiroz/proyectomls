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


public partial class Solicitudes_Capacitacion : System.Web.UI.Page
{
    static Antares.model.SolicitudCapacitacion sol_Cap;
    static Antares.model.Personal _persona;
    static int IdSol;
    static string Horas;
    static DateTime fechainicio;
    static DateTime fechafin;



    protected Personal Persona
    {
        get { return _persona; }
        set { _persona = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (BiFactory.Empleado.IdEmpleados > 0)
            {
                cmpVFecha.ValueToCompare = DateTime.Today.ToString("dd/MM/yyyy");
                CargarCombos();
                FillSol();
            }
            else
            {
                Session["mensaje"] = "El usuario " + BiFactory.User.Nombre + " no esta relacionado con ningun Empleado";
                Response.Redirect("~/default.aspx");
            }
    
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            SolicitudCapacitacion Cap = SolicitudCapacitacion.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

            if (Cap == null)
            {
                Cap = new SolicitudCapacitacion();
                Cap.IdSolicitud = BiFactory.Sol.Id_Solicitud;
                Cap.Descripcion = BiFactory.Sol.Descripcion;

            }

            Cap.FechaInicio = DateTime.MinValue;
            Cap.FechaFin = DateTime.MinValue;
            //Cap.FechaInicio = DateTime.Parse(txtInicio.Text);
            //Cap.FechaFin = DateTime.Parse(txtFin.Text);
            Cap.Nivel = cmbNivelCapacitacion.SelectedValue;
            Cap.Instructor = txtInstructor.Text;
            Cap.PuntuacionExamen = txtPuntuacionExamen.Text;
            //Cap.Duracion = decimal.Parse(txtDuracion.Text);
            Cap.EntidadEducativa = txtEntidad.Text;
            Cap.AreaEstudio = txtAreaEstudios.Text;
            Cap.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            Cap.SaveAndFlush();

            //pnlCapacitacion.Visible = false;

            //ucCapacitacion.Numero = Cap.IdSolicitud.ToString();
            //ucCapacitacion.Titulo = Cap.Descripcion;
            //ucCapacitacion.Nivel = Cap.Nivel;
            //ucCapacitacion.FechaInicio = Cap.FechaInicio.ToShortDateString();
            //ucCapacitacion.FechaFin = Cap.FechaFin.ToShortDateString();
            //ucCapacitacion.Duracion = Cap.Duracion.ToString();
            //ucCapacitacion.AreaEstudios = Cap.AreaEstudio;
            //ucCapacitacion.Instructor = Cap.Instructor;
            //ucCapacitacion.EntidadEducativa = Cap.EntidadEducativa;
            //ucCapacitacion.PuntuacionExamen = Cap.PuntuacionExamen;
            //ucCapacitacion.Visible = true;

            Sol = null;
        }
    }

    private void FillSol()
    {
        if (Request.QueryString["Id"] != null)
        {
            IdSol = int.Parse(Request.QueryString["Id"].ToString());
            BiFactory.Sol = Solicitud.GetById(IdSol);

        }

        sol_Cap = SolicitudCapacitacion.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
       
        if (sol_Cap != null)
        {
            
            cmbNivelCapacitacion.SelectedValue = sol_Cap.Nivel;
            txtAreaEstudios.Text = sol_Cap.AreaEstudio;
            txtEntidad.Text = sol_Cap.EntidadEducativa;
            txtInstructor.Text = sol_Cap.Instructor;
            txtPuntuacionExamen.Text = sol_Cap.PuntuacionExamen;
            Persona = Personal.GetById(sol_Cap.IdEmpleado.ToString());
            FillHorasPersonalGrid(0);
            MostrarSolicitud();
        }
        else
        {
            Persona = Personal.GetById(BiFactory.Empleado.IdEmpleados.ToString());
        }
        litHorasPersonalPersona.Text = Persona.Apellido + "," + Persona.Nombres;
        //CargarCombos();
    }
   
    private void CargarCombos()
    {
        cmbNivelCapacitacion.Items.Clear();
        cmbNivelCapacitacion.Items.Add("Básico");
        cmbNivelCapacitacion.Items.Add("Intermedio");
        cmbNivelCapacitacion.Items.Add("Avanzado");
        cmbNivelCapacitacion.Items.Add("Experto");
      
    }

    protected void DeshabilitaPanelPrincipal()
    {
        txtAreaEstudios.Enabled = false;
        txtEntidad.Enabled = false;
        txtInstructor.Enabled = false;
        txtPuntuacionExamen.Enabled = false;
        cmbNivelCapacitacion.Enabled = false;

    }

    protected void HabilitaPanelPrincipal()
    {
        txtAreaEstudios.Enabled = true;
        txtEntidad.Enabled = true;
        txtInstructor.Enabled = true;
        txtPuntuacionExamen.Enabled = true;
        cmbNivelCapacitacion.Enabled = true;
        txtAreaEstudios.Text = string.Empty;
        txtEntidad.Text = string.Empty;
        txtInstructor.Text = string.Empty;
        txtPuntuacionExamen.Text = string.Empty;
        BiFactory.Sol = null;


    }

    #region Horas del PErsonal

    protected void LimpiaFormulario()
    {
        //txtHorasPersonalDia.Text = string.Empty;
        //ddlHorasPersonalHoras.SelectedIndex = 0;

    }

    protected void gvHorasPersonal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillHorasPersonalGrid(e.NewPageIndex);

    }

    private void FillHorasPersonalGrid(int pageIndex)
    {
        DbDataReader reader = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(BiFactory.Sol.Id_Solicitud, BiFactory.Empleado.IdEmpleados);

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
       
    }

    protected void btnHorasPersonalGuardar_Click(object sender, EventArgs e)
    {
        
        if (IsValid)
        {
            //int idPersona = int.Parse(hfHorasPersonalPersona.Value);
            //int idSolicitud = int.Parse(hfHorasPersonalSolicitud.Value);
            DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);

            SolicitudRendicionPersonalHoras ph = new SolicitudRendicionPersonalHoras();
            ph.IdPersona = BiFactory.Empleado.IdEmpleados ;
            ph.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            ph.Fecha = fecha;
            ph.Horas = decimal.Parse(ddlHorasPersonalHoras.SelectedValue);
            ph.Descripcion = txtHorasPersonalDescripcion.Text;
            ph.SaveAndFlush();
            LimpiaFormulario();
            FillHorasPersonalGrid(0);

        }
    }

    protected void btnHorasPersonalCerrar_Click(object sender, EventArgs e)
    {
//        divHoras.Visible= false;
    }

    
    protected void cvCheckHorasRestantes_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //args.Value
        //int idPersona = int.Parse(hfHorasPersonalPersona.Value);
        int idPersona = BiFactory.Empleado.IdEmpleados;

        CultureInfo nfo = new CultureInfo("es-ES");
        DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text, nfo);
        //DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);
        Personal P = Personal.GetById(idPersona.ToString());

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

        cvCheckHorasRestantes.ErrorMessage = P.Apellido + ',' + P.Nombres
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
                    cvCheckHorasRestantes.ErrorMessage = P.Apellido + ',' + P.Nombres +
                        " Tiene " + Horas_Cargadas_Semana_Pasada.ToString() + "Hs." +
                        " cargadas la semana pasada  , esta semana solo puede cargar " + (HorasHabilitadasParaCargar).ToString() + " Hs." +
                        " - Solo puede cargar  " + (HorasHabilitadasParaCargar - Horas_Cargadas_Semana).ToString() + " Hs.";
                }
                args.IsValid = false;

            }

        }
    }
    #endregion

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }

    protected void btnVisualizar_Click(object sender, EventArgs e)
    {
        ucCapacitacion.MostrarEnsolicitud = true;
        MostrarSolicitud();
           
    }

    protected void btnConfirmarCarga_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            SolicitudCapacitacion Cap = SolicitudCapacitacion.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

            if (Cap == null)
            {
                Cap = new SolicitudCapacitacion();
                Cap.IdSolicitud = Sol.Id_Solicitud;
                Cap.Descripcion = Sol.Descripcion;
            }

            Cap.FechaInicio = AntaresHelper.FechaNula();
            Cap.FechaFin = AntaresHelper.FechaNula();
            Cap.Nivel = cmbNivelCapacitacion.SelectedValue;
            Cap.Instructor = txtInstructor.Text;
            Cap.PuntuacionExamen = txtPuntuacionExamen.Text;
            Cap.EntidadEducativa = txtEntidad.Text;
            Cap.AreaEstudio = txtAreaEstudios.Text;
            Cap.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            Cap.SaveAndFlush();
            //AntaresHelper.NotificaSolicitud(Cap.IdSolicitud);

            //txtHorasPersonalDia.Text = string.Empty;
            //ddlHorasPersonalHoras.SelectedIndex = 0;


            //DateTime fechaInicio = Sol.FechaCreacion;
            //DateTime fechaFin = DateTime.MinValue;

            //this.Persona = Personal.GetById(Cap.IdEmpleado.ToString());
            //litHorasPersonalPersona.Text = Persona.Apellido + "," + Persona.Nombres;
            //cmpVFecha.ValueToCompare = DateTime.Today.ToString("dd/MM/yyyy");
            //FillHorasPersonalGrid(0);
            Session["mensaje"] = "La solicitud " + BiFactory.Sol.Id_Solicitud.ToString() + " Ha sido creada con Exito";
            Response.Redirect("~/default.aspx");

        }
    }

    protected void btnFinalizarCarga_Click(object sender, EventArgs e)
    {
        MostrarSolicitud();
    }

    protected void MostrarSolicitud()
    {
        //ucCapacitacion.Duracion.Tipo = cmbTipoTarea.SelectedItem.Text;
        ucCapacitacion.Titulo= BiFactory.Sol.Descripcion;
        CargaInfoSolicitud();
        ucCapacitacion.Numero = BiFactory.Sol.Id_Solicitud.ToString();
        ucCapacitacion.Duracion = Horas;
        ucCapacitacion.Nivel = cmbNivelCapacitacion.SelectedValue;
        ucCapacitacion.AreaEstudios = txtAreaEstudios.Text;
        ucCapacitacion.Instructor = txtInstructor.Text;
        ucCapacitacion.EntidadEducativa = txtEntidad.Text;
        ucCapacitacion.PuntuacionExamen = txtPuntuacionExamen.Text;
        ucCapacitacion.Sol = BiFactory.Sol;
        ucCapacitacion.Visible = true;
        ucCapacitacion.FillGridHoras = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(BiFactory.Sol.Id_Solicitud, Persona.IdEmpleados);
        ucCapacitacion.MostrarEnsolicitud = true;
        //btnVisualizar.Visible = false;
        btnConfirmar.Visible = true;



        
    }

    public void CargaInfoSolicitud()
    {
        
        Horas = Solicitud.Get_SumaHoras_X_Persona_X_Solicitud(BiFactory.Sol.Id_Solicitud, Persona.IdEmpleados).ToString();
    }

    protected void cvValidaFechas_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;
        DateTime fecha_a_Cargar = DateTime.Parse(txtHorasPersonalDia.Text);
        args.IsValid = AntaresHelper.EsCorrecta_Fecha_a_Cargar(fecha_a_Cargar);

        if (args.IsValid)
        {
            fechainicio = Solicitud.Get_PrimerDiaCargado_X_Solicitud(BiFactory.Sol.Id_Solicitud, Persona.IdEmpleados);

            if (fechainicio == DateTime.MinValue)
            {
                //esto significa que no hay ninguna fecha cargada
                //entonces seteo las fechas inicio y fin
                fechainicio = AntaresHelper.PrimerDiaSemana(fecha_a_Cargar);
                fechafin = AntaresHelper.UltimoDiaSemana(fecha_a_Cargar);
            }
            else
            {
                //esto significa que habia una sol cargada, entonces le seteo la fecha fin maxima
                fechafin = AntaresHelper.UltimoDiaSemana(fechainicio);
            }
            if (fechafin > DateTime.Today)
            {
                fechafin = DateTime.Today;
            }

            if (fecha_a_Cargar > fechafin)
            {

                args.IsValid = false;
                cvValidaFechas.ErrorMessage = "Solo puede cargar horas entre los dias " + fechainicio.ToString("dd/MM/yyyy") + " y " + fechafin.ToString("dd/MM/yyyy");
            }

            if (AntaresHelper.Semana(fecha_a_Cargar) != AntaresHelper.Semana(fechainicio))
            {
                args.IsValid = false;
                cvValidaFechas.ErrorMessage = "Solo puede cargar horas entre los dias " + fechainicio.ToString("dd/MM/yyyy") + " y " + fechafin.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            args.IsValid = false;
            cvValidaFechas.ErrorMessage = "Solo puede cargar horas en la semana anterior on en la semana actual hasta el dia de hoy";

        }

    }

    protected void cvValidaHorasCargadas_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!Solicitud.TieneHorasCargadasPersonal_En_Solicitud(BiFactory.Sol.Id_Solicitud))
        {
            args.IsValid = false;
            cvValidaHorasCargadas.ErrorMessage = "No se han cargado horas en la Solicitud";
        }
        else
        {
            args.IsValid = true;
        }

    } 
    
   
}
