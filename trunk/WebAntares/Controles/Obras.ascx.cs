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

public partial class Controles_Obras : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        VersionSistema.Text = WebAntares.AntaresHelper.Get_Config_VersionSistema();
    }

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Solicitudes.aspx");
    }

    public string Numero
    {
        set { litNumero.Text = value; }
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
        set { litMonto.Text = "$" + value; }
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
   
    public SolicitudGastos[] Gastos
    {
        set
        {
            gvGastos.DataSource = value;
            gvGastos.DataBind();
        }
    }
}
