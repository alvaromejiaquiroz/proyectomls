﻿using System;
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
                    case (int)EnumTipoSolicitud.MantenimientoPreventivo:
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
                    case (int)EnumTipoSolicitud.MantenimientoCorrectivo:
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
                    case (int)EnumTipoSolicitud.Obras:
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
                    case (int)EnumTipoSolicitud.Capacitacion:
                        SolicitudCapacitacion solicitudCapacitacion = SolicitudCapacitacion.FindOne(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucCapacitacion.Numero = solicitudCapacitacion.IdSolicitud.ToString();
                        ucCapacitacion.Titulo = solicitudCapacitacion.Descripcion;
                        ucCapacitacion.Nivel = solicitudCapacitacion.Nivel;
                        ucCapacitacion.FechaInicio = solicitudCapacitacion.FechaInicio;
                        ucCapacitacion.FechaFin = solicitudCapacitacion.FechaFin;
                        ucCapacitacion.Duracion = solicitudCapacitacion.Duracion;
                        ucCapacitacion.AreaEstudios = solicitudCapacitacion.AreaEstudio;
                        ucCapacitacion.Instructor = solicitudCapacitacion.Instructor;
                        ucCapacitacion.EntidadEducativa = solicitudCapacitacion.EntidadEducativa;
                        ucCapacitacion.PuntuacionExamen = solicitudCapacitacion.PuntuacionExamen;
                        ucCapacitacion.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.FrancosCompensatorios:
                        SolicitudFrancosCompensatorios solicitudFrancosCompensatorios = SolicitudFrancosCompensatorios.FindOne(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucFrancosCompensatorios.Numero = solicitudFrancosCompensatorios.IdSolicitud.ToString();
                        ucFrancosCompensatorios.Titulo = solicitud.Descripcion;
                        ucFrancosCompensatorios.Fecha = solicitudFrancosCompensatorios.FechaInicio;
                        ucFrancosCompensatorios.Duracion = solicitudFrancosCompensatorios.Duracion;
                        ucFrancosCompensatorios.Descripcion = solicitudFrancosCompensatorios.Descripcion;
                        ucFrancosCompensatorios.ConsentimientoDe = solicitudFrancosCompensatorios.ConElConsentimiento;
                        ucFrancosCompensatorios.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.Licencias:
                        SolicitudLicencias solicitudLicencias = SolicitudLicencias.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucLicencias.Numero = solicitudLicencias.IdSolicitud.ToString();
                        ucLicencias.Titulo = solicitud.Descripcion;
                        ucLicencias.Tipo = TipoLicencia.FindFirst(Expression.Eq("Id", solicitudLicencias.IdTipolicencia)).Descripcion;
                        ucLicencias.FechaInicio = solicitudLicencias.FechaInicio;
                        ucLicencias.FechaFin = solicitudLicencias.FechaFin;
                        ucLicencias.Duracion = solicitudLicencias.Duracion;
                        ucLicencias.Descripcion = solicitudLicencias.Descripcion;
                        ucLicencias.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.TareasGenerales:
                        SolicitudTareasGenerales solicitudTareasGenerales = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucTareasGenerales.Numero = solicitudTareasGenerales.IdSolicitud.ToString();
                        ucTareasGenerales.Titulo = solicitud.Descripcion;
                        ucTareasGenerales.Tipo = TipoTarea.FindFirst(Expression.Eq("Id", solicitudTareasGenerales.IdTipotarea)).Descripcion;
                        ucTareasGenerales.FechaInicio = solicitudTareasGenerales.FechaInicio;
                        ucTareasGenerales.FechaFin = solicitudTareasGenerales.FechaFin;
                        ucTareasGenerales.Duracion = solicitudTareasGenerales.Duracion;
                        ucTareasGenerales.Descripcion = solicitudTareasGenerales.Descripcion;
                        ucTareasGenerales.Visible = true;
                        break;
                }
            }
        }
    }
}
