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
using Antares.model;
using WebAntares;
using NHibernate.Expression;

public partial class Solicitudes_FrancosCompensatorios : System.Web.UI.Page
{
    static Antares.model.SolicitudFrancosCompensatorios Fc;
    static int Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            FillSol();
        }
    }

    private void FillSol()
    {
        Fc = SolicitudFrancosCompensatorios.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        if (Fc != null)
        {
            txtDescripcion.Text = Fc.Descripcion;
            txtDuracion.Text = Fc.Duracion;
            txtFecha.Text = Fc.FechaInicio;
            txtConsentimiento.Text = Fc.ConElConsentimiento;

        }
        else
        {
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            Sol.Descripcion = txtDescripcion.Text;
            Sol.Status = eEstados.Pendiente.ToString();
            Sol.Save();

            if (Fc == null)
                Fc = new SolicitudFrancosCompensatorios();

            Fc.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            Fc.Descripcion = txtDescripcion.Text; 
            Fc.FechaInicio = txtFecha.Text;
            Fc.Duracion = txtDuracion.Text;
            Fc.ConElConsentimiento = txtConsentimiento.Text;
            Fc.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            Fc.SaveAndFlush();

            pnlFrancosCompensatorios.Visible = false;

            ucFrancosCompensatorios.Numero = Fc.IdSolicitud.ToString();
            ucFrancosCompensatorios.Titulo = Sol.Descripcion;
            ucFrancosCompensatorios.Fecha = Fc.FechaInicio;
            ucFrancosCompensatorios.Duracion = Fc.Duracion;
            ucFrancosCompensatorios.Descripcion = Fc.Descripcion;
            ucFrancosCompensatorios.ConsentimientoDe = Fc.ConElConsentimiento;
            ucFrancosCompensatorios.Visible = true;
        }
    }
}
