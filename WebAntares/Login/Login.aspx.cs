using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebAntares;


public partial class Login_Login1t : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     //   lblPie.Text = (Context.Server.MachineName);
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {

            
            if (BiFactory.CheckLogin(Login1.UserName, Login1.Password))
            {
                FormsAuthentication.SetAuthCookie(BiFactory.User.Nombre, true);
                Logger.Log(TipoEvento.Login,"Inició Session");
                Response.Redirect("../default.aspx");

            }
            else
            {
                Session["user"] = null;
              //  Logger.Log(TipoEvento.Login, Login1.UserName.ToString() + " Intento Conectarse al Sistema");
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ("Error" + ex.Message.ToString());
            Login1.InstructionText = "Usuario invalido";
        }

    }
}
