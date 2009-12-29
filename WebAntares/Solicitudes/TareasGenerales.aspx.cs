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
    static SolicitudTareasGenerales sol_Tg;
    static Antares.model.Personal _persona;
    static int IdSol;
    static string Inicio;
    static string Fin;
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
            
            if (BiFactory.Empleado.IdEmpleados > 0 )
            {

                cmpVFecha.ValueToCompare = DateTime.Today.ToString("dd/MM/yyyy");
                CargarCombos();
                FillSol();
                //CargaInfoSolicitud();
                
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
            SolicitudTareasGenerales sol_Tg = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));


            if (sol_Tg == null)
            {
                sol_Tg = new SolicitudTareasGenerales();
                sol_Tg.IdSolicitud = Sol.Id_Solicitud;
                sol_Tg.Descripcion = Sol.Descripcion;
            }

            sol_Tg.FechaInicio = AntaresHelper.FechaNula();
            sol_Tg.FechaFin = AntaresHelper.FechaNula();
            sol_Tg.Duracion = 0;
            sol_Tg.IdTipotarea = int.Parse(cmbTipoTarea.SelectedValue);
            sol_Tg.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            sol_Tg.SaveAndFlush();
            DeshabilitaPanelPrincipal();
            //Sol = null;
        }
    }

    private void FillSol()
    {
        if (Request.QueryString["Id"] != null)
        {
            IdSol = int.Parse(Request.QueryString["Id"].ToString());
            BiFactory.Sol = Solicitud.GetById(IdSol);

        }

        SolicitudTareasGenerales sol_Tg = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        if (sol_Tg != null)
        {
            cmbTipoTarea.SelectedValue = sol_Tg.IdTipotarea.ToString();
            txtDescripcion.Text = sol_Tg.Descripcion;
            Persona = Personal.GetById(sol_Tg.IdEmpleado.ToString());
            FillHorasPersonalGrid(0);
            MostrarSolicitud();

        }
        else
        {
            Persona = Personal.GetById(BiFactory.Empleado.IdEmpleados.ToString());
        }
        litHorasPersonalPersona.Text = Persona.Apellido + "," + Persona.Nombres;
        
        
    }
   
    private void CargarCombos()
    {
        foreach (Antares.model.TipoTarea Tg in Antares.model.TipoTarea.FindAll())
        {
            cmbTipoTarea.Items.Add(new ListItem(Tg.Descripcion, Tg.Id.ToString()));
        }
        if (sol_Tg != null)
        {
            if (cmbTipoTarea.Items.FindByValue(sol_Tg.IdTipotarea.ToString()) != null)
            {
                cmbTipoTarea.SelectedItem.Value = sol_Tg.IdTipotarea.ToString();
            }
        }
      
    }

    protected void btnComenzarCarga_Click(object sender, EventArgs e)
    {

        if (IsValid)
        {
            Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            SolicitudTareasGenerales sol_Tg = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));


            if (sol_Tg == null)
            {
                sol_Tg = new SolicitudTareasGenerales();
                sol_Tg.IdSolicitud = Sol.Id_Solicitud;
                sol_Tg.Descripcion = Sol.Descripcion;
            }

            sol_Tg.FechaInicio = AntaresHelper.FechaNula();
            sol_Tg.FechaFin = AntaresHelper.FechaNula();
            sol_Tg.Duracion = 0;
            sol_Tg.IdTipotarea = int.Parse(cmbTipoTarea.SelectedValue);
            sol_Tg.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            sol_Tg.SaveAndFlush();

            txtHorasPersonalDia.Text = string.Empty;
            ddlHorasPersonalHoras.SelectedIndex = 0;


            DateTime fechaInicio = Sol.FechaCreacion;
            DateTime fechaFin = DateTime.MinValue;

            this.Persona = Personal.GetById(sol_Tg.IdEmpleado.ToString());
            litHorasPersonalPersona.Text = Persona.Apellido + "," + Persona.Nombres;
            cmpVFecha.ValueToCompare = DateTime.Today.ToString("dd/MM/yyyy");
            FillHorasPersonalGrid(0);
            


        }
    }

    protected void btnFinalizarCarga_Click(object sender, EventArgs e)
    {

        MostrarSolicitud();
        
    }

    protected void DeshabilitaPanelPrincipal()
    {
        txtDescripcion.Enabled = false;
        cmbTipoTarea.Enabled = false;
        
    }

    protected void HabilitaPanelPrincipal()
    {
        txtDescripcion.Text = string.Empty;
        cmbTipoTarea.Enabled = true;
        BiFactory.Sol = null;
    }

    #region Horas del PErsonal

    protected void LimpiaFormulario()
    {
        txtHorasPersonalDescripcion.Text = string.Empty;
        txtHorasPersonalDia.Text = string.Empty;
        ddlHorasPersonalHoras.SelectedIndex = 0;

    }

    protected void gvHorasPersonal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillHorasPersonalGrid(e.NewPageIndex);

    }

    private void FillHorasPersonalGrid(int pageIndex)
    {
        DbDataReader reader = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(BiFactory.Sol.Id_Solicitud, Persona.IdEmpleados);

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
        vsHorasPersonal.Enabled = true;
        if (IsValid)
        {
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
        CargaInfoSolicitud();

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
        Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        SolicitudTareasGenerales sol_Tg = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        if (sol_Tg != null)
        {
            sol_Tg.Delete();
            foreach(SolicitudRendicionPersonalHoras hs in SolicitudRendicionPersonalHoras.FindAll(Expression.Eq("IdSolicitud",Sol.Id_Solicitud)))
            {
                hs.Delete();
            }
            
        }

        Response.Redirect("~/solicitudes/listatareasgenerales.aspx");
    }
    
    protected void btnVisualizar_Click(object sender, EventArgs e)
    {
       
            ucTareasGenerales.MostrarEnsolicitud = true;
            MostrarSolicitud();
            //Response.Write("<script>");
            //Response.Write("window.open('visualizarsolicitud.aspx?id="+BiFactory.Sol.Id_Solicitud.ToString() +" ','_blank')");
            //Response.Write("</script>");     
        

    }
    
    protected void MostrarSolicitud()
    {
        ucTareasGenerales.Tipo = cmbTipoTarea.SelectedItem.Text;
        ucTareasGenerales.Titulo = BiFactory.Sol.Descripcion;
        CargaInfoSolicitud();
        ucTareasGenerales.Numero = BiFactory.Sol.Id_Solicitud.ToString();
        ucTareasGenerales.Duracion = Horas;
        ucTareasGenerales.Descripcion = txtDescripcion.Text;
        ucTareasGenerales.FillGridHoras = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(BiFactory.Sol.Id_Solicitud,Persona.IdEmpleados);
        ucTareasGenerales.MostrarEnsolicitud = true;
        ucTareasGenerales.Visible = true;
        btnVisualizar.Visible = false;
        btnConfirmar.Visible = true;
    }

    public void CargaInfoSolicitud()
    {
        Horas = Solicitud.Get_SumaHoras_X_Persona_X_Solicitud(BiFactory.Sol.Id_Solicitud, Persona.IdEmpleados).ToString();
    }

    protected void btnConfirmarCarga_Click(object sender, EventArgs e)
    {

        if (IsValid)
        {
            Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            SolicitudTareasGenerales sol_Tg = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));


            if (sol_Tg == null)
            {
                sol_Tg = new SolicitudTareasGenerales();
                sol_Tg.IdSolicitud = Sol.Id_Solicitud;
                sol_Tg.Descripcion = Sol.Descripcion;
            }

            sol_Tg.FechaInicio = AntaresHelper.FechaNula();
            sol_Tg.FechaFin = AntaresHelper.FechaNula();
            sol_Tg.Duracion = 0;
            sol_Tg.IdTipotarea = int.Parse(cmbTipoTarea.SelectedValue);
            sol_Tg.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            sol_Tg.SaveAndFlush();

            txtHorasPersonalDia.Text = string.Empty;
            ddlHorasPersonalHoras.SelectedIndex = 0;


            DateTime fechaInicio = Sol.FechaCreacion;
            DateTime fechaFin = DateTime.MinValue;

            Session["mensaje"] = "La solicitud " + BiFactory.Sol.Id_Solicitud.ToString()+ " Ha sido creada con Exito";
            Response.Redirect("~/default.aspx");

        }
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
