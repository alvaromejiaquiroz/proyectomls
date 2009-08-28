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

public partial class Controles_Obras : System.Web.UI.UserControl
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

    public string Titulo
    {
        set { litTitulo.Text = value; }
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
}
