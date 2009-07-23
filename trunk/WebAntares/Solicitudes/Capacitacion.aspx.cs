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
using WebAntares;
using NHibernate.Expression;



public partial class Solicitudes_Capacitacion : System.Web.UI.Page
{
    static Antares.model.SolicitudCapacitacion sol_Cap;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            UsuariosEmpleados ue = BiFactory.Empleado;

            if (ue != null)
            {

                string id = Request.QueryString["id"];
                CargarCombos();

                if (id != null)
                {
                    sol_Cap = SolicitudCapacitacion.FindFirst(Expression.Eq("IdSolicitud", int.Parse(id)));
                    if (sol_Cap != null)
                    {
                        BiFactory.Sol = Solicitud.GetById(sol_Cap.IdSolicitud);
                        cmbNivelCapacitacion.SelectedValue = sol_Cap.Nivel;
                        txtAreaEstudios.Text = sol_Cap.AreaEstudio;
                        txtEntidad.Text = sol_Cap.EntidadEducativa;
                        txtHoras.Text = sol_Cap.Duracion;
                        txtInstructor.Text = sol_Cap.Instructor;
                        txtPuntuacionExamen.Text = sol_Cap.PuntuacionExamen;
                        dtpDesde.Text = sol_Cap.FechaInicio;
                        dtpHasta.Text = sol_Cap.FechaFin;
                    }
                }
            }
           else
                {
                    HtmlGenericControl lb = (HtmlGenericControl)Master.FindControl("divMensajes");
                    if (lb != null)
                    {
                        lb.InnerText = "El Usuario NO esta relacionado con un Empleado";
                    }

                }
            }
    
    }
            
            
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        SolicitudCapacitacion Cap  = SolicitudCapacitacion.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        
        if (Cap == null)
        {
            Cap = new SolicitudCapacitacion();
            Cap.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            Cap.Descripcion = BiFactory.Sol.Descripcion;

        }
        
        Cap.FechaInicio = dtpDesde.Text;
        Cap.FechaFin = dtpHasta.Text;
        Cap.Nivel = cmbNivelCapacitacion.SelectedValue;
        Cap.Instructor = txtInstructor.Text;
        Cap.PuntuacionExamen = txtPuntuacionExamen.Text;
        Cap.Duracion = txtHoras.Text;
        Cap.EntidadEducativa = txtEntidad.Text;
        Cap.AreaEstudio = txtAreaEstudios.Text;
        Cap.IdEmpleado = BiFactory.Empleado.IdEmpleados;
        Cap.SaveAndFlush();
        Response.Redirect("./ListaCapacitacion.aspx");
        Sol = null;

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("./ListaCapacitacion.aspx");

    }
    protected void LimpiaFormulario() 
    {

        txtAreaEstudios.Text = "";
        txtEntidad.Text = "";
        txtHoras.Text = "";
        txtInstructor.Text = "";
        txtPuntuacionExamen.Text = "";
        dtpDesde.Text = "";
        dtpHasta.Text = "";
        

    }
    private void CargarCombos()
    {
        
            cmbNivelCapacitacion.Items.Add("Basico");
            cmbNivelCapacitacion.Items.Add("Intermedio");
            cmbNivelCapacitacion.Items.Add("Avanzado");
            cmbNivelCapacitacion.Items.Add("Experto");
    }

}