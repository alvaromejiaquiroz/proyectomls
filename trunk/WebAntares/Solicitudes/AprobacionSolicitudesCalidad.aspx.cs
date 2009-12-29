using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Antares.model;
using WebAntares;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;

public partial class Solicitudes_AprobacionSolicitudesGestion : System.Web.UI.Page
{
    string Sector = "Calidad";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (HabilitarSegunPerfil())
            {
                FillGrid(0);
            }
        }
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Solicitud S = Solicitud.FindOne(Expression.Eq("Id_Solicitud", (DataBinder.Eval(e.Row.DataItem, "Solicitud"))));
            HyperLink lnkReporte = (HyperLink)e.Row.FindControl("lnkReporte");
            HyperLink lnkSolicitud = (HyperLink)e.Row.FindControl("lnkReporte");

            Image imgStatus = (Image)e.Row.FindControl("imgEstado");
            
            lnkReporte.Visible = false;

            
            string valorEstado = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Status"));
            switch (valorEstado )
            {

                case "Anulado":
             
                        imgStatus.ImageUrl = "../images/deshabilitado.gif";
                        imgStatus.ToolTip = "Anulado";
                        break;
                    
                case "Pendiente":
                        imgStatus.ImageUrl = "../images/pendiente.gif";
                        imgStatus.ToolTip = "Pendiente";
                        break;

                case "Realizado":
                        if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id_Reporte")) != null)
                        {
                            lnkReporte.Visible = true;
                        }
                        imgStatus.ImageUrl = "../images/realizado.gif";
                        imgStatus.ToolTip = "Realizado";
                        break;
                    
                case "Reprogramado":
                        imgStatus.ImageUrl = "../images/reprogramado.gif";
                        imgStatus.ToolTip = "REPROGRAMADO: " +  S.Causa;
                        break;
                case "Cancelado":
                        imgStatus.ImageUrl = "../images/cancelado.gif";
                        imgStatus.ToolTip = "CANCELADO: " + S.Causa;
                        break;
                case "Vencido":
                        imgStatus.ImageUrl = "../images/vencido.gif";
                        imgStatus.ToolTip = "VENCIDO: se ha exedido el plazo para la realización de esta Solicitud";
                        break;
            }
            
        }
    }
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(e.NewPageIndex);
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string a = e.CommandName.ToString();

        if (!e.CommandName.Equals("Page"))
        {
            Int32 IdSolicitud = Int32.Parse(e.CommandArgument.ToString());
            
             
            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/Reportes/MostrarSolicitud.aspx?id=" + IdSolicitud.ToString());
                    break;
                case "Aprobar":
                    AprobarSolicitud(IdSolicitud,true);
                    break;
                case "Rechazar":
                    AprobarSolicitud(IdSolicitud, false);

                    break;

            }
            Response.Redirect("./AprobacionSolicitudesCalidad.aspx");
        }
    }

    protected void GridView1_Load(object sender, EventArgs e)
    {

    }

    private void FillGrid(int pageIndex)
    {
        DbDataReader reader = Antares.model.Solicitud.GetReaderSinAprobacion("Calidad");
        DataTable table = new DataTable();
        table.Load(reader);
        GridView1.DataSource = table;
        GridView1.PageIndex = pageIndex;
        GridView1.DataBind();
    }
    
    private bool HabilitarSegunPerfil()
    {
        if (AntaresHelper.GetPuedeAprobacionCalidad(BiFactory.Perfil.Detalle))
        {
            return true;
        }
        else
        {
            return false;
        }

      

    }
    
    private void AprobarSolicitud(int id, bool aprueba)
    {
        Solicitud sol = Solicitud.FindOne(Expression.Eq("Id_Solicitud", id));
        SolicitudAprobaciones apro;
        
        if (sol != null)
        {

            apro = SolicitudAprobaciones.FindFirst(Expression.Eq("IdSolicitud",sol.Id_Solicitud),Expression.Eq("Sector",Sector));

            if (apro == null)
            {
                apro = new SolicitudAprobaciones();
                apro.IdSolicitud = sol.Id_Solicitud;
                apro.Sector = Sector;

                
            }
            
            if (aprueba)
                {
                    apro.Aprobado = true;
                    apro.Save();
                }
                else
                {
                        apro.Aprobado = false;
                    Solicitud Reporte = Solicitud.FindOne(Expression.Eq("IdSolicitudInicial", sol.Id_Solicitud));
                    if (Reporte != null)
                    {
                        Reporte.Delete();

                    }

                     foreach(Antares.model.SolicitudAprobaciones ap in SolicitudAprobaciones.FindAll(Expression.Eq("IdSolicitud", sol.Id_Solicitud)))
                    {
                        ap.Delete();

                    }
                     sol.Status = "Pendiente";
                     sol.IdSolicitudInicial = 0;
                     sol.Save();


                }
                
        }
                    
    }
}
