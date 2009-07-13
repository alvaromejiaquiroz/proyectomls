using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Controles_FechayHora : System.Web.UI.UserControl
{
    public DateTime Fecha
    {
        get
        {
            DateTime dt;

            if (DateTime.TryParse(Cal1.Text, out dt))
            {
                return dt;
            }
            else
            {
                return DateTime.Now;
            }
        }

    }
    public string Text
    {
        get { return Cal1.Text; }
        set { Cal1.Text = value; }
    }
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    private string _customScript;

    public string CustomScript
    {
        get { return _customScript; }
        set { _customScript = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CargarCombos();
        RegistrarScript();
        
    }

    protected void CargarCombos()
    {
        Cal1.Text = DateTime.Now.Date.ToString();
        Cal1.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        string temp;
        int i;
        for (i = 0; i <= 23; i++)
        {
            temp = i.ToString();
            if (i < 10)
            {
                temp = "0" + i.ToString();
            }

            cmbhora.Items.Add(new ListItem(temp, temp));

        }
    }

    private void RegistrarScript()
    {
        string idClt = Cal1.ClientID;

        StringBuilder sb = new StringBuilder();

        sb.Append(@"<script language='javascript'>");
        if (_name != null)
        {
            sb.Append(@"var " + _name + "='" + Cal1.ClientID + "';");
        }
        sb.Append(@"$(document).ready(function() {
            //date picker
            $('#" + Cal1.ClientID + "').datepicker($.extend({},$.datepicker.regional['es'],{");

        if (_customScript != null)
            sb.Append(@"beforeShow: " + _customScript + ",");

        sb.Append(@"showOn: 'both', buttonImage: '../images/calendar.gif',   buttonImageOnly: true })    );    });");
        sb.Append(@"</script>");

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "datePick" + Cal1.ClientID, sb.ToString());
    }

    public override void RenderControl(HtmlTextWriter writer)
    {
        RegistrarScript();
        base.RenderControl(writer);
    }

}
