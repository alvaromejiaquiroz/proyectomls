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
using NHibernate.Expression;

public partial class Solicitudes_VisualizarReporte : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]) && int.TryParse(Request.QueryString["id"], out id))
            {
                Solicitud solicitud = Solicitud.GetById(id);
                switch (solicitud.Tipo.IdTiposolicitud)
                {
                    case (int)EnumTipoSolicitud.MantenimientoPreventivo:
                        SolicitudPreventivo solicitudPreventivo = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucMantenimientoPreventivoRendicion.Numero = solicitud.Id_Solicitud.ToString();
                        ucMantenimientoPreventivoRendicion.SolicitudInicial = solicitud.IdSolicitudInicial.ToString();
                        ucMantenimientoPreventivoRendicion.Titulo = solicitud.Descripcion;
                        ucMantenimientoPreventivoRendicion.Estado = solicitud.Status;
                        ucMantenimientoPreventivoRendicion.Sitio = Sitios.FindFirst(Expression.Eq("IdSitio", solicitudPreventivo.IdSitio)).Descripcion;
                        ucMantenimientoPreventivoRendicion.Tareas = SolicitudTareas.GetReader(solicitudPreventivo.IdSolicitud);
                        ucMantenimientoPreventivoRendicion.Personal = SolicitudRecursosEmpleados.GetPersonaHoras_Detalle_EnSolicitud(solicitud.IdSolicitudInicial);
                        ucMantenimientoPreventivoRendicion.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudPreventivo.IdSolicitud);
                        ucMantenimientoPreventivoRendicion.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucMantenimientoPreventivoRendicion.ContactoCliente = solicitud.Contacto;
                        ucMantenimientoPreventivoRendicion.NroOrden = solicitud.NroOrdenCte;
                        ucMantenimientoPreventivoRendicion.TelefonoContacto = solicitud.ContactoTel;
                        ucMantenimientoPreventivoRendicion.MailContacto = solicitud.ContactoMail;
                        ucMantenimientoPreventivoRendicion.Adjuntos = solicitud.GetAdjuntos();
                        ucMantenimientoPreventivoRendicion.Monto = solicitudPreventivo.Presupuesto;
                        ucMantenimientoPreventivoRendicion.Responsable = Solicitud.GetResponsable(solicitudPreventivo.IdSolicitud.ToString());
                        ucMantenimientoPreventivoRendicion.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.MantenimientoCorrectivo:
                        SolicitudCorrectivo solicitudCorrectivo = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucMantenimientoCorrectivoRendicion.Numero = solicitudCorrectivo.IdSolicitud.ToString();
                        ucMantenimientoCorrectivoRendicion.SolicitudInicial = solicitud.IdSolicitudInicial.ToString();
                        ucMantenimientoCorrectivoRendicion.Titulo = solicitud.Descripcion;
                        ucMantenimientoCorrectivoRendicion.Estado = solicitud.Status;
                        ucMantenimientoCorrectivoRendicion.ReportoFalla = solicitudCorrectivo.PersonaReportoFalla;
                        ucMantenimientoCorrectivoRendicion.CausaProbable = solicitudCorrectivo.CausaPosible;
                        ucMantenimientoCorrectivoRendicion.FechaReporte = solicitudCorrectivo.FechanotificacionCliente.ToString("dd/MM/yyyy HH:mm");
                        ucMantenimientoCorrectivoRendicion.Falla = solicitudCorrectivo.FallaReportada;
                        ucMantenimientoCorrectivoRendicion.Servicios = SolicitudServiciosAfectados.GetServiciosAfectados(solicitudCorrectivo.IdSolicitud);
                        ucMantenimientoCorrectivoRendicion.Plazo = PlazoRealizacion.FindFirst(Expression.Eq("Id", solicitudCorrectivo.IdPlazoAtencion)).Descripcion;
                        ucMantenimientoCorrectivoRendicion.Personal = SolicitudRecursosEmpleados.GetPersonaHoras_Detalle_EnSolicitud(solicitud.IdSolicitudInicial);
                        ucMantenimientoCorrectivoRendicion.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudCorrectivo.IdSolicitud);
                        ucMantenimientoCorrectivoRendicion.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucMantenimientoCorrectivoRendicion.ContactoCliente = solicitud.Contacto;
                        ucMantenimientoCorrectivoRendicion.NroOrden = solicitud.NroOrdenCte;
                        ucMantenimientoCorrectivoRendicion.TelefonoContacto = solicitud.ContactoTel;
                        ucMantenimientoCorrectivoRendicion.MailContacto = solicitud.ContactoMail;
                        ucMantenimientoCorrectivoRendicion.Adjuntos = solicitud.GetAdjuntos();
                        ucMantenimientoCorrectivoRendicion.Monto = solicitudCorrectivo.Presupuesto;
                        ucMantenimientoCorrectivoRendicion.Responsable = Solicitud.GetResponsable(solicitudCorrectivo.IdSolicitud.ToString());
                        ucMantenimientoCorrectivoRendicion.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.Obras:
                        SolicitudObra solicitudObra = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucObrasRendicion.Numero = solicitudObra.IdSolicitud.ToString();
                        ucObrasRendicion.SolicitudInicial = solicitud.IdSolicitudInicial.ToString();
                        ucObrasRendicion.Titulo = solicitud.Descripcion;
                        ucObrasRendicion.Estado = solicitud.Status;
                        ucObrasRendicion.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucObrasRendicion.NroOrden = solicitud.NroOrdenCte;
                        ucObrasRendicion.Contacto = solicitud.Contacto;
                        ucObrasRendicion.MailContacto = solicitud.ContactoMail;
                        ucObrasRendicion.TelefonoContacto = solicitud.ContactoTel;
                        ucObrasRendicion.DescripcionTareas = solicitudObra.DescripcionTareas;
                        ucObrasRendicion.FechaInicio = solicitudObra.FechaInicio;
                        ucObrasRendicion.FechaEntrega = solicitudObra.FechaFin;
                        ucObrasRendicion.RequisitosAprobacion = solicitudObra.RequisitosAprovacion;
                        ucObrasRendicion.RequisitosIngreso = solicitudObra.RequisitosIngreso;
                        ucObrasRendicion.Personal = SolicitudRecursosEmpleados.GetPersonaHoras_Detalle_EnSolicitud(solicitud.IdSolicitudInicial);
                        ucObrasRendicion.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudObra.IdSolicitud);
                        ucObrasRendicion.Monto = solicitudObra.Presupuesto;
                        ucObrasRendicion.Adjuntos = solicitud.GetAdjuntos();
                        ucObrasRendicion.Responsable = Solicitud.GetResponsable(solicitudObra.IdSolicitud.ToString());
                        ucObrasRendicion.Visible = true;
                        break;
                }
            }
        }
    }
}
