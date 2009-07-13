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
using NHibernate.Expression;
using Castle.ActiveRecord;
using Antares.model;
using WebAntares;
public partial class Solicitudes_ListaTareasGenerales : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            FillGrilla();
        }
    }
    private void FillGrilla()
    {
        string idUser = BiFactory.User.IdUsuario.ToString();
        GridView1.DataKeyNames = new string[] { "IdSolicitud" };
        //GridView1.DataSource = Antares.model.SolicitudFrancosCompensatorios.FindAll(Expression.Eq("IdUsuarioCreador",idUser));
        GridView1.DataSource = Antares.model.SolicitudTareasGenerales.FindAllByProperty("IdEmpleado", BiFactory.Empleado.IdEmpleados);
        GridView1.DataBind();

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int item_seleccionado = int.Parse(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        Response.Redirect("./TareasGenerales.aspx?id=" + item_seleccionado.ToString());



    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item_seleccionado = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

        SolicitudTareasGenerales sol = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", item_seleccionado));
        sol.Delete();

        FillGrilla();

    }
}
