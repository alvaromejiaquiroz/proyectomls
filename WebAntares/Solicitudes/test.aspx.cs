using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Globalization;
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

public partial class Solicitudes_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fecha = "30/10/2009";
        CultureInfo nfo = new CultureInfo("es-ES");
        DateTime date = DateTime.Parse(fecha, nfo);

        CultureInfo nfo2 = new CultureInfo("en-US");
        Console.WriteLine(date.ToString(nfo2));

        Label1.Text = date.ToString("yyyyMMdd");
        if (!Page.IsPostBack)
        {
           cargamenu();
             CargaMenu2();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Tabs.ActiveTab.TabIndex <= Tabs.Tabs.Count)
        {
            Tabs.ActiveTab =Tabs.Tabs[ Tabs.ActiveTab.TabIndex + 1];

        }
        else
        {
            Tabs.ActiveTab = Tabs.Tabs[1]   ;
        }


    }
    protected void m_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void Menu1_MenuItemDataBound(object sender, MenuEventArgs e)
    {
        
    }
    protected void cargamenu()
    {
        
        foreach (SiteMapNode adminNode in SiteMap.RootNode.ChildNodes)
        {
            if (adminNode.Roles.Count > 0)
            {

                if (adminNode.Roles.Contains(BiFactory.User.IdPerfil.ToString()))
                    if (true)
                    {
                       
                        MenuItem mi = new MenuItem(adminNode.Title, adminNode.Title);
                        if (adminNode.HasChildNodes)
                        {
                            agregaHijo(mi,adminNode.Title,adminNode.Url);  
                            //
                            foreach (SiteMapNode hijos in adminNode.ChildNodes)
                            {
                                agregaNodo(hijos,mi);  
                            }
                        }
                        
                        Menu1.Items.Add(mi);
                                              
                    }
            }
        }

        
        Menu1.Visible = true;
        Menu1.Enabled = true;
        Menu1.DataBind();
    }
    private void agregaNodo(SiteMapNode nodo , MenuItem m)
    {
        
        foreach (SiteMapNode n in nodo.ChildNodes)
        {
            if (n.ChildNodes.Count > 0)
            {
                agregaHijo(m ,n.Title, n.Url);
                //HtmlGenericControl uChi = new HtmlGenericControl("ul");
                //uChi.Attributes.Add("id", "uno");
                //uParent.Controls.Add(uChi);
                //  agregaHijo(m, n.Title, n.Url);
            }
            //else { agregaHijo(m, n.Title, n.Url); ; }
        }
    }
    private void agregaHijo(MenuItem m ,string titulo, string url)
    {
        MenuItem hi = new MenuItem(titulo, titulo, "", url);
        m.ChildItems.Add(hi);
    }

    protected void CargaMenu2()
    {
        foreach (SiteMapNode cNode in SiteMap.RootNode.ChildNodes)
        {
            walkTree(cNode);
        }
    }
    protected void walkTree(SiteMapNode pNode)
    {
        foreach (SiteMapNode child in pNode.ChildNodes)
        {
            if ((child.Title.Length > 0) && (child.Url.Length > 0))
            {
                Response.Write(child.Title.ToString() + " -- " + child.Url.ToString());

            }
        }
    }

}
