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
            CargarCombos();
            txtDesde.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

    }
    
    protected void CargarCombos()
    {
        
      
    }
    
    protected void FillGrid()
    {
        int Semana = 0;
        DateTime fecha = DateTime.Parse("1900-01-01");

        if (txtSemanaAño.Text != string.Empty)
        {
            Semana = int.Parse(txtSemanaAño.Text);
        }
        if (txtDesde.Text != string.Empty)
        {

            fecha = DateTime.Parse(txtDesde.Text);
            
        }

        gvTiemposPreventivo.DataSource = Personal.GetTiempos_Personal_MPREVENTIVO(fecha.ToString("yyyy-MM-dd"), Semana);
        gvTiemposCorrectivo.DataSource = Personal.GetTiempos_Personal_MCORRECTIVO(fecha.ToString("yyyy-MM-dd"), Semana);
        gvTiemposObra.DataSource = Personal.GetTiempos_Personal_OBRAS(fecha.ToString("yyyy-MM-dd"), Semana);
        gvCapacitacion.DataSource = Personal.GetTiempos_Personal_CAPACITACION(fecha.ToString("yyyy-MM-dd"), Semana);
        gvLicencias.DataSource = Personal.GetTiempos_Personal_LICENCIAS(fecha.ToString("yyyy-MM-dd"), Semana);
        gvTareasGenerales.DataSource = Personal.GetTiempos_Personal_TAREASGENERALES(fecha.ToString("yyyy-MM-dd"), Semana);

        gvTiemposPreventivo.DataBind();
        gvTiemposCorrectivo.DataBind();
        gvTiemposObra.DataBind();
        gvCapacitacion.DataBind();
        gvLicencias.DataBind();
        gvTareasGenerales.DataBind();
        
        if (gvTiemposPreventivo.Rows.Count > 0 )
        {
            pnlSolicitudesPreventivas.Visible = true;
        }

        if (gvTiemposCorrectivo.Rows.Count > 0)
        {
            pnlSolicitudesCorrectivas.Visible = true;
        }


        if (gvTiemposObra.Rows.Count > 0)
        {
            pnlObras.Visible = true;
        }

        if (gvCapacitacion.Rows.Count > 0)
        {
            pnlCapa.Visible = true;
        }

        if (gvLicencias.Rows.Count > 0)
        {
            pnlLicencias.Visible = true;
        }

        if (gvTareasGenerales.Rows.Count > 0)
        {
            pnlTG.Visible = true;
        }

    }
 
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            FillGrid();
            //pnlSolicitudes.Visible = true;
        }
        
    }

    
}
