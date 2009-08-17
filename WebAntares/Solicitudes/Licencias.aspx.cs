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

public partial class Solicitudes_Licencias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["Accion"] = "Alta";
            CargarCombos();
            string id = Request.QueryString["id"];
            if (id != null)
            {
                Session["Accion"] = "Mod";
                Session["id"] = int.Parse(id);
                
                SolicitudLicencias sol_Lic = SolicitudLicencias.FindFirst(Expression.Eq("IdSolicitud", int.Parse(id)));
                TipoLicencia tipolic = TipoLicencia.FindFirst(Expression.Eq("Id", sol_Lic.IdTipolicencia));
                WebAntares.BiFactory.Sol = Antares.model.Solicitud.GetById(int.Parse(Request.QueryString["id"].ToString()));

                cmbTipoLicencia.SelectedValue = tipolic.Descripcion;
                cmbTipoLicencia.SelectedItem.Value=  tipolic.Id.ToString();

                txtDescripcion.Text = sol_Lic.Descripcion;
                txtDuracion.Text = sol_Lic.Duracion;
                txtInicio.Text = sol_Lic.FechaInicio;
                txtFin.Text = sol_Lic.FechaFin;
            }
        }
    }

    private void CargarCombos()
    {
        foreach (Antares.model.TipoLicencia Tl in Antares.model.TipoLicencia.FindAll())
        {
            cmbTipoLicencia.Items.Add(new ListItem(Tl.Descripcion, Tl.Id.ToString()));
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            SolicitudLicencias sol_Lic;

            string Ac = Session["Accion"].ToString();
            if (Ac == "Alta")
            {
                Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
                sol_Lic = new Antares.model.SolicitudLicencias();
                sol_Lic.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            }
            else
            {
                sol_Lic = SolicitudLicencias.FindFirst(Expression.Eq("IdSolicitud", Session["id"]));
            }

            sol_Lic.Descripcion = txtDescripcion.Text;
            sol_Lic.FechaInicio = txtInicio.Text;
            sol_Lic.FechaFin = txtFin.Text;
            sol_Lic.Duracion = txtDuracion.Text;
            sol_Lic.IdTipolicencia = int.Parse(cmbTipoLicencia.SelectedItem.Value);
            sol_Lic.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            sol_Lic.Save();

            pnlLicencias.Visible = false;

            ucLicencias.Numero = sol_Lic.IdSolicitud.ToString();
            ucLicencias.Titulo = BiFactory.Sol.Descripcion;
            ucLicencias.Tipo = cmbTipoLicencia.SelectedItem.Text;
            ucLicencias.FechaInicio = sol_Lic.FechaInicio;
            ucLicencias.FechaFin = sol_Lic.FechaFin;
            ucLicencias.Duracion = sol_Lic.Duracion;
            ucLicencias.Descripcion = sol_Lic.Descripcion;
            ucLicencias.Visible = true;
        }
    }
}
