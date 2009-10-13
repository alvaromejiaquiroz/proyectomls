using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Antares.model;
using WebAntares;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;

public partial class Solicitudes_AprobacionSolicitudesGestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (HabilitarSegunPerfil())
            {
                FillGrid(0);
            }


        }

        else { FillGrid(0); }


    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(e.NewPageIndex);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
        {
            Int32 IdSolicitud = Int32.Parse(e.CommandArgument.ToString());
            BiFactory.Sol = Solicitud.GetById(IdSolicitud);

            switch (e.CommandName)
            {
                case "Visualizar":
                    Response.Redirect("~/Reportes/MostrarSolicitud.aspx?id=" + IdSolicitud.ToString());
                    break;
                case "Aprobar":
                    BiFactory.Sol.AprobacionTecnica = true;
                    BiFactory.Sol.FechaAprobacionTecnica = DateTime.Now;
                    BiFactory.Sol.Update();
                    Response.Redirect("./AprobacionSolicitudesGestion.aspx");
                    break;
                case "Rechazar":
                    BiFactory.Sol.AprobacionTecnica = false;
                    BiFactory.Sol.FechaAprobacionTecnica = DateTime.Now;
                    BiFactory.Sol.Update();
                    Response.Redirect("./AprobacionSolicitudesGestion.aspx");
                    break; 

            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgStatus = (Image)e.Row.FindControl("imgStatus");
            imgStatus.ImageUrl = "../images/realizado.gif";
            imgStatus.ToolTip = "Realizado";
        }       
    }
    private void FillGrid(int pageIndex)
    {
        string IdSolicitud= string.Empty;
        string TipoSolicitud = string.Empty;
        string IdResponsable = string.Empty;
        //Realizado es 2 y aprovada por Tecnico NULL
        DbDataReader reader = Antares.model.Solicitud.GetReader(IdSolicitud, TipoSolicitud,BiFactory.User.IdPerfil.ToString(),BiFactory.User.IdUsuario,"2",string.Empty );
        DataTable table = new DataTable();
        table.Load(reader);
        GridView1.DataSource = table;
        GridView1.PageIndex = pageIndex;
        GridView1.DataBind();
    }
    private bool HabilitarSegunPerfil()
    {

        if (BiFactory.User.IdPerfil < 3)
        {
            return true;
        }
        else
        {
            return false; 
        }

    }
}
