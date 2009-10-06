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

public partial class Administracion_ListadoFacturas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarGrilla();
            
        }
    }
    protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id     =     e.CommandArgument.ToString();
        Facturas F = Antares.model.Facturas.FindFirst(Expression.Eq("Id", Int32.Parse(id)));

        switch (e.CommandName)
        {
            case "Eliminar":
                
                F.DeleteAndFlush();

                break;

            case "Editar":
                Session["Factura"] = id;

                Response.Redirect("./Facturas.aspx");

                break;
        }

        CargarGrilla();

    }
    private void CargarGrilla()
    {
        gvFacturas.DataSource = Antares.model.Facturas.ListadoFacturas();
        gvFacturas.DataBind();

    }
}
