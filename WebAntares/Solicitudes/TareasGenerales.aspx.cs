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

public partial class Solicitudes_TareasGenerales : System.Web.UI.Page
{
    static Antares.model.SolicitudTareasGenerales tarea_general;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDatos();
            CargarCombos();
        }
    }

    private void FillDatos()
    { 
        tarea_general = Antares.model.SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        if (tarea_general != null)
        {
            txtInicio.Text = tarea_general.FechaInicio;
            txtFin.Text = tarea_general.FechaFin.ToString();
            txtDuracion.Text = tarea_general.Duracion;
            txtDescripcion.Text = tarea_general.Descripcion;
            if (cmbTipoTarea.Items.FindByValue(tarea_general.IdTipotarea.ToString()) != null)
            {
                cmbTipoTarea.SelectedItem.Value = tarea_general.IdTipotarea.ToString();
            }
        }
    }

    private void CargarCombos()
    {
        foreach (Antares.model.TipoTarea Tg in Antares.model.TipoTarea.FindAll())
        {
            cmbTipoTarea.Items.Add(new ListItem(Tg.Descripcion, Tg.Id.ToString()));
        }
        if (tarea_general != null)
        {
            if (cmbTipoTarea.Items.FindByValue(tarea_general.IdTipotarea.ToString()) != null)
            {
                cmbTipoTarea.SelectedItem.Value = tarea_general.IdTipotarea.ToString();
            }
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);

        if (tarea_general == null)
        {
            tarea_general = new SolicitudTareasGenerales();
        }

        Sol.Descripcion = txtDescripcion.Text;
        Sol.Status = eEstados.Pendiente.ToString();
        Sol.SaveAndFlush();

        tarea_general.IdSolicitud = BiFactory.Sol.Id_Solicitud;
        tarea_general.Descripcion = txtDescripcion.Text;
        tarea_general.FechaInicio = txtInicio.Text;
        tarea_general.FechaFin = txtFin.Text;
        tarea_general.Duracion = txtDuracion.Text;
        tarea_general.IdTipotarea = int.Parse(cmbTipoTarea.SelectedItem.Value);
        tarea_general.IdEmpleado = BiFactory.Empleado.IdEmpleados;
        tarea_general.SaveAndFlush();
        pnlSolicitud.Visible = false;

        ucTareasGenerales.Numero = tarea_general.IdSolicitud.ToString();
        ucTareasGenerales.Titulo = Sol.Descripcion;
        ucTareasGenerales.Tipo = cmbTipoTarea.SelectedItem.Text;
        ucTareasGenerales.FechaInicio = tarea_general.FechaInicio;
        ucTareasGenerales.FechaFin = tarea_general.FechaFin;
        ucTareasGenerales.Duracion = tarea_general.Duracion;
        ucTareasGenerales.Descripcion = tarea_general.Descripcion;
        ucTareasGenerales.Visible = true;
    }
}