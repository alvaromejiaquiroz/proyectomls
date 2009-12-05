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

public partial class Controles_Gastos : System.Web.UI.UserControl
{

    private string _modo;

    private string _descripcion;
        
    private Solicitud _solicitud;

    private Personal _persona;
    
    public Solicitud Sol 
    {
        get { return _solicitud; }
        set { _solicitud = value; }
    }

    public string Modo
    {
        get { return _modo; }
        set { _modo = value; }
    }

    public Personal Persona
    {
        get { return _persona; }
        set { _persona = value; }
    }

    public string Descripcion
    {
        get { return _descripcion; }
        set { _descripcion = value; }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            litHorasPersonalPersona.Text = Persona.Apellido + "," + Persona.Nombres;
            if (Descripcion != string.Empty)
            {
                txtHorasPersonalDescripcion.Visible = false;
                lblDes.Visible = false;
            }
            FillHorasPersonalGrid(0);
            
        }
    }

    public void MustraHorasCargadas()
    {
        FillHorasPersonalGrid(0);
    }

    private void FillHorasPersonalGrid(int pageIndex)
    {
        DbDataReader reader = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(Sol.Id_Solicitud,Persona.IdEmpleados);

        DataTable table = new DataTable();
        table.Load(reader);
        gvHorasPersonal.DataSource = table;
        gvHorasPersonal.DataKeyNames = new string[] { "Id" };
        gvHorasPersonal.PageIndex = pageIndex;
        gvHorasPersonal.DataBind();


    }

    protected void btnHorasPersonalGuardar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);
            SolicitudRendicionPersonalHoras ph = new SolicitudRendicionPersonalHoras();
            ph.IdPersona = Persona.IdEmpleados;
            ph.IdSolicitud = Sol.Id_Solicitud;
            ph.Fecha = fecha;
            ph.Horas = decimal.Parse(ddlHorasPersonalHoras.SelectedValue);

            string descripcion;

            if (txtHorasPersonalDescripcion.Text == string.Empty)
            {    
                descripcion = Descripcion;
            }
            else 
            {
                descripcion =txtHorasPersonalDescripcion.Text;
                }

            ph.Descripcion = descripcion;

            ph.SaveAndFlush();
            LimpiaFormulario();
            FillHorasPersonalGrid(0);
        }
        
    }

    public void LimpiaFormulario()
    {
        txtHorasPersonalDia.Text = string.Empty;
        txtHorasPersonalDescripcion.Text = string.Empty;
        ddlHorasPersonalHoras.SelectedIndex = 0;

    }

    protected void gvHorasPersonal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int i = int.Parse(gvHorasPersonal.DataKeys[e.RowIndex].Value.ToString());
        SolicitudRendicionPersonalHoras R = SolicitudRendicionPersonalHoras.FindFirst(Expression.Eq("Id", i));
        R.Delete();
        FillHorasPersonalGrid(0);
    }

    protected void gvHorasPersonal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillHorasPersonalGrid(e.NewPageIndex);
     
    }

    protected void cvCheckHorasRestantes_ServerValidate(object source, ServerValidateEventArgs args)
    {
         CultureInfo nfo = new CultureInfo("es-ES");
        DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text, nfo);
        //DateTime fecha = DateTime.Parse(txtHorasPersonalDia.Text);

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

    protected void btnHorasPersonalCerrar_Click(object sender, EventArgs e)
    {
        LimpiaFormulario();
        pnlHorasPersonal.Visible = false;
    }
    
    public void MuestraPanel()
    {
        pnlHorasPersonal.Visible = true;
    }

    public void OcultaPanel()
    {
        pnlHorasPersonal.Visible = false;
    }
}
