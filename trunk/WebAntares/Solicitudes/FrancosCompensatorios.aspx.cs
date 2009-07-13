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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillSol();
        }

    }

    private void fillSol()
    {
        Fc = SolicitudFrancosCompensatorios.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        if (Fc != null)
        {
            txtDescripcion.Text = Fc.Descripcion.ToString();
            txtDuracion.Text = Fc.Duracion.ToString();
            //dtpFin.Text = Fc.FechaFin.ToString();
            dtpInicio.Text = Fc.FechaInicio.ToString();
            txtConsentimiento.Text = Fc.ConElConsentimiento.ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (RadioButtonList1.SelectedValue == "Si")
        {

            Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            Sol.Descripcion = txtDescripcion.Text;
            Sol.Status = eEstados.Pendiente.ToString();
            Sol.Save();

            if (Fc == null)
                Fc = new SolicitudFrancosCompensatorios();

            Fc.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            Fc.Descripcion = txtDescripcion.Text; 
            Fc.FechaInicio = dtpInicio.Text;
            //Fc.FechaFin = dtpFin.Text;
            Fc.Duracion = txtDuracion.Text;
            Fc.ConElConsentimiento = txtConsentimiento.Text;
            Fc.IdEmpleado = BiFactory.Empleado.IdEmpleados;
            Fc.SaveAndFlush();
            Response.Redirect("./Solicitudes.aspx");
        }
        else
        {


            string stext = "";

            stext = System.Environment.NewLine + "Debe Confirmar Para poder Continuar";

            HtmlGenericControl lb = (HtmlGenericControl)Master.FindControl("divMensajesGrilla");
            if (lb != null)
            {
                lb.InnerText = stext;
            }
            //   return stext.Length == 0;
        }



    }
}
