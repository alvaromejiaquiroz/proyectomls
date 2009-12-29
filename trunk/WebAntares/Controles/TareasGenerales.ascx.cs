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
using System.Data.Common;

public partial class Controles_TareasGenerales : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        VersionSistema.Text = WebAntares.AntaresHelper.Get_Config_VersionSistema();
    }

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListaTareasGenerales.aspx");
    }

    public string Numero 
    {
        set { litNumero.Text = value; }
    }

    public string Titulo
    {
        set { litTitulo.Text = value; }
    }

    public string Tipo
    {
        set { litTipo.Text = value; }
    }

    public string Duracion
    {
        set { litDuracion.Text = value; }
    }

    public string Descripcion
    {
        set { litDescripcion.Text = value; }
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

    public bool MostrarEnsolicitud
    {
        set
        {
            btnFinalizar.Visible = !value;
            pnlImprimir.Visible = !value;
            imgAntares.Visible = value;
        }
    }
   
    public DbDataReader FillGridHoras
    {
         set
        {
            DataTable table = new DataTable();
            table.Load(value);
            gvHorasPersonal.DataSource = table;
            gvHorasPersonal.DataKeyNames = new string[] { "Id" };
            gvHorasPersonal.DataBind();
        }
    }
}
