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

public partial class Controles_MantenimientoCorrectivoRendicion : System.Web.UI.UserControl
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

    public string ReportoFalla
    {
        set { litReportoFalla.Text = value; }
    }

    public string CausaProbable
    {
        set { litCausaProbable.Text = value; }
    }

    public string FechaReporte
    {
        set { litFechaReporte.Text = value; }
    }

    public string Falla
    {
        set { litFalla.Text = value; }
    }

    public string Responsable
    {
        set { litResponsable.Text = value; }
    }

    public string Sitio
    {
        set { litSitio.Text = value; }
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

    public string ContactoCliente
    {
        set { litContactoCliente.Text = value; }
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
        set { litMonto.Text = "$"+ value; }
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

    public string Descripcion_TrabajoRealizado
    {
        set { lblDescripcion_TrabajoRealizado.Text = value; }
    }
    public string Conformidad_Cliente
    {
        set { lblConformidadCliente.Text = value; }
    }

    public string Fecha_Cierre_Mantenimiento
    {
        set { litFechaCierre.Text = value; }
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
    protected void gvAdjuntos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 Id = Int32.Parse(e.CommandArgument.ToString());
        Adjunto Adj = Adjunto.FindOne(Expression.Eq("IdAdjunto", Id));

        switch (e.CommandName)
        {
            case "download":
                System.IO.FileInfo file = new System.IO.FileInfo(Adj.PathFile);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Adj.FileName);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();

                }
                break;
        }
    }
}
