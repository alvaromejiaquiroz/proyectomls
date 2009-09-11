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
using System.Data.Common;
using Antares.model;
using NHibernate.Expression;

public partial class Controles_ObrasRendicion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Solicitudes.aspx");
    }

    public string Numero
    {
        set { litNumero.Text = value; }
    }

    public string SolicitudInicial
    {
        get { return ViewState["SolicitudInicial"].ToString(); }
        set { ViewState["SolicitudInicial"] = value; }
    }

    public string Titulo
    {
        set { litTitulo.Text = value; }
    }

    public string Estado
    {
        set { litEstado.Text = value; }
    }

    public string DescripcionTareas
    {
        set { litDescripcionTareas.Text = value; }
    }

    public string FechaInicio
    {
        set { litFechaInicio.Text = value; }
    }

    public string FechaEntrega
    {
        set { litFechaEntrega.Text = value; }
    }

    public string RequisitosAprobacion
    {
        set { litRequisitosAprobacion.Text = value; }
    }

    public string RequisitosIngreso
    {
        set { litRequisitosIngreso.Text = value; }
    }

    public DbDataReader Personal
    {
        set
        {
            gvPersonal.DataSource = value;
            gvPersonal.DataBind();
        }
    }

    public DbDataReader Vehiculos
    {
        set
        {
            gvVehiculos.DataSource = value;
            gvVehiculos.DataBind();
        }
    }

    public string Cliente
    {
        set { litCliente.Text = value; }
    }

    public string Contacto
    {
        set { litContacto.Text = value; }
    }

    public string NroOrden
    {
        set { litNroOrden.Text = value; }
    }

    public string TelefonoContacto
    {
        set { litTelefonoContacto.Text = value; }
    }

    public string MailContacto
    {
        set { litMailContacto.Text = value; }
    }

    public DbDataReader Adjuntos
    {
        set
        {
            gvAdjuntos.DataSource = value;
            gvAdjuntos.DataBind();
        }
    }

    public string Monto
    {
        set { litMonto.Text = value; }
    }

    public bool Imprimible
    {
        set
        {
            btnFinalizar.Visible = !value;
            pnlImprimir.Visible = value;
            imgAntares.Visible = value;
        }
    }

    protected void gvPersonal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DbDataRecord record = (DbDataRecord)e.Row.DataItem;
            ((Literal)e.Row.Cells[0].FindControl("litEmpleado")).Text = record["Empleado"].ToString();
            ((CheckBox)e.Row.Cells[0].FindControl("chkResponsable")).Checked = (bool)record["Responsable"];
            SolicitudRecursosEmpleados solicitudRecursosEmpleados = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", (int)record["Id"]));
            GridView horas = (GridView)e.Row.Cells[0].FindControl("gvHorasPersonal");
            horas.DataSource = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(int.Parse(SolicitudInicial), solicitudRecursosEmpleados.IdEmpleado);
            horas.DataBind();
        }
    }

    protected void gvVehiculos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DbDataRecord record = (DbDataRecord)e.Row.DataItem;
            ((Literal)e.Row.Cells[0].FindControl("litVehiculo")).Text = record["Vehiculo"].ToString();
            SolicitudRecursosVehiculos solicitudRecursosVehiculos = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", (int)record["Id"]));
            GridView horas = (GridView)e.Row.Cells[0].FindControl("gvHorasVehiculos");
            horas.DataSource = SolicitudRendicionVehiculosHoras.GetVehiculosKm_Detalle_EnSolicitud(int.Parse(SolicitudInicial), solicitudRecursosVehiculos.IdVehiculo);
            horas.DataBind();
        }
    }
}
