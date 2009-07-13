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

public partial class site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession || BiFactory.User ==null)
        {
            if (Request.RawUrl.ToLower().IndexOf("login.aspx") == -1)
                Response.Redirect(FormsAuthentication.LoginUrl);
        }
        if (!Page.IsCallback)
            if (Context.User.Identity.IsAuthenticated) { BindMenu(); };
    }
    private void BindMenu()
    {
        //// .Providers["SecuritySiteMap"].RootNode
        
        //foreach (SiteMapNode adminNode in SiteMap.RootNode.ChildNodes)
        //{
        //    if (adminNode.Roles.Count > 0)            {

        //        if (adminNode.Roles.Contains(biFactory.User().IdPerfil.ToString()))
        //        if (true)
        //        {
        //            HtmlGenericControl uMenu = new HtmlGenericControl("ul");
        //            uMenu.Attributes.Add("id", "raiz");
        //            menu.Controls.Add(uMenu);
        //            AddItem(adminNode.Title + "[" + biFactory.User().IdPerfil + "]", adminNode.Url, uMenu);
        //            HtmlGenericControl uChi = new HtmlGenericControl("ul");
        //            uChi.Attributes.Add("id", "uno");
        //            uMenu.Controls.Add(uChi);
        //            //ulMenu.Controls.Add(uChi);
        //            AddChilds(adminNode, uChi);
                    
        //        }
        //    }
        //}

        //// if (!Request.RawUrl.ToUpperInvariant().Contains("/ADMIN/"))
        //     AddItem(Resources.labels.changePassword, Utils.RelativeWebRoot + "login.aspx");
    }

    private void AddChilds(SiteMapNode adminNode, HtmlGenericControl uParent)
    {
        foreach (SiteMapNode n in adminNode.ChildNodes)
        {
            if (n.ChildNodes.Count > 0)
            {
                AddItem(n.Title, n.Url, uParent);
                HtmlGenericControl uChi = new HtmlGenericControl("ul");
                uChi.Attributes.Add("id", "uno");
                uParent.Controls.Add(uChi);
                AddChilds(n, uChi);
            }
            else { AddItem(n.Title, n.Url, uParent); }
        }
    }

    public void AddItem(string text, string url, HtmlGenericControl uParent)
    {
        HtmlAnchor a = new HtmlAnchor();
        a.InnerHtml =   text ;
        a.HRef = url;

        HtmlGenericControl li = new HtmlGenericControl("li");
        li.Controls.Add(a);
        uParent.Controls.Add(li);
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
