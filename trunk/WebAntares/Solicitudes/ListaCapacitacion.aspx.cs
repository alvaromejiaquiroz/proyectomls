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
using WebAntares;
using Antares.model;
using NHibernate.Expression;


public partial class Solicitudes_ListaCapacitacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!Page.IsPostBack)
        {
            FillGrilla();
        }
    }
    private void   FillGrilla()
    {
        GridView1.DataKeyNames = new string[] { "IdSolicitud" };
        
        //GridView1.DataSource = Antares.model.SolicitudCapacitacion.GetReader();
        GridView1.DataSource = Antares.model.SolicitudCapacitacion.FindAllByProperty("IdEmpleado", BiFactory.Empleado.IdEmpleados);
        GridView1.DataBind();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Int32 IdSolicitud = Int32.Parse(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        BiFactory.Sol = Solicitud.GetById(IdSolicitud);
        Response.Redirect("./Capacitacion.aspx?id=" + IdSolicitud.ToString());

        
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       int item_seleccionado = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
       Solicitud sol = Solicitud.FindFirst(Expression.Eq("Id_Solicitud", item_seleccionado));
       if (sol != null)
       {
           sol.Delete();
           FillGrilla();
       }
       
    }
}
