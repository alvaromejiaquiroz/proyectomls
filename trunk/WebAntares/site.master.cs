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
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using Antares.model;

public partial class site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string appPath; 
        if (Session.IsNewSession || BiFactory.User ==null)
        {
            if (Request.RawUrl.ToLower().IndexOf("login.aspx") == -1)
            {
                Response.Redirect(FormsAuthentication.LoginUrl);
            }
            
        }
        if (!Page.IsCallback)
        {
            appPath = HttpContext.Current.Request.ApplicationPath;
        
            if (Context.User.Identity.IsAuthenticated) { 
            
                UsuariosEmpleados Relacion = Antares.model.UsuariosEmpleados.FindOne(Expression.Eq("IdUsuarios", BiFactory.User.IdUsuario));
                Imagen_Usuario.ImageUrl = "~/images/Empleados/NN.jpg";

                if (Relacion != null)
                {
                    Personal Empleado = Personal.FindOne(Expression.Eq("IdEmpleados", Relacion.IdEmpleados));
                    Imagen_Usuario.ToolTip = Empleado.Apellido + "," + Empleado.Nombres;
                    if (Empleado.Foto != null )
                    {

                        Imagen_Usuario.ImageUrl = "~/images/Empleados/" + Empleado.Foto;
                    }
                }
                else
                {
                    Imagen_Usuario.ToolTip = "Este usuario no esta relacionado con ningun Empleado, Contactarse con Sistemas";
                
                }
               
                //BindMenu();
                
            };
        }

        if (!IsPostBack)
        {
            LoadNodos();
        }
    }
        
    //private void BindMenu()
    //{
    
    //    foreach (SiteMapNode adminNode in SiteMap.RootNode.ChildNodes)
    //    {
    //        if (adminNode.Roles.Count > 0)
    //        {

    //            if (adminNode.Roles.Contains(BiFactory.User.IdPerfil.ToString()))
    //                if (true)
    //                {
    //                    //HtmlGenericControl uMenu = new HtmlGenericControl("Estilo_menu");
    //                    HtmlGenericControl uMenu = new HtmlGenericControl("menu");
    //                    uMenu.Attributes.Add("id", "raiz");
    //                    menu.Controls.Add(uMenu);
                       
    //                    AddItem(adminNode.Title , adminNode.Url, uMenu);
    //                    HtmlGenericControl uChi = new HtmlGenericControl("ul");
    //                    uChi.Attributes.Add("id", "uno");
    //                    uMenu.Controls.Add(uChi);
    //                    AddChilds(adminNode, uChi);

    //                }
    //        }
    //    }
    //}

    //private void AddChilds(SiteMapNode adminNode, HtmlGenericControl uParent)
    //{
    //    foreach (SiteMapNode n in adminNode.ChildNodes)
    //    {
    //        if (n.ChildNodes.Count > 0)
    //        {
    //            AddItem(n.Title, n.Url, uParent);
    //            HtmlGenericControl uChi = new HtmlGenericControl("ul");
    //            uChi.Attributes.Add("id", "uno");
    //            uParent.Controls.Add(uChi);
    //            AddChilds(n, uChi);
    //        }
    //        else { AddItem(n.Title, n.Url, uParent); }
    //    }
    //}

    //public void AddItem(string text, string url, HtmlGenericControl uParent)
    //{
    //    HtmlAnchor a = new HtmlAnchor();
    //    a.InnerHtml =   text ;
    //    a.HRef = url;

    //    HtmlGenericControl li = new HtmlGenericControl("li");
    //    li.Controls.Add(a);
    //    uParent.Controls.Add(li);
    //}

    private void LoadNodos()
    {
        foreach (SiteMapNode adminNode in SiteMap.RootNode.ChildNodes)
        {
            if (adminNode.Roles.Contains(BiFactory.User.IdPerfil.ToString()))
            {
                MenuItem item = new MenuItem(adminNode.Title, adminNode.Title);
                ucMenu.Items.Add(item);
                foreach (SiteMapNode node in adminNode.ChildNodes)
                {
                    LoadNodo(node, item);
                }
            }
        }
    }

    private void LoadNodo(SiteMapNode node, MenuItem parent)
    {
        MenuItem item = new MenuItem(node.Title, node.Description, string.Empty, node.Url);
        parent.ChildItems.Add(item);
        foreach (SiteMapNode nodeChild in node.ChildNodes)
        {
            LoadNodo(nodeChild, item);
        }
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
