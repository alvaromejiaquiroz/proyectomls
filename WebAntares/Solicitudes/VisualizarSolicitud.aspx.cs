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
                decimal gastos = Solicitud.Valida_Gastos_Ingresados_Solicitud(solicitud.Id_Solicitud);

                switch (solicitud.Tipo.IdTiposolicitud)
                {
                    case (int)EnumTipoSolicitud.MantenimientoPreventivo:
                        SolicitudPreventivo solicitudPreventivo = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucMantenimientoPreventivo.Numero = solicitudPreventivo.IdSolicitud.ToString();
                        ucMantenimientoPreventivo.Titulo = solicitud.Descripcion;
                        ucMantenimientoPreventivo.Estado = solicitud.Status;
                        switch (solicitud.Status)
                        {
                            case "Reprogramado":
                                ucMantenimientoPreventivo.HabilitarDivReprogramacion = true;
                                ucMantenimientoPreventivo.Fecha_Reprogramacion = solicitud.FechaReprogramacion.ToString("dd/MM/yyyy");
                                ucMantenimientoPreventivo.Aprobador_Reprogramacion = solicitud.AprobadorReprosusp;
                                ucMantenimientoPreventivo.Motivo_Reprogramacion = solicitud.Causa.ToString();
                            break;
                        }
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
                        ucMantenimientoPreventivo.Monto =  gastos.ToString();
                        //ucMantenimientoPreventivo.Gastos = SolicitudGastos.FindAll(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud)); 
                        ucMantenimientoPreventivo.Gastos = SolicitudGastos.GetGastosSolicitud(solicitud.Id_Solicitud); 

                        SolicitudArchivoCalidad S = SolicitudArchivoCalidad.FindOne(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        if (S != null)
                        {
                            ucMantenimientoPreventivo.HabilitarArchivoCalidad = true;
                            ucMantenimientoPreventivo.Calidad = CalidadArchivos.FindAll(Expression.Eq("Id", S.IdCalidadArchivo)); ;
                        }
            
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
                        ucMantenimientoCorrectivo.Sitio = Sitios.FindFirst(Expression.IdEq(solicitudCorrectivo.IdSitio)).Nombre.ToString();
                        ucMantenimientoCorrectivo.Plazo = PlazoRealizacion.FindFirst(Expression.Eq("Id", solicitudCorrectivo.IdPlazoAtencion)).Descripcion;
                        string penaliza;
                        if (solicitudCorrectivo.Penaliza) { penaliza = "Genera Penalización"; } else { penaliza = "No Genera Penalizacion"; }
                        ucMantenimientoCorrectivo.Penaliza = penaliza;


                        ucMantenimientoCorrectivo.Personal = SolicitudRecursosEmpleados.GetReader(solicitudCorrectivo.IdSolicitud);
                        ucMantenimientoCorrectivo.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudCorrectivo.IdSolicitud);
                        ucMantenimientoCorrectivo.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucMantenimientoCorrectivo.ContactoCliente = solicitud.Contacto;
                        ucMantenimientoCorrectivo.NroOrden = solicitud.NroOrdenCte;
                        ucMantenimientoCorrectivo.TelefonoContacto = solicitud.ContactoTel;
                        ucMantenimientoCorrectivo.MailContacto = solicitud.ContactoMail;
                        ucMantenimientoCorrectivo.Adjuntos = solicitud.GetAdjuntos();
                        ucMantenimientoCorrectivo.Monto =  gastos.ToString();
                        //ucMantenimientoCorrectivo.Gastos = SolicitudGastos.FindAll(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud)); 
                        ucMantenimientoCorrectivo.Gastos = SolicitudGastos.GetGastosSolicitud(solicitud.Id_Solicitud); 
                        ucMantenimientoCorrectivo.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.Obras:
                        SolicitudObra solicitudObra = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucObras.Numero = solicitudObra.IdSolicitud.ToString();
                        ucObras.Titulo = solicitud.Descripcion;
                        ucObras.Estado = solicitud.Status;
                        switch (solicitud.Status)
                        {
                            case "Reprogramado":
                                ucObras.HabilitarDivReprogramacion = true;
                                ucObras.Fecha_Reprogramacion = solicitud.FechaReprogramacion.ToString("dd/MM/yyyy");
                                ucObras.Aprobador_Reprogramacion = solicitud.AprobadorReprosusp;
                                ucObras.Motivo_Reprogramacion = solicitud.Causa.ToString();
                                break;
                        }
                        ucObras.CodigoObra = solicitudObra.NroObra.ToString();
                        ucObras.Cliente = Empresas.FindFirst(Expression.Eq("IdEmpresa", solicitud.IdCliente)).Nombre;
                        ucObras.NroOrden = solicitud.NroOrdenCte;
                        ucObras.Contacto = solicitud.Contacto;
                        ucObras.MailContacto = solicitud.ContactoMail;
                        ucObras.TelefonoContacto = solicitud.ContactoTel;
                        ucObras.DescripcionTareas = solicitudObra.DescripcionTareas;
                        ucObras.FechaInicio = solicitudObra.FechaInicio.ToString("dd/MM/yyyy");
                        ucObras.FechaEntrega = solicitudObra.FechaFin.ToString("dd/MM/yyyy");
                        ucObras.RequisitosAprobacion = solicitudObra.RequisitosAprovacion;
                        ucObras.RequisitosIngreso = solicitudObra.RequisitosIngreso;
                        ucObras.Personal = SolicitudRecursosEmpleados.GetReader(solicitudObra.IdSolicitud);
                        ucObras.Vehiculos = SolicitudRecursosVehiculos.GetReader(solicitudObra.IdSolicitud);
                        ucObras.Monto =  gastos.ToString();
                        //ucObras.Gastos = SolicitudGastos.FindAll(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucObras.Gastos = SolicitudGastos.GetGastosSolicitud(solicitudObra.IdSolicitud); 
                        ucObras.Adjuntos = solicitud.GetAdjuntos();
                        ucObras.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.Capacitacion:
                        SolicitudCapacitacion solicitudCapacitacion = SolicitudCapacitacion.FindOne(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucCapacitacion.Sol = solicitud;
                        ucCapacitacion.IdEmpleado = solicitudCapacitacion.IdEmpleado;

                        ucCapacitacion.Numero = solicitudCapacitacion.IdSolicitud.ToString();
                        ucCapacitacion.Titulo = solicitudCapacitacion.Descripcion;
                        ucCapacitacion.Nivel = solicitudCapacitacion.Nivel;
                        //ACA CARGO LA INFO QUE VIENE DE UN STORED CUSTOM , COMO LA FECHA INICIO Y FIN Y LAS HORAS
                        ucCapacitacion.CargaInfoSolicitud();
                        ucCapacitacion.AreaEstudios = solicitudCapacitacion.AreaEstudio;
                        ucCapacitacion.Instructor = solicitudCapacitacion.Instructor;
                        ucCapacitacion.EntidadEducativa = solicitudCapacitacion.EntidadEducativa;
                        ucCapacitacion.PuntuacionExamen = solicitudCapacitacion.PuntuacionExamen;
                        ucCapacitacion.Sol = solicitud;
                        ucCapacitacion.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.FrancosCompensatorios:
                        SolicitudFrancosCompensatorios solicitudFrancosCompensatorios = SolicitudFrancosCompensatorios.FindOne(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucFrancosCompensatorios.Numero = solicitudFrancosCompensatorios.IdSolicitud.ToString();
                        ucFrancosCompensatorios.Titulo = solicitud.Descripcion;
                        ucFrancosCompensatorios.Fecha = solicitudFrancosCompensatorios.FechaInicio.ToShortDateString();
                        ucFrancosCompensatorios.Duracion = solicitudFrancosCompensatorios.Duracion.ToString();
                        ucFrancosCompensatorios.Descripcion = solicitudFrancosCompensatorios.Descripcion;
                        ucFrancosCompensatorios.ConsentimientoDe = solicitudFrancosCompensatorios.ConElConsentimiento;
                        ucFrancosCompensatorios.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.Licencias:
                        SolicitudLicencias solicitudLicencias = SolicitudLicencias.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucLicencias.Numero = solicitudLicencias.IdSolicitud.ToString();
                        ucLicencias.Titulo = solicitud.Descripcion;
                        ucLicencias.Tipo = TipoLicencia.FindFirst(Expression.Eq("Id", solicitudLicencias.IdTipolicencia)).Descripcion;
                        ucLicencias.FechaInicio = solicitudLicencias.FechaInicio.ToShortDateString();
                        ucLicencias.FechaFin = solicitudLicencias.FechaFin.ToShortDateString();
                        ucLicencias.Duracion = solicitudLicencias.Duracion.ToString();
                        ucLicencias.Descripcion = solicitudLicencias.Descripcion;
                        ucLicencias.Visible = true;
                        break;
                    case (int)EnumTipoSolicitud.TareasGenerales:
                        SolicitudTareasGenerales solicitudTareasGenerales = SolicitudTareasGenerales.FindFirst(Expression.Eq("IdSolicitud", solicitud.Id_Solicitud));
                        ucTareasGenerales.Numero = solicitudTareasGenerales.IdSolicitud.ToString();
                        ucTareasGenerales.Titulo = solicitud.Descripcion;
                        ucTareasGenerales.Tipo = TipoTarea.FindFirst(Expression.Eq("Id", solicitudTareasGenerales.IdTipotarea)).Descripcion;
                        ucTareasGenerales.Duracion = Solicitud.Get_SumaHoras_X_Persona_X_Solicitud(solicitudTareasGenerales.IdSolicitud, solicitudTareasGenerales.IdEmpleado).ToString();
                        ucTareasGenerales.Descripcion = solicitudTareasGenerales.Descripcion;
                        ucTareasGenerales.FillGridHoras = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(solicitudTareasGenerales.IdSolicitud, solicitudTareasGenerales.IdEmpleado);
                        ucTareasGenerales.Visible = true;
                        break;
                }
            }
        }
    }
}
