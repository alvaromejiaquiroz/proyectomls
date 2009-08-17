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

public partial class Controles_Capacitacion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

    public bool Imprimible
    {
        set
        {
            btnFinalizar.Visible = !value;
            pnlImprimir.Visible = value;
            imgAntares.Visible = value;
        }
    }
}
