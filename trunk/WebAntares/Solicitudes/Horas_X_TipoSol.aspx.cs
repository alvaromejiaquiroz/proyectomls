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
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;

public partial class Solicitudes_TiemposPersonal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillGrid();
         
        }

    }
    protected void CargarCombos()
    {
        
      
    }
    protected void FillGrid()
    {
        gvHorasActualesXTipo.DataSource = Personal.GetTiempos_Personal_X_TipoSolicitud();
        gvHorasActualesXTipo.DataBind();
        gvHorasActualesXTipo_MES.DataSource = Personal.GetTiempos_Personal_X_TipoSolicitud_MES();
        gvHorasActualesXTipo_MES.DataBind();

        gvRankingHoras.DataSource = Personal.GetRankingHorasPersonal();
        gvRankingHoras.DataBind();
    }
    
}
