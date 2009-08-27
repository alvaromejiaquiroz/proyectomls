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

public partial class Reportes_MostrarSolicitud : System.Web.UI.Page
{
    int idSol;
    SolicitudCorrectivo SolCor;
    SolicitudPreventivo SolPre;
    SolicitudObra SolObr;
    Sitios sitio;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null)
        {
            idSol = int.Parse(Request.QueryString["id"].ToString());

            Solicitud sol = Solicitud.GetById(idSol);

            /// Cargo la data generica de las solicitudes
            /// 
            if (sol.Reporte == "SI")
            {

                lblNroSolicitud.Text = sol.IdSolicitudInicial.ToString();
            }
            else
            {
                lblNroSolicitud.Text = sol.Id_Solicitud.ToString();
            }
            lblDescripcion.Text = sol.Descripcion;
            lblEstado.Text = sol.Status;
            lblFechaCreacion.Text = sol.FechaCreacion.ToShortDateString();
            lblTipoSolicitud.Text = sol.Tipo.Descripcion.ToString();
            lblCliente.Text = Empresas.FindFirst(Expression.Eq("IdEmpresa", sol.IdCliente)).Nombre;
            lblContacto_Cliente.Text = sol.Contacto;
            lblContacto_Mail.Text = sol.ContactoMail;
            lblContacto_Telefono.Text = sol.ContactoTel;
            lblNRO_OrdenCliente.Text = sol.NroOrdenCte;

            switch (sol.Tipo.IdTiposolicitud.ToString())
            {
                case "1":
                    //Preventiva

                    SolPre = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                    PresentaSolicitudPreventiva(SolPre);
                    break;
                case "2":
                    //Correctiva
                    SolCor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                    PresentaSolicitudCorrectiva(SolCor);
                    break;
                case "6":
                    //Obra
                    SolObr = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                    PresentaSolicitudObra(SolObr);
                    break;
                default:
                    break;
            }

        }
        
    }

    void CargaGrillaPersonal(int idSol)
    {
        //gvPersonal.DataSource = SolicitudRecursosEmpleados.GetPersonasEnSolicitud(idSol);
        gvPersonal.DataSource = SolicitudRecursosEmpleados.GetPersonasEnSolicitud_Simple(idSol);
        gvPersonal.DataBind();

    }
    void CargaGrillaVehiculos(int idSol)
    {
        gvVehiculos.DataSource = SolicitudRecursosVehiculos.GetVehiculosEnSolicitud(idSol);
        gvVehiculos.DataBind();

    }
    void CargaGrillaCorrectiva_ServiciosAfectados(int idSol)
    {
        gvCorrectiva_ServiciosAfectados.DataSource = SolicitudServiciosAfectados.GetServiciosAfectados(idSol);
        gvCorrectiva_ServiciosAfectados.DataBind();

    }
    void CargaGrillaTareas_A_Realizar(int idSol)
    {
        gvSolicitud_Preventiva_Tareas_A_Realizar.DataSource = SolicitudTareas.GetSolicitud_Tareas_Simple(idSol);
        gvSolicitud_Preventiva_Tareas_A_Realizar.DataBind();

    }

    void PresentaSolicitudPreventiva(SolicitudPreventivo SolPre)
    {
        sitio = Sitios.FindFirst(Expression.Eq("IdSitio", SolPre.IdSitio));

        //Panel Preventiva
        lblPvSitio.Text = sitio.Descripcion + " (" + sitio.Direccion + ")";
        lblPvFechaFin.Text = DateTime.Parse(SolPre.FechaFin).ToShortDateString();
        lblPvFechaInicio.Text = DateTime.Parse(SolPre.FechaInicio).ToShortDateString();
        
        //Panel Comun

        lblPresupuesto.Text = SolPre.Presupuesto;
        // Grillas
        CargaGrillaPersonal(SolPre.IdSolicitud);
        CargaGrillaVehiculos(SolPre.IdSolicitud);
        CargaGrillaTareas_A_Realizar(SolPre.IdSolicitud);
        //TODO

        pnlPersonalAsignado.Visible = true;
        pnlVehiculosAsignados.Visible = true;
        pnlSolicitud_Preventiva_Tareas_A_Realizar.Visible = true;
        pnlPreventiva.Visible = true;
    }
    void PresentaSolicitudCorrectiva(SolicitudCorrectivo S)
    {
        

        //Panel Correctivo
        lblCorrectiva_CausaProbable.Text = S.CausaPosible;
        lblCorrectiva_Falla_Reportada.Text = S.FallaReportada;
        lblCorrectiva_FechaNotificacionCliente.Text = S.FechanotificacionCliente.ToString();
        lblCorrectiva_Persona_ReportoFalla.Text = S.PersonaReportoFalla;
        lblCorrectiva_Plazo_Atencion.Text = PlazoRealizacion.FindFirst(Expression.Eq("Id", S.IdPlazoAtencion)).Descripcion;
        
        

        //Panel Comun
        lblPresupuesto.Text = S.Presupuesto;
        
        // Grillas
        CargaGrillaPersonal(S.IdSolicitud);
        CargaGrillaVehiculos(S.IdSolicitud);
        CargaGrillaCorrectiva_ServiciosAfectados(S.IdSolicitud);
        
        //TODO

        pnlPersonalAsignado.Visible = true;
        pnlVehiculosAsignados.Visible = true;
        pnlCorrectiva_ServiciosAfectados.Visible = true;
        pnlCorrectiva.Visible = true;
        

    }
    void PresentaSolicitudObra(SolicitudObra S)
    {


        //Panel Obra
        lblObra_Desc_Tareas.Text = S.DescripcionTareas;
        lblObra_FechaFin.Text = S.FechaFin;
        lblObra_FechaInicio.Text = S.FechaInicio;
        lblObra_Req_Aprovacion.Text = S.RequisitosAprovacion;
        lblObra_Req_Ingreso.Text = S.RequisitosIngreso;
        
        //Panel Comun
        lblPresupuesto.Text = S.Presupuesto;

        // Grillas
        CargaGrillaPersonal(S.IdSolicitud);
        CargaGrillaVehiculos(S.IdSolicitud);
        //CargaGrillaCorrectiva_ServiciosAfectados(S.IdSolicitud);

        //TODO

        pnlPersonalAsignado.Visible = true;
        pnlVehiculosAsignados.Visible = true;
        pnlComun.Visible = true;
        pnlObra.Visible = true;
        
    }

}
