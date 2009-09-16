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

public partial class Solicitudes_ListaLicencias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombo();
        }
    }

    private void CargarCombo()
    {
        cmbEmpleado.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal p in Antares.model.Personal.FindAll())
        {
            cmbEmpleado.Items.Add(new ListItem(p.Apellido + ", " + p.Nombres, p.IdEmpleados.ToString()));
        }
    }

    private void FillGrilla(string Id_Empleado)
    {
        GridView1.DataKeyNames = new string[] { "IdSolicitud" };
        GridView1.DataSource = Antares.model.SolicitudLicencias.FindAllByProperty("IdEmpleado", int.Parse(Id_Empleado));
        GridView1.DataBind();
    }
        
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int item_seleccionado = int.Parse(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        Response.Redirect("Licencias.aspx?id=" + item_seleccionado.ToString());
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item_seleccionado = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
        Solicitud sol = Solicitud.FindFirst(Expression.Eq("Id_Solicitud", item_seleccionado));
        if (sol != null)
        {
            sol.Delete();
            FillGrilla(cmbEmpleado.SelectedValue);
        }
       
       
    }

    protected void cmbEmpleado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbEmpleado.SelectedValue.Equals("-1"))
        {
            GridView1.Visible = false;
        }
        else
        {
            GridView1.Visible = true;
            FillGrilla(cmbEmpleado.SelectedValue);
        }
    }
}
