using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Errores_MostrarError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Mostrar();


    }
    protected void Mostrar()
    {
        //  string mensaje = Session["LastException"].ToString();
        string mensaje = Request.QueryString["msg"].ToString();
        lblError.Text = mensaje;
        //lblError.Text = Request.QueryString["msg"].ToString();
        Server.ClearError();
    }

}
