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
        gvSolicitudes.DataSource = Personal.GetHorasSolicitudes(cmbPersonal.SelectedValue,cmbFecha.Fecha.ToString("yyyyMMdd"));//BiFactory.Sol.Id_Solicitud);
        //gvSolicitudes.DataKeyNames = new string[] { "Id" };
        gvSolicitudes.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}
