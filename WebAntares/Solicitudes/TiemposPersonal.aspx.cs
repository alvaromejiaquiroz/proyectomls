﻿using System;
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
        
        foreach (Antares.model.Personal p in Antares.model.Personal.GetPersonalActivo())
        {
            cmbPersonal.Items.Add(new ListItem(p.Apellido + "," + p.Nombres, p.IdEmpleados.ToString()));
        }
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
       

        gvTiempos.DataSource = null;
        gvTiempos.DataBind();
        gvTiempos.DataSource = Personal.GetHorasSolicitudes(cmbPersonal.SelectedValue, fecha.ToString("yyyy-MM-dd"), Semana);
        //gvTiempos.DataKeyNames = new string[] { "ID" };
        gvTiempos.DataBind();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            FillGrid();
        }
        
    }
    protected void gvTiempos_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvTiempos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;

        if (cmbPersonal.SelectedIndex > 0)
        {
            args.IsValid = true;
        }
    }
}
