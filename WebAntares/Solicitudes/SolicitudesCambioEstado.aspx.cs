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
using Castle.ActiveRecord;

public partial class Solicitudes_SolicitudesCambioEstado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            LlenaCombo();
        }
    }

    protected void IniciaPanel()
    {
        txtAprobador.Text = string.Empty;
        txtCausa.Text = string.Empty;
        txtReprogramacion.Text = string.Empty;

        txtInicio.Text = DateTime.Today.ToString("dd/MM/yyyy");
        txtReprogramacion.Text = DateTime.Today.ToString("dd/MM/yyyy");
     //   switch ((eEstados)int.Parse(cmbEstados.SelectedValue))
        switch (cmbEstados.SelectedValue)
        {
            case "Reprogramado":
                btnAceptar.Visible = false;
                cmbEstados.Enabled = false;
                pnlReprogramado.Visible = true;
                trFechas.Visible = true;
                litFecha.Text = "Fecha de reprogramación";
                rfvReprogramacion.ErrorMessage = "Debe ingresar la fecha de reprogramación.";
                cvReprogramacion.ErrorMessage = "La fecha de reprogramación no es válida.";
                rfvInicio.Enabled = true;
                cvInicio.Enabled = true;
                rfvFin.Enabled = true;
                cvFin.Enabled = true;
                cvFechas.Enabled = true;
                break;
            case "Cancelado":
                btnAceptar.Visible = false;
                cmbEstados.Enabled = false;
                pnlReprogramado.Visible = true;
                trFechas.Visible = false;
                litFecha.Text = "Fecha de cancelación";
                rfvReprogramacion.ErrorMessage = "Debe ingresar la fecha de cancelación.";
                cvReprogramacion.ErrorMessage = "La fecha de cancelación no es válida.";
                rfvInicio.Enabled = false;
                cvInicio.Enabled = false;
                rfvFin.Enabled = false;
                cvFin.Enabled = false;
                cvFechas.Enabled = false;
                break;
            case "Realizado":
                MakeRendicion(BiFactory.Sol.Id_Solicitud);
                break;
        }
    }
    
    private void MakeRendicion(int idSolOrg)
    {
        TransactionScope _transaction = new TransactionScope();
        Solicitud sol;
        sol = Solicitud.GetById(idSolOrg);
        DateTime fechanula = DateTime.Parse("01/01/1900");
        Solicitud  reporte ;//= new Solicitud();
        try {
            reporte = Solicitud.FindOne(Expression.Eq("IdSolicitudInicial", idSolOrg));
        
            if (reporte == null)
            {
                
                reporte = new Solicitud();
                reporte.IdSolicitudInicial = sol.Id_Solicitud;
                reporte.IdCliente = sol.IdCliente;
                reporte.IdTipoSolicitud =  sol.IdTipoSolicitud;
                reporte.IdUsuarioCreador = BiFactory.User.IdUsuario;
                reporte.Reporte = "SI";
                reporte.Status = eEstados.Pendiente.ToString();
                reporte.NroOrdenCte = sol.NroOrdenCte;
                reporte.Contacto = sol.Contacto;
                reporte.ContactoMail = sol.ContactoMail;
                reporte.ContactoTel = sol.ContactoTel;
                reporte.Descripcion = sol.Descripcion;

                reporte.FechaCreacion = DateTime.Now;
                reporte.FechaAprobacionCalidad = fechanula;
                reporte.FechaAprobacionTecnica = fechanula;
                reporte.FechaReprogramacion = fechanula;
                reporte.FechaSuspencion = fechanula;
                reporte.ProximaFechaInicio = fechanula;
                reporte.ProximaFechaFin = fechanula;
                reporte.Save();

                TipoSolicitud tipo = sol.Tipo;

                if (tipo.IdTiposolicitud == 1) ///Mantenimiento Preventivo
                {
                    SolicitudPreventivo Sol_Prev = SolicitudPreventivo.FindOne(Expression.Eq("IdSolicitud", idSolOrg));
                    Sol_Prev.Id = 0;
                    Sol_Prev.IdSolicitud = reporte.Id_Solicitud;
                    Sol_Prev.SaveCopyAndFlush();


                    //copio las tareas
                    SolicitudTareas[] tt = SolicitudTareas.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
                    foreach (SolicitudTareas t in tt)
                    {
                        t.Id = 0; //para que guarde una copia
                        t.IdSolicitud = reporte.Id_Solicitud;
                        t.SaveCopyAndFlush();
                    }


                    //copio los servicios afectados
                    SolicitudServiciosAfectados[] ss = SolicitudServiciosAfectados.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
                    foreach (SolicitudServiciosAfectados s in ss)
                    {
                        s.Id = 0;
                        s.IdSolicitud = reporte.Id_Solicitud;
                        s.SaveCopyAndFlush();
                    }

                }

                if (tipo.IdTiposolicitud == 2) ///Mantenimiento Correctivo
                {
                    SolicitudCorrectivo Sol_Corr_original= SolicitudCorrectivo.FindOne(Expression.Eq("IdSolicitud", idSolOrg));
                    Sol_Corr_original.Id = 0;
                    Sol_Corr_original.IdSolicitud = reporte.Id_Solicitud;
                    Sol_Corr_original.FechaResolucion = DateTime.Parse("1900-01-01");
                    Sol_Corr_original.SaveCopyAndFlush();

                  
                }

                if (tipo.IdTiposolicitud == 6) ///Obras e Instalaciones
                {

                    
                        SolicitudObra Sol_Obra = SolicitudObra.FindOne(Expression.Eq("IdSolicitud",idSolOrg));
                        Sol_Obra.Id = 0;
                        Sol_Obra.IdSolicitud = reporte.Id_Solicitud;
                        Sol_Obra.SaveCopy();

                }

                //copio las PErsonas
                SolicitudRecursosEmpleados[] ee = SolicitudRecursosEmpleados.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
                foreach (SolicitudRecursosEmpleados e in ee)
                {
                    e.Id = 0;
                    e.IdSolicitud = reporte.Id_Solicitud;
                    e.SaveCopy();
                }

                //copio los vehiculos
                SolicitudRecursosVehiculos[] vv = SolicitudRecursosVehiculos.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
                foreach (SolicitudRecursosVehiculos v in vv)
                {
                    v.Id = 0;
                    v.IdSolicitud = reporte.Id_Solicitud;
                    v.Horas = 0;
                    v.Km = 0;
                    v.SaveCopy();
                 
                }
            }
            reporte.Save();
        }
        catch (Exception)
        {
            _transaction.VoteRollBack();
            throw;
        }
        finally
        {
            _transaction.Dispose();

            switch (sol.IdTipoSolicitud)
            {
                case 1:
                    Response.Redirect("MantPreventivoRendicion.aspx");
                    break;
                case 2:
                    Response.Redirect("MantCorrectivoRendicion.aspx");
                    break;
                case 6:
                    Response.Redirect("Reporte_Obras.aspx");
                    break;
            }
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            IniciaPanel();
        }
    }

    public void btnAceptarReprogramacion_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            sol.Causa = txtCausa.Text;
            sol.AprobadorReprosusp = txtAprobador.Text;
            switch (cmbEstados.SelectedItem.ToString())
            {
                case "Reprogramado":
                    switch (sol.IdTipoSolicitud)
                    {
                        case 1:
                            sol.Status = eEstados.Reprogramado.ToString();
                            sol.FechaReprogramacion = DateTime.Parse(txtReprogramacion.Text);

                            SolicitudPreventivo sP = SolicitudPreventivo.FindOne(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                            SolicitudTareas sT = SolicitudTareas.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                            if ((sP != null) && (sT != null))
                            {
                            sP.FechaInicio = DateTime.Parse(txtInicio.Text);
                            sP.FechaFin = DateTime.Parse(txtFin.Text);
                            sol.ProximaFechaInicio = sT.FechaInicio;
                            sol.ProximaFechaFin = sT.FechaFin;
                            sT.FechaInicio = DateTime.Parse(txtInicio.Text);
                            sT.FechaFin = DateTime.Parse(txtFin.Text);
                            sP.Update();
                            sT.Update();
                            }
                            break;
                        default:
                            sol.Status = eEstados.Reprogramado.ToString();
                            sol.FechaReprogramacion = DateTime.Parse(txtReprogramacion.Text);
                            sol.ProximaFechaInicio = DateTime.Parse(txtInicio.Text);
                            sol.ProximaFechaFin = DateTime.Parse(txtFin.Text);
                            break;
                    } break;

                case "Cancelado":
                    sol.Status = eEstados.Cancelado.ToString();
                    sol.FechaSuspencion = DateTime.Parse(txtReprogramacion.Text);
                    break;
            }
            sol.Update();
            pnlReprogramado.Visible = false;
            Response.Redirect("Solicitudes.aspx");
        }
    }

    public void btnCancelar_Click(object sender, EventArgs e)
    {
        btnAceptar.Visible = true;
        cmbEstados.Enabled = true;
        pnlReprogramado.Visible = false;
    }

    protected void LlenaCombo()
    {
        cmbEstados.Items.Add(new ListItem("Seleccione...", "-1"));

        
        cmbEstados.DataSource = AntaresHelper.GetListaEstadosAutorizados(BiFactory.Perfil.Detalle);
        cmbEstados.DataTextField = "Valor";
        cmbEstados.DataValueField = "Valor";
        cmbEstados.DataBind();

        if (!cmbEstados.Items.Contains(new ListItem("Reprogramado")) || (BiFactory.Sol.Status == "Reprogramado"))
        {
            cmbEstados.Items.Remove(new ListItem("Reprogramado", "Reprogramado"));
        }
  

        int idResponsable = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud), Expression.Eq("Responsable", true)).IdEmpleado;
        if (idResponsable == BiFactory.Empleado.IdEmpleados)
        {
            if (!cmbEstados.Items.Contains(new ListItem("Realizado")))
            {
                cmbEstados.Items.Add(new ListItem("Realizado", "Realizado"));
            }
        }

    }
}
