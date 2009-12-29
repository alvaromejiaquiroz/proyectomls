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
using System.IO;

public partial class site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string appPath; 
        
        if (Session.IsNewSession || BiFactory.User == null)
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

                if (Relacion != null && Relacion.IdEmpleados > 0)
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

                if(!File.Exists( Server.MapPath(Imagen_Usuario.ImageUrl)))
                {
                    Imagen_Usuario.ImageUrl = "";
                }
                
                //BindMenu();
                
            };
        }

        if (!IsPostBack)
        {
            LoadNodos();
        }

        CargaInfoSistema();
    }
        
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
        Logger.Log(TipoEvento.Login, "Termino Session");

        Session.Abandon();
        FormsAuthentication.SignOut();
    }

    private void CargaInfoSistema()
    {
        VersionSistema.Text = AntaresHelper.Get_Config_VersionSistema();
        NombreSistema.Text = AntaresHelper.Get_Config_NombreSistema();
    }
}
