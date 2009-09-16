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

public partial class Solicitudes_Capacitacion : System.Web.UI.Page
{
    static Antares.model.SolicitudCapacitacion sol_Cap;
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillSol();
            //UsuariosEmpleados ue = BiFactory.Empleado;

            //if (ue != null)
            //{
            //    string id = Request.QueryString["id"];
            //    CargarCombos();

            //    if (id != null)
            //    {
            //        sol_Cap = SolicitudCapacitacion.FindFirst(Expression.Eq("IdSolicitud", int.Parse(id)));
            //        if (sol_Cap != null)
            //        {
            //            BiFactory.Sol = Solicitud.GetById(sol_Cap.IdSolicitud);
            //            cmbNivelCapacitacion.SelectedValue = sol_Cap.Nivel;
            //            txtAreaEstudios.Text = sol_Cap.AreaEstudio;
            //            txtEntidad.Text = sol_Cap.EntidadEducativa;
            //            txtDuracion.Text = sol_Cap.Duracion;
            //            txtInstructor.Text = sol_Cap.Instructor;
            //            txtPuntuacionExamen.Text = sol_Cap.PuntuacionExamen;
            //            txtInicio.Text = sol_Cap.FechaInicio;
            //            txtFin.Text = sol_Cap.FechaFin;
            //        }
            //    }
            //}
            //else
            //{
            //    HtmlGenericControl lb = (HtmlGenericControl)Master.FindControl("divMensajes");
            //    if (lb != null)
            //    {
            //        lb.InnerText = "El Usuario NO esta relacionado con un Empleado";
            //    }
            //}
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        SolicitudCapacitacion Cap = SolicitudCapacitacion.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        if (Cap == null)
        {
            Cap = new SolicitudCapacitacion();
            Cap.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            Cap.Descripcion = BiFactory.Sol.Descripcion;

        }

        Cap.FechaInicio = txtInicio.Text;
        Cap.FechaFin = txtFin.Text;
        Cap.Nivel = cmbNivelCapacitacion.SelectedValue;
        Cap.Instructor = txtInstructor.Text;
        Cap.PuntuacionExamen = txtPuntuacionExamen.Text;
        Cap.Duracion = txtDuracion.Text;
        Cap.EntidadEducativa = txtEntidad.Text;
        Cap.AreaEstudio = txtAreaEstudios.Text;
        Cap.IdEmpleado = BiFactory.Empleado.IdEmpleados;
        Cap.SaveAndFlush();

        pnlCapacitacion.Visible = false;

        ucCapacitacion.Numero = Cap.IdSolicitud.ToString();
        ucCapacitacion.Titulo = Cap.Descripcion;
        ucCapacitacion.Nivel = Cap.Nivel;
        ucCapacitacion.FechaInicio = Cap.FechaInicio;
        ucCapacitacion.FechaFin = Cap.FechaFin;
        ucCapacitacion.Duracion = Cap.Duracion;
        ucCapacitacion.AreaEstudios = Cap.AreaEstudio;
        ucCapacitacion.Instructor = Cap.Instructor;
        ucCapacitacion.EntidadEducativa = Cap.EntidadEducativa;
        ucCapacitacion.PuntuacionExamen = Cap.PuntuacionExamen;
        ucCapacitacion.Visible = true;

        Sol = null;
    }

    private void FillSol()
    {
        sol_Cap = SolicitudCapacitacion.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        if (sol_Cap != null)
        {
            cmbNivelCapacitacion.SelectedValue = sol_Cap.Nivel;
            txtAreaEstudios.Text = sol_Cap.AreaEstudio;
            txtEntidad.Text = sol_Cap.EntidadEducativa;
            txtDuracion.Text = sol_Cap.Duracion;
            txtInstructor.Text = sol_Cap.Instructor;
            txtPuntuacionExamen.Text = sol_Cap.PuntuacionExamen;
            txtInicio.Text = sol_Cap.FechaInicio;
            txtFin.Text = sol_Cap.FechaFin;
        }
        else
        {
            txtInicio.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }
        CargarCombos();
    }
    private void CargarCombos()
    {
        cmbNivelCapacitacion.Items.Add("Básico");
        cmbNivelCapacitacion.Items.Add("Intermedio");
        cmbNivelCapacitacion.Items.Add("Avanzado");
        cmbNivelCapacitacion.Items.Add("Experto");
    }

}
