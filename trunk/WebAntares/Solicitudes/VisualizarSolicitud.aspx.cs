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

public partial class Solicitudes_VisualizarSolicitud : System.Web.UI.Page
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
                    case (int)TipoSolicitudEnum.MantenimientoPreventivo:
                        SolicitudPreventivo solicitudPreventivo = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucMantenimientoPreventivo.Numero = solicitudPreventivo.IdSolicitud.ToString();
                        ucMantenimientoPreventivo.Titulo = solicitud.Descripcion;
                        ucMantenimientoPreventivo.Estado = solicitud.Status;
                        ucMantenimientoPreventivo.Sitio = Sitios.FindFirst(Expression.Eq("IdSitio", solicitudPreventivo.IdSitio)).Descripcion;
                        ucMantenimientoPreventivo.Tareas = SolicitudTareas.GetReader(solicitudPreventivo.IdSolicitud);
                        ucMantenimientoPreventivo.Personal = SolicitudRecursosEmpleados.GetReader(solicitudPreventivo.IdSolicitud);
                        ucMantenimientoPreventivo.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudPreventivo.IdSolicitud);
                        ucMantenimientoPreventivo.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucMantenimientoPreventivo.ContactoCliente = solicitud.Contacto;
                        ucMantenimientoPreventivo.NroOrden = solicitud.NroOrdenCte;
                        ucMantenimientoPreventivo.TelefonoContacto = solicitud.ContactoTel;
                        ucMantenimientoPreventivo.MailContacto = solicitud.ContactoMail;
                        ucMantenimientoPreventivo.Adjuntos = solicitud.GetAdjuntos();
                        ucMantenimientoPreventivo.Monto = solicitudPreventivo.Presupuesto;
                        ucMantenimientoPreventivo.Visible = true;
                        break;
                    case (int)TipoSolicitudEnum.MantenimientoCorrectivo:
                        SolicitudCorrectivo solicitudCorrectivo = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucMantenimientoCorrectivo.Numero = solicitudCorrectivo.IdSolicitud.ToString();
                        ucMantenimientoCorrectivo.Titulo = solicitud.Descripcion;
                        ucMantenimientoCorrectivo.Estado = solicitud.Status;
                        ucMantenimientoCorrectivo.ReportoFalla = solicitudCorrectivo.PersonaReportoFalla;
                        ucMantenimientoCorrectivo.CausaProbable = solicitudCorrectivo.CausaPosible;
                        ucMantenimientoCorrectivo.FechaReporte = solicitudCorrectivo.FechanotificacionCliente.ToString("dd/MM/yyyy HH:mm");
                        ucMantenimientoCorrectivo.Falla = solicitudCorrectivo.FallaReportada;
                        ucMantenimientoCorrectivo.Servicios = SolicitudServiciosAfectados.GetServiciosAfectados(solicitudCorrectivo.IdSolicitud);
                        ucMantenimientoCorrectivo.Plazo = PlazoRealizacion.FindFirst(Expression.Eq("Id", solicitudCorrectivo.IdPlazoAtencion)).Descripcion;
                        ucMantenimientoCorrectivo.Personal = SolicitudRecursosEmpleados.GetReader(solicitudCorrectivo.IdSolicitud);
                        ucMantenimientoCorrectivo.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudCorrectivo.IdSolicitud);
                        ucMantenimientoCorrectivo.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucMantenimientoCorrectivo.ContactoCliente = solicitud.Contacto;
                        ucMantenimientoCorrectivo.NroOrden = solicitud.NroOrdenCte;
                        ucMantenimientoCorrectivo.TelefonoContacto = solicitud.ContactoTel;
                        ucMantenimientoCorrectivo.MailContacto = solicitud.ContactoMail;
                        ucMantenimientoCorrectivo.Adjuntos = solicitud.GetAdjuntos();
                        ucMantenimientoCorrectivo.Monto = solicitudCorrectivo.Presupuesto;
                        ucMantenimientoCorrectivo.Visible = true;
                        break;
                    case 6:
                        SolicitudObra solicitudObra = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucObras.Numero = solicitudObra.IdSolicitud.ToString();
                        ucObras.Titulo = solicitud.Descripcion;
                        ucObras.Estado = solicitud.Status;
                        ucObras.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucObras.NroOrden = solicitud.NroOrdenCte;
                        ucObras.Contacto = solicitud.Contacto;
                        ucObras.MailContacto = solicitud.ContactoMail;
                        ucObras.TelefonoContacto = solicitud.ContactoTel;
                        ucObras.DescripcionTareas = solicitudObra.DescripcionTareas;
                        ucObras.FechaInicio = solicitudObra.FechaInicio;
                        ucObras.FechaEntrega = solicitudObra.FechaFin;
                        ucObras.RequisitosAprobacion = solicitudObra.RequisitosAprovacion;
                        ucObras.RequisitosIngreso = solicitudObra.RequisitosIngreso;
                        ucObras.Personal = SolicitudRecursosEmpleados.GetReader(solicitudObra.IdSolicitud);
                        ucObras.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudObra.IdSolicitud);
                        ucObras.Monto = solicitudObra.Presupuesto;
                        ucObras.Adjuntos = solicitud.GetAdjuntos();
                        ucObras.Visible = true;
                        break;
                }
            }
        }
    }
}
