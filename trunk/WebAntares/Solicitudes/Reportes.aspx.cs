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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using NHibernate;
using Castle.ActiveRecord;
using Antares.model;


public partial class Solicitudes_pruebacontroles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //int idSol = int.Parse(Request.QueryString["id"].ToString()) ;
        int idSol = 0;

        if (Request.QueryString["id"]  != null)
           {
               idSol = int.Parse(Request.QueryString["id"].ToString());

           }

        Solicitud sol = Solicitud.GetById(idSol);
        
        string path ;
        switch (sol.Tipo.IdTiposolicitud.ToString())
        {
            case "1":
                 path = Server.MapPath("../Reportes/Solicitud_Preventiva.rpt");
                break;
            case "2":
                 path = Server.MapPath("../Reportes/Solicitud_Correctiva.rpt");
                break;
            case "6":
                 path = Server.MapPath("../Reportes/Solicitud_Obra.rpt");
                break;
            default:
                 path = Server.MapPath("../Reportes/EnConstruccion.rpt");
                break;
        }

        ReportDocument report = new ReportDocument();
        report.Load(path);
        report.SetDatabaseLogon("app", "1234", "LOCALHOST", "WebAntares");
        
        //report.SetDatabaseLogon("sa", "123456", ".\\sqlexpress", "WebAntares");
        

        report.SetParameterValue("@idSolicitud", idSol);
        CrystalReportViewer1.ReportSource = report;
        
                
        
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}
