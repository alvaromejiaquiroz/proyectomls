using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

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
}
