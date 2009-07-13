using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antares.model;

public delegate void cboSelected(string aselec);

public partial class Controles_cboEmpresas : System.Web.UI.UserControl
{
    public event cboSelected seleciona;   

    public string value
    {
        get { return ddlGeneric.SelectedValue; }
        set{
            if (ddlGeneric.Items.FindByValue(value) != null)
            {
                ddlGeneric.SelectedValue = value;            
                
            }
        }
    }

    public void Clear()
    {
        ddlGeneric.SelectedIndex = 0;
    }
    public bool Enabled
    {
        get { return ddlGeneric.Enabled; }
        set { ddlGeneric.Enabled = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rendercbo();
        }
    }


    public virtual void rendercbo()
    {
        ddlGeneric.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Sitios t in Sitios.FindAll())
        {
            ddlGeneric.Items.Add(new ListItem(t.Descripcion, t.IdSitio.ToString()));
        }
    }
    protected void ddlGeneric_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (seleciona != null)
        { // El evento se lanza como cualquier delegado
                   seleciona.Invoke("select");
        }
    }
  
}
