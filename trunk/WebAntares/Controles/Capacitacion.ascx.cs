using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Antares.model;
using WebAntares;
using System.Data.Common;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;



public partial class Controles_Capacitacion : System.Web.UI.UserControl
{
    private Solicitud _solicitud;
    private int _persona;

    public Solicitud Sol
    {
        get { return _solicitud; }
        set { _solicitud = value; }
    }

    public int IdEmpleado
    {
        get { return _persona; }
        set { _persona = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           // FillGridCapacitacion();
        }
    }

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListaCapacitacion.aspx");
    }

    public string Numero
    {
        set { litNumero.Text = value; }
    }

    public string Titulo
    {
        set { litTitulo.Text = value; }
    }

    public string Nivel
    {
        set { litNivel.Text = value; }
    }

    public string FechaInicio
    {
        set { litFechaInicio.Text = value; }
    }

    public string FechaFin
    {
        set { litFechaFin.Text = value; }
    }

    public string Duracion
    {
        set { litDuracion.Text = value; }
    }

    public string AreaEstudios
    {
        set { litAreaEstudios.Text = value; }
    }

    public string Instructor
    {
        set { litInstructor.Text = value; }
    }

    public string EntidadEducativa
    {
        set { litEntidadEducativa.Text = value; }
    }

    public string PuntuacionExamen
    {
        set { litPuntuacionExamen.Text = value; }
    }

    protected void FillGridCapacitacion()
    {
        DbDataReader reader = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(Sol.Id_Solicitud, this.IdEmpleado);
        DataTable table = new DataTable();
        table.Load(reader);
        gvHorasPersonal.DataSource = table;
        gvHorasPersonal.DataKeyNames = new string[] { "Id" };
        gvHorasPersonal.DataBind();
    }

    public bool Imprimible
    {
        set
        {
            btnFinalizar.Visible = !value;
            pnlImprimir.Visible = value;
            imgAntares.Visible = value;
        }
    }

    public void CargaInfoSolicitud()
    {
        DbDataReader dr = SolicitudCapacitacion.Get_TotalHoras_X_Persona_X_Solicitud(Sol.Id_Solicitud, this.IdEmpleado);
        DateTime fecha = DateTime.MinValue;
        while (dr.Read())
        {
            if (dr.HasRows)
            {

                if (dr["FechaInicio"] != System.DBNull.Value)
                {
                    fecha = DateTime.Parse(dr["FechaInicio"].ToString());
                    FechaInicio = fecha.ToString("dd/MM/yyyy");

                }

                if (dr["FechaFin"] != System.DBNull.Value)
                {
                    fecha = DateTime.Parse(dr["FechaFin"].ToString());
                    FechaFin = fecha.ToString("dd/MM/yyyy");

                }

                if (dr["Horas"] != System.DBNull.Value)
                {
                    Duracion = dr["Horas"].ToString();

                }
            }
        }

        FillGridCapacitacion();
    }
    
}
