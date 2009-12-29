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
using System.Data.Common;


public partial class Solicitudes_ListaTareasGenerales : System.Web.UI.Page
{
    protected int IdEmpleado = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (AntaresHelper.GetPuedeBuscar_Listado_TareasGenerales(BiFactory.Perfil.Detalle))
            {

                CargarCombos();
                pnlBuscar.Visible = true;
            }
            else
            {
                IdEmpleado = BiFactory.Empleado.IdEmpleados;
                FillGrilla(0, IdEmpleado);
            }
        }
    }
    
    private void CargarCombos()
    {

        cmbEmpleados.Items.Clear();
        cmbEmpleados.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal empleado in Antares.model.Personal.GetPersonalActivo())
        {
            cmbEmpleados.Items.Add(new ListItem(empleado.Apellido + ", " + empleado.Nombres, empleado.IdEmpleados.ToString()));
        }
    }
   
    private void FillGrilla(int pageIndex, int IdEmpleado)
    {

        DataTable t = new DataTable();
        DbDataReader reader = Antares.model.SolicitudTareasGenerales.Get_TareasGenerales_X_Persona(IdEmpleado);
        t.Load(reader);

        GridView1.DataKeyNames = new string[] { "IdSolicitud" };
        GridView1.DataSource = t;
        GridView1.PageIndex = pageIndex;
        GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Int32 IdSolicitud = Int32.Parse(GridView1.DataKeys[e.NewEditIndex].Value.ToString());
        BiFactory.Sol = Solicitud.GetById(IdSolicitud);
        Response.Redirect("./TareasGenerales.aspx?id=" + IdSolicitud.ToString());
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int item_seleccionado = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
        Solicitud sol = Solicitud.FindFirst(Expression.Eq("Id_Solicitud", item_seleccionado));
        if (IdEmpleado == 0)
        {
            IdEmpleado = int.Parse(cmbEmpleados.SelectedValue);
        }
        if (sol != null)
        {
            sol.Delete();
            FillGrilla(0, IdEmpleado);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrilla(e.NewPageIndex, IdEmpleado);
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (IdEmpleado == 0)
        {
            IdEmpleado = int.Parse(cmbEmpleados.SelectedValue);
        }
        FillGrilla(0, IdEmpleado);
    }
}
