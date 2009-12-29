using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Net.Mail;
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
using System.Text.RegularExpressions;

public partial class Solicitudes_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //int a = 0;
        //int b;
        //b = 100 / a;


        //Solicitud S = Solicitud.FindFirst(Expression.IdEq(868));
        //ucGastos.Sol = S;
        //ucGastos.MuestraGastos();

        //DateTime Hoy = DateTime.Today;
        ////txtFin.Text = Hoy.AddDays(7 - int.Parse(Hoy.DayOfWeek));
        //DayOfWeek d = DateTime.Now.DayOfWeek;

        //lbl.Text = DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek.ToString() + " " + DayOfWeek.Saturday;

//        FillGrid();
        

        //lbl.Text = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)System.DateTime.Now.DayOfWeek] + " " +            System.DateTime.Now.DayOfWeek.ToString() + " " + CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(DateTime.Now).ToString(); 
        

    }

    protected void DiaSemana(DateTime fecha)
    {
       // fecha.DayOfWeek;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //if (Tabs.ActiveTab.TabIndex <= Tabs.Tabs.Count)
        //{
        //    Tabs.ActiveTab =Tabs.Tabs[ Tabs.ActiveTab.TabIndex + 1];

        //}
        //else
        //{
        //    Tabs.ActiveTab = Tabs.Tabs[1]   ;
        //}


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
        //Menu1.DataBind();
    }

    private SiteMapDataSource GetSiteMapDataSource()
    {
        XmlSiteMapProvider xmlSiteMap = new XmlSiteMapProvider();
        System.Collections.Specialized.NameValueCollection
               myCollection = new
               System.Collections.Specialized.NameValueCollection(1);
        myCollection.Add("siteMapFile", "Web.sitemap");
        xmlSiteMap.Initialize("provider", myCollection);
        xmlSiteMap.BuildSiteMap();
        SiteMapDataSource siteMap = new SiteMapDataSource();
        siteMap.ShowStartingNode = false;
        return siteMap;
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

    protected void Button4_Click(object sender, EventArgs e)
    {

    }
   
    protected void btnSend_Click(object sender, EventArgs e)
    {
        //Response.Write( WebAntares.AntaresHelper.EnviaMail("Matias"));
        //WebAntares.AntaresHelper.NotificaSolicitud(0);
        //WebAntares.AntaresHelper.Loguea_Evento("hola");
        //throw (new ArgumentNullException());
        AntaresHelper.SQLSendMail_a_Lista("minga","prueba de correo usando el db_mail","dbmail testing desde asp.net");
        

        
    
    }
    
    protected void FillGrid()
    {
        gvTest.DataSource = CustomDAL.ExecQuery();
        gvTest.DataBind();
        //NHibernate.SqlCommand.WhereBuilder whe;

        //string notin = " apellido not in (select nombre from silvia)";
        //gvTest.DataSource = Personal.FindAll(Expression.Sql(notin));

    }

    protected void gvTest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell cell = e.Row.Cells[1];
            string texto_celda = cell.Text;
            Style estilo = new Style();
            //string a = "cilia"; //  ACA IRIA TU TEXTBOX
            string a = txttexto.Text;
            string[] miarray = Regex.Split(a, " ");
            foreach (string item in miarray)
            {
                if (cell.Text.ToLower().Contains(item.ToLower()))
                {   
                    texto_celda = Regex.Replace(cell.Text, item, "<span style=\"color:Red\">" + item + "</span>").ToString();
                }
            }
            cell.Text = texto_celda;

        }

    }
   
    protected void btnBuscaTexto_Click(object sender, EventArgs e)
    {
        FillGrid();        
    }
}
