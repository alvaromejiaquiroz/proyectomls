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

public partial class Administracion_Pagos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CargarCombos();
    }
    public void CargarCombos()
    {
        cmbBancos.Items.Clear();
        cmbBancos.Items.Add(new ListItem("Seleccione...", "-1"));

        foreach (Antares.model.Bancos bc in Antares.model.Bancos.FindAll())
        {
            cmbBancos.Items.Add(new ListItem(bc.Nombre, bc.Id.ToString()));
        }



    }
}
