using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Antares.model;
using WebAntares;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;

public partial class Solicitudes_Reporte_Obras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            FillCamposObra();
            FillSolicitudTareas();
            FillSolicitudVehiculos();
            FillSolicitudEmpleados();
        }

    }
    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos t = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        FillSolicitudVehiculos();
    }
    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos t = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        FillSolicitudVehiculos();
    }
    protected void btnAsignaVehiculoSolicitud_Click(object sender, EventArgs e)
    {
        int idVehiculo = int.Parse(cmbVehiculo.SelectedValue);
        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        if (!SolicitudRecursosVehiculos.ExisteVehiculoEnSolicitud(sol.Id_Solicitud, idVehiculo))
        {

            SolicitudRecursosVehiculos t = new SolicitudRecursosVehiculos();
            t.IdSolicitud = sol.Id_Solicitud;
            t.IdVehiculo = int.Parse(cmbVehiculo.SelectedValue);
            t.Km = 0;
            t.Horas = 0;
            t.Fecha = null;
            t.Save();
        
        }
        FillSolicitudVehiculos();
    }
    protected void btnAsignaEmpleadoSolicitud_Click(object sender, EventArgs e)
    {
        int idEmpleado = int.Parse(cmbEmpleado.SelectedValue);
        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        if (!SolicitudRecursosEmpleados.ExisteEmpleadoEnSolicitud(sol.Id_Solicitud, idEmpleado))
        {
            SolicitudRecursosEmpleados p = new SolicitudRecursosEmpleados();
            p.IdSolicitud = sol.Id_Solicitud;
            p.IdEmpleado = idEmpleado;
            p.Horas = 0;
            p.Responsable = false;
            p.Fecha = DateTime.Now.ToString();
            p.Save();
        }
        FillSolicitudEmpleados();
    }   
    private void FillCamposObra()
    {
        
        SolicitudObra o = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", int.Parse(BiFactory.Sol.RelacionadaCon)));
        txtTareasSolicitadas_Desc.Text = o.DescripcionTareas;
        txtGastos.Text = o.Presupuesto;
        

    }
    private void FillSolicitudEmpleados()
    {
        gvSolicitudPersonas.DataSource = SolicitudRecursosEmpleados.GetPersonasEnSolicitud(BiFactory.Sol.Id_Solicitud);
        gvSolicitudPersonas.DataKeyNames = new string[] { "Id" };
        gvSolicitudPersonas.DataBind();
    }
    private void FillSolicitudVehiculos()
    {
        //gvSolicitudVehiculos.DataSource = SolicitudRecursosVehiculos.GetVehiculosKmEnSolicitud(BiFactory.Sol.Id_Solicitud);gvSolicitudVehiculos.DataSource = SolicitudRecursosVehiculos.GetVehiculosKmEnSolicitud(BiFactory.Sol.Id_Solicitud);
        gvSolicitudVehiculos.DataSource = SolicitudRecursosVehiculos.GetVehiculosEnSolicitud(BiFactory.Sol.Id_Solicitud);
        gvSolicitudVehiculos.DataKeyNames = new string[] { "Id" };
        gvSolicitudVehiculos.DataBind();
    }
    private void FillSolicitudTareas()
    {
        gvTareasObras.DataSource = SolicitudObraReporteTareas.FindAll(Expression.Eq("IdSolicitud", (BiFactory.Sol.Id_Solicitud)));
        gvTareasObras.DataKeyNames = new string[] { "Id" };
        gvTareasObras.DataBind();
    }

    protected void gvTareasObras_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvTareasObras_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudObraReporteTareas t = SolicitudObraReporteTareas.FindFirst(Expression.Eq("Id", int.Parse(gvTareasObras.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        FillSolicitudTareas();
    }
    private void CargarCombos()
    {

        foreach (Antares.model.Vehiculos v in Antares.model.Vehiculos.FindAll())
        {
            cmbVehiculo.Items.Add(new ListItem(v.Marca + " " + v.Modelo + " " + v.Patente, v.IdVehiculos.ToString()));
        }

      

        foreach (Antares.model.Personal p in Antares.model.Personal.FindAll())
        {
            cmbEmpleado.Items.Add(new ListItem(p.Apellido + "," + p.Nombres, p.IdEmpleados.ToString()));
        }


    }

 
    
    protected void btnAgregaTareaAObra_Click(object sender, EventArgs e)
    {
        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        SolicitudObraReporteTareas t = new SolicitudObraReporteTareas();
        t.Tarea = txtTareaRealizada.Text;
        t.DescripcionTareas = txtDescripcionTarea.Text;
        t.IdSolicitud = sol.Id_Solicitud;
        t.Save();
        txtTareaRealizada.Text = string.Empty;
        txtDescripcionTarea.Text = string.Empty;

        FillSolicitudTareas();


    }

 
    protected void btnCancelarSolicitud_Click(object sender, EventArgs e)
    {

    }
    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
            Solicitud_Relacion Rela = Solicitud_Relacion.FindFirst(Expression.Eq("IdSolicitud",BiFactory.Sol.Id_Solicitud));
            Solicitud Sol_Original = Solicitud.GetById(Rela.IdSolicitud_Relacionada);
            Sol_Original.Status = eEstados.Realizado.ToString();
            Sol_Original.Update();
    
            Solicitud Reporte = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            
            SolicitudObra Obra = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
            
            TransactionScope _transaction = new TransactionScope();
            try
            {
            SolicitudReporteObra r = new SolicitudReporteObra();
            r.IdSolicitud = Obra.IdSolicitud;
            r.RecibioConformeCliente   = rdbConformeCli.Text;
            r.SeCumplieronRequisitosApr = rdbCumplieronReq.Text;
            r.FechaEntregaCliente = dtpFechaEntregaCliente.Text;
            r.FechaActualizacion = DateTime.Now;
            Reporte.Status = eEstados.Realizado.ToString();
            r.Save();

            }
            catch (Exception oEx)
            {
                
                _transaction.VoteRollBack();
                Response.Redirect("./Mensaje.aspx?Id=" + Sol_Original.Id_Solicitud.ToString() + "&St=false");
                throw;
            }
            finally
            {
                _transaction.Dispose();
                Response.Redirect("./Mensaje.aspx?Id=" + Sol_Original.Id_Solicitud.ToString() + "&St=true" );
                //Response.Redirect("./Solicitudes.aspx");
            }
        }
        
}


