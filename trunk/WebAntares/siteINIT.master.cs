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

public partial class site2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession || BiFactory.User ==null)
        {
            if (Request.RawUrl.ToLower().IndexOf("login.aspx") == -1)
            {
                Response.Redirect(FormsAuthentication.LoginUrl);
            }
            
        }
        //    if (!Page.IsCallback)
           // if (Context.User.Identity.IsAuthenticated) { BindMenu(); };
    }
    private void BindMenu()
    {
    
    }


    public string Translate(string text)
    {
        try
        {
            return this.GetGlobalResourceObject("labels", text).ToString();
        }
        catch (NullReferenceException)
        {
            return text;
        }
    }
    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)

    {
        
        Session.Abandon();
        FormsAuthentication.SignOut();
    }
}
