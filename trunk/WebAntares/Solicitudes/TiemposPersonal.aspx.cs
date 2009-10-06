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
         
        }

    }
    protected void CargarCombos()
    {
        
        cmbPersonal.Items.Clear();
        cmbPersonal.Items.Add(new ListItem("Seleccione...", "-1"));
        
        foreach (Antares.model.Personal p in Antares.model.Personal.FindAll())
        {
            cmbPersonal.Items.Add(new ListItem(p.Apellido + "," + p.Nombres, p.IdEmpleados.ToString()));
        }
    }

    protected void FillGrid()
    {
        //string Semana = cmbFecha.Fecha.ToString("yyyyMMdd");
        
        gvSolicitudes.DataSource = Personal.GetHorasSolicitudes(cmbPersonal.SelectedValue, txtDesde.Text);
        //gvSolicitudes.DataKeyNames = new string[] { "Id" };
        gvSolicitudes.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //DateTime fecha_seleccionada;
        //CultureInfo ciCurr = CultureInfo.CurrentCulture;

        
        //if (txtDesde.Text != string.Empty)
        //{
        //    fecha_seleccionada = DateTime.Parse(txtDesde.Text);
        //}


        //int semana = ciCurr.Calendar.GetWeekOfYear(fecha_seleccionada, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        //string Semana =  fecha_seleccionada;
        FillGrid();
        
    }
}
