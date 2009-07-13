using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Controles_jDatePick : System.Web.UI.UserControl
{
    public DateTime Fecha
    {
        get
        {
            DateTime dt;

            if (DateTime.TryParse(ctlDate.Text, out dt))
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
        get { return ctlDate.Text; }
        set { ctlDate.Text = value ; }
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
        RegistrarScript();

    }

    private void RegistrarScript()
    {
        string idClt = ctlDate.ClientID;

        StringBuilder sb = new StringBuilder();

        sb.Append(@"<script language='javascript'>");
        if (_name != null)
        {
            sb.Append(@"var " + _name + "='" + ctlDate.ClientID + "';");
        }
        sb.Append(@"$(document).ready(function() {
            //date picker
            $('#" + ctlDate.ClientID + "').datepicker($.extend({},$.datepicker.regional['es'],{");

        if (_customScript != null)
            sb.Append(@"beforeShow: " + _customScript + ",");

        sb.Append(@"showOn: 'both', buttonImage: '../images/calendar.gif',   buttonImageOnly: true })    );    });");
        sb.Append(@"</script>");

        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "datePick" + ctlDate.ClientID, sb.ToString());
    }

    public override void RenderControl(HtmlTextWriter writer)
    {
        RegistrarScript();
        base.RenderControl(writer);
    }
}
