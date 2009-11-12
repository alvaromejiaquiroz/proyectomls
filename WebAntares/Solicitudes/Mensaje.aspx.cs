using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Antares.model;
using NHibernate.Expression;
using System.Diagnostics;

public partial class Solicitudes_Mensaje : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        HttpContext ctx = HttpContext.Current;
        Exception exception = ctx.Server.GetLastError();
        if (exception != null)
            {
                
                string httpPathRoot = ctx.Request.ApplicationPath;
                Response.Write("Error " + exception.Message);
                ctx.Server.ClearError();
            }

        
        
    }
}
