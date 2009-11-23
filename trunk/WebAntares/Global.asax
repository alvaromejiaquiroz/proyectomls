<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta al iniciarse la aplicación
        WebAntares.BiFactory.initActiveRecord();
        
       // System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday;
       
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Código que se ejecuta cuando se cierra la aplicación

    }
        
    void Application_Error_matias(object sender, EventArgs e) 
    { 
        
        
        HttpContext ctx = HttpContext.Current;
        if (ctx != null)
        {
            Exception exception = ctx.Server.GetLastError();
            Exception e1 = exception.InnerException;
            int i = 0;
            string httpPathRoot = ctx.Request.ApplicationPath;

            

            System.Web.UI.WebControls.Table myTablaBtn = new Table();
            System.Web.UI.WebControls.Table myTabla1 = new Table();
            System.Web.UI.WebControls.Table myTabla = new Table();
            System.Web.UI.WebControls.Table myTablaDeta = new Table();

            
            myTabla1.ID = "MsgError";
            myTabla1.Style.Add("text-align", "center");
            myTabla1.Style.Add("font-family", "verdana");
            myTabla1.Style.Add("font-size", "12px");
            myTabla1.Style.Add("color", "#666666");
            myTabla1.Style.Add("width", "100%");
            myTabla1.Style.Add("height", "97%");
            myTabla1.Style.Add("background-position", "10% 45%");
            myTabla1.Style.Add("display", "");

            myTabla1.Rows.Add(new TableRow());
            myTabla1.Rows[0].Cells.Add(new TableCell());
            myTabla1.Rows[0].Style.Add("font-weight", "bold");
            myTabla1.Rows[0].Cells[0].Text = "Disculpe las molestias, existe un problema en la pagina.";

            myTabla1.Rows.Add(new TableRow());
            myTabla1.Rows[1].Cells.Add(new TableCell());
            myTabla1.Rows[1].Style.Add("text-align", "center");
            myTabla1.Rows[1].Cells[0].Text = exception.Message;
            if (e1 != null)
            {
                myTabla1.Rows.Add(new TableRow());
                myTabla1.Rows[1].Cells.Add(new TableCell());
                myTabla1.Rows[1].Style.Add("text-align", "center");
                myTabla1.Rows[1].Cells[0].Text = e1.Message.ToString();
            }

            System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite1 = new System.Web.UI.HtmlTextWriter(stringWrite1);
            myTabla1.RenderControl(htmlWrite1);

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            myTabla.RenderControl(htmlWrite);

            ctx.Response.Write( stringWrite1.ToString() + stringWrite.ToString());


           ////  --------------------------------------------------
            ////To let the page finish running we clear the error
            ////--------------------------------------------------
            ctx.Server.ClearError();
  
            }
 
    }


    void Application_Error_X(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError().GetBaseException();
        // You can perform logging here // Store it in the session
        string msg  = ex.Message;
        Server.Transfer("~/Errores/MostrarError.aspx?msg="+ Server.UrlEncode(msg));


    }
    
    void Application_Error(object sender, EventArgs e)
    {
        HttpContext ctx = HttpContext.Current;
        if (ctx != null)
        {
            Exception exception = ctx.Server.GetLastError();
            Exception e1 = exception.InnerException;
            int i = 0;
            string httpPathRoot = ctx.Request.ApplicationPath;



            System.Web.UI.WebControls.Table myTablaBtn = new Table();
            System.Web.UI.WebControls.Table myTabla1 = new Table();
            System.Web.UI.WebControls.Table myTabla = new Table();
            System.Web.UI.WebControls.Table myTablaDeta = new Table();

            System.Web.UI.WebControls.Panel myPanelSend = new Panel();
            myPanelSend.ID = "PanelSendMail";
            myPanelSend.ScrollBars = System.Web.UI.WebControls.ScrollBars.None;
            myPanelSend.Style.Add("width", "100%");
            myPanelSend.Style.Add("height", "0px");
            myPanelSend.Style.Add("display", "");

            #region tabla mail
            myTablaBtn.ID = "ButtonError";
            myTablaBtn.Style.Add("text-align", "left");
            myTablaBtn.Style.Add("font-family", "verdana");
            myTablaBtn.Style.Add("font-size", "12px");
            myTablaBtn.Style.Add("color", "#666666");
            myTablaBtn.Style.Add("width", "100%");
            myTablaBtn.Style.Add("height", "30px");
            myTablaBtn.Style.Add("top", "0px");
            myTablaBtn.Style.Add("display", "");

            myTablaBtn.Rows.Add(new TableRow());
            myTablaBtn.Rows[0].Cells.Add(new TableCell());
            myTablaBtn.Rows[0].Cells[0].Style.Add("width", "25px");
            myTablaBtn.Rows[0].Cells[0].Text = "<img src='" + httpPathRoot + "/Images/LogoAntaresThumbnail.png' cursor=\"hand\">";
            myTablaBtn.Rows[0].Cells.Add(new TableCell());
            myTablaBtn.Rows[0].Cells[1].Style.Add("width", "150px");
            myTablaBtn.Rows[0].Cells[1].Text = "Reportar error.";
            myTablaBtn.Rows[0].Cells.Add(new TableCell());
            myTablaBtn.Rows[0].Cells[2].Style.Add("width", "100%");
            myTablaBtn.Rows[0].Cells[2].Style.Add("vertical-aling", "top");
            myTablaBtn.Rows[0].Cells[2].Controls.Add(myPanelSend);

            System.IO.StringWriter stringWriteBtn = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWriteBtn = new System.Web.UI.HtmlTextWriter(stringWriteBtn);
            myTablaBtn.RenderControl(htmlWriteBtn);
            #endregion

            myTabla1.ID = "MsgError";
            myTabla1.Style.Add("text-align", "center");
            myTabla1.Style.Add("font-family", "verdana");
            myTabla1.Style.Add("font-size", "12px");
            myTabla1.Style.Add("color", "#666666");
            myTabla1.Style.Add("width", "100%");
            myTabla1.Style.Add("height", "97%");
            myTabla1.Style.Add("BACKGROUND", "url('" + httpPathRoot + "/Images/ico_alerta_gr.gif') no-repeat");
            myTabla1.Style.Add("background-position", "10% 45%");
            myTabla1.Style.Add("display", "");

            myTabla1.Rows.Add(new TableRow());
            myTabla1.Rows[0].Cells.Add(new TableCell());
            myTabla1.Rows[0].Style.Add("font-weight", "bold");
            myTabla1.Rows[0].Cells[0].Text = "Disculpe las molestias, existe un problema en la pagina.";

            myTabla1.Rows.Add(new TableRow());
            myTabla1.Rows[1].Cells.Add(new TableCell());
            myTabla1.Rows[1].Style.Add("text-align", "right");
            myTabla1.Rows[1].Style.Add("height", "50px");
            myTabla1.Rows[1].Cells[0].Text = "<a href=\"#\" onclick=\"OcultarMostrarTable('MsgDetalleError','MsgError');\" " +
                                             " style=\"color:#666666;\">Ver detalles</a>";

            System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite1 = new System.Web.UI.HtmlTextWriter(stringWrite1);
            myTabla1.RenderControl(htmlWrite1);

            myTablaDeta.ID = "MsgDetalle";
            myTablaDeta.Style.Add("text-align", "left");
            myTablaDeta.Style.Add("font-family", "verdana");
            myTablaDeta.Style.Add("font-size", "8pt");
            myTablaDeta.Style.Add("width", "100%");
            myTablaDeta.Style.Add("height", "97%");
            myTablaDeta.Style.Add("cellspacing", "1px");
            myTablaDeta.Style.Add("cellpadding", "1px");
            myTablaDeta.Style.Add("display", "");
            myTablaDeta.Style.Add("border", "#e0e0e0 1px solid");

            i = 0;
            if (ctx.User != null)
            {
                myTablaDeta.Rows.Add(new TableRow());
                myTablaDeta.Rows[i].Style.Add("background-color", "#e0e0e0");
                myTablaDeta.Rows[i].Cells.Add(new TableCell());
                myTablaDeta.Rows[i].Cells[0].Text = "Usuario";
                myTablaDeta.Rows[i].Cells.Add(new TableCell());
                myTablaDeta.Rows[i].Cells[1].Text = ctx.User.Identity.Name.ToString();

            }
            else
            {
                i = -1;
            }
            i++;
            myTablaDeta.Rows.Add(new TableRow());
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[0].Text = "IP";
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[1].Text = ctx.Request.ServerVariables.Get(32).ToString();

            i++;
            myTablaDeta.Rows.Add(new TableRow());
            myTablaDeta.Rows[i].Style.Add("background-color", "#e0e0e0");
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[0].Text = "Fecha y hora";
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[1].Text = DateTime.Now.ToString();

            i++;
            myTablaDeta.Rows.Add(new TableRow());
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[0].Text = "Inner Exception";
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[1].Text = (e1 == null ? "null" : e1.ToString());

            i++;
            myTablaDeta.Rows.Add(new TableRow());
            myTablaDeta.Rows[i].Style.Add("background-color", "#e0e0e0");
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[0].Text = "Offending URL";
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[1].Text = ctx.Request.Url.ToString();

            i++;
            myTablaDeta.Rows.Add(new TableRow());
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[0].Text = "Source";
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[1].Text = exception.Source.ToString();

            i++;
            myTablaDeta.Rows.Add(new TableRow());
            myTablaDeta.Rows[i].Style.Add("background-color", "#e0e0e0");
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[0].Text = "Message";
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[1].Text = exception.Message;

            i++;
            myTablaDeta.Rows.Add(new TableRow());
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[0].Text = "Stack trace";
            myTablaDeta.Rows[i].Cells.Add(new TableCell());
            myTablaDeta.Rows[i].Cells[1].Text = exception.StackTrace;

            for (int j = 0; j < i; j++)
            {
                myTablaDeta.Rows[j].Cells[0].Style.Add("width", "100px");
                myTablaDeta.Rows[j].Cells[0].Style.Add("border-RIGHT", "1px solid #e0e0e0");
                myTablaDeta.Rows[j].Cells[1].Style.Add("width", "400px");
            }

            System.Web.UI.WebControls.Panel myPanel = new Panel();
            myPanel.ScrollBars = System.Web.UI.WebControls.ScrollBars.Auto;
            myPanel.ID = "PanelMsgError";
            myPanel.Style.Add("SCROLLBAR-FACE-COLOR", "#e0e0e0");
            myPanel.Style.Add("SCROLLBAR-SHADOW-COLOR", "#c0c0c0");
            myPanel.Style.Add("SCROLLBAR-HIGHLIGHT-COLOR", "#e0e0e0");
            myPanel.Style.Add("SCROLLBAR-3DLIGHT-COLOR", "#c0c0c0");
            myPanel.Style.Add("SCROLLBAR-DARKSHADOW-COLOR", "#e0e0e0");
            myPanel.Style.Add("SCROLLBAR-ARROW-COLOR", "#black");
            myPanel.Style.Add("SCROLLBAR-TRACK-COLOR", "#f0f0f0");
            myPanel.Style.Add("width", "550px");
            myPanel.Style.Add("height", "400px");
            myPanel.Controls.Add(myTablaDeta);

            myTabla.ID = "MsgDetalleError";
            myTabla.Style.Add("text-align", "left");
            myTabla.Style.Add("font-family", "verdana");
            myTabla.Style.Add("font-size", "12px");
            myTabla.Style.Add("width", "100%");
            myTabla.Style.Add("height", "97%");
            myTabla.Style.Add("cellspacing", "2px");
            myTabla.Style.Add("cellpadding", "2px");
            myTabla.Style.Add("display", "none");

            myTabla.Rows.Add(new TableRow());
            myTabla.Rows[0].Cells.Add(new TableCell());
            myTabla.Rows[0].Style.Add("valign", "top");
            myTabla.Rows[0].Cells[0].Controls.Add(myPanel);

            myTabla.Rows.Add(new TableRow());
            myTabla.Rows[1].Style.Add("text-align", "right");
            myTabla.Rows[1].Style.Add("height", "50px");
            myTabla.Rows[1].Cells.Add(new TableCell());
            myTabla.Rows[1].Cells[0].Text = "<a href=\"#\" onclick=\"OcultarMostrarTable('MsgError','MsgDetalleError');\" " +
                                             " style=\"color:#666666;\">Ocultar detalle</a>";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            myTabla.RenderControl(htmlWrite);

            string js = "<script type=\"text/javascript\" language=\"javascript\"> \n" +
                " try {parent.ShowContentFrame();}catch(e){}\n" +
                " function OcultarMostrarTable (mostrarTableID, ocultarTableID){ \n" +
                "   document.getElementById(mostrarTableID).style.display = ''; \n" +
                "   document.getElementById(ocultarTableID).style.display = 'none'; \n" +
                " }\n \n" +
                " function SendMail(){ " +
                " document.getElementById('PanelSendMail').style.top = '0px'; " +
                " document.getElementById('PanelSendMail').style.height = '30px'; " +
                " document.getElementById('PanelSendMail').innerHTML = '<iframe src=\"" + httpPathRoot + "/SendMail.aspx\" id=\"iframeSendMail\" height =\"30px\" scrolling=\"no\" border=\"0\" frameborder=\"3\" />'; " +
                "} \n" +
                "</scri" + "pt>\n";

            ctx.Response.Write(js + stringWriteBtn.ToString() + stringWrite1.ToString() + stringWrite.ToString());


            ////  --------------------------------------------------
            ////To let the page finish running we clear the error
            ////--------------------------------------------------
            ctx.Server.ClearError();

        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando se inicia una nueva sesión
		FormsAuthentication.SignOut();
        
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: El evento Session_End se desencadena sólo con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
        // o SQLServer, el evento no se genera.
        Session.Abandon();
        FormsAuthentication.SignOut();
        

    }
       
</script>
