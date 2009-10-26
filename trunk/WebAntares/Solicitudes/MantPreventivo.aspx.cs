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
using System.Collections;

/// <summary>
/// Mantenimiento PREVENTIVO
/// 
/// </summary>
public partial class Solicitudes_MantPreventivo : System.Web.UI.Page
{
    protected override void OnInitComplete(EventArgs e)
    {
        ucAdjuntos.sol = BiFactory.Sol;
        base.OnInitComplete(e);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            FillTareas();
            FillSolicitudEmpleados();
            FillSolicitudVehiculos();
            LoadDataComplementaria();
            FillAdjuntosCalidad();
            string accion;
            
            if (Session["Accion"] != null)
            {
                accion = Session["Accion"].ToString();
                if (accion == "e")
                {
                    Solicitud sol = BiFactory.Sol;
                    SolicitudPreventivo sol_p = SolicitudPreventivo.FindOne(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                   
                    cboSitios.Enabled = false;
                    txtDesde.Enabled = false;
                    txtHasta.Enabled = false;
                    lstTareas.Enabled = false;
                    btnAgregarTarea.Enabled = false;
                    Session["Accion"] = null;
                }
            }
        }

    }

    private void LoadDataComplementaria()
    {
        Solicitud sol = BiFactory.Sol;
        SolicitudPreventivo sol_p = SolicitudPreventivo.FindOne(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
        if (sol_p != null)
        {
            
            cmbClientes.SelectedValue = sol.IdCliente.ToString();
            Sitios unSitio = Sitios.FindOne(Expression.Eq("IdSitio", sol_p.IdSitio));
            cboSitios.SelectedIndex = unSitio.IdSitio;
            cboSitios.SelectedValue = unSitio.Descripcion;

            SolicitudTareas sT = SolicitudTareas.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
            if (sT != null)
            {
                txtDesde.Text = sT.FechaInicio.ToShortDateString();
                txtHasta.Text = sT.FechaFin.ToShortDateString();
            }

            txtContactoCliente.Text = sol.Contacto;
            txtMailContacto.Text = sol.ContactoMail;
            txtNroOrdenCliente.Text = sol.NroOrdenCte;
            txtTelefonoContacto.Text = sol.ContactoTel;
            
            txtPresupuesto.Text = "";
            lblGastos.Visible = true;
            lblGastos.Text = "$"+sol_p.Presupuesto;

            ucAdjuntos.ListaAdjuntos(sol.Id_Solicitud.ToString());
        }

    }

    protected void btnAgregarTarea_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int id_Sol = BiFactory.Sol.Id_Solicitud;
            int id_Tarea;

            for (int i = 0; i < lstTareas.Items.Count; i++)
            {
                if (lstTareas.Items[i].Selected)
                {
                    id_Tarea = int.Parse(lstTareas.Items[i].Value);

                    if (!SolicitudTareas.ExisteTareaEnSolicitud( id_Sol , id_Tarea ))
                    {
                        SolicitudTareas t = new SolicitudTareas();
                        t.IdTarea = id_Tarea;
                        t.FechaInicio = DateTime.Parse(txtDesde.Text);
                        t.FechaFin = DateTime.Parse(txtHasta.Text);
                        t.IdSolicitud = id_Sol;
                        t.Save();
                        cboSitios.Enabled = false;
                    }
                }
            }
            //Aca asocio el/los registros de calidad necesarios de la solicitud
            AdjuntaRegistroCalidad();
            FillAdjuntosCalidad();
            FillTareas();
        }
    }
        
    private void FillTareas()
    {
        gvTareas.DataSource = SolicitudTareas.GetReader(BiFactory.Sol.Id_Solicitud);
        gvTareas.DataKeyNames = new string[] { "Id" };
        gvTareas.DataBind();
    }

    public void CargaComboSitios()
    {
        cboSitios.Enabled = true;
        cboSitios.Items.Clear();
        cboSitios.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Sitios sitio in Antares.model.Sitios.FindAll(Expression.Eq("Activo",true)))
        {
            cboSitios.Items.Add(new ListItem(sitio.Nombre, sitio.IdSitio.ToString()));
        }
        
    }

    public void CargarCombos()
    {
        CargaComboSitios();
        txtDesde.Text = DateTime.Today.ToString("dd/MM/yyyy");
   
        foreach (Antares.model.Tareas t in Antares.model.Tareas.FindAll())
        {
            lstTareas.Items.Add(new ListItem(t.Tarea, t.Id.ToString()));
        }

        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.GetVehiculosActivos())
        {
            lstVehiculos.Items.Add(new ListItem(vehi.Marca + " " + vehi.Modelo + " " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }
        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre , emp.IdEmpresa.ToString()));
        }

        cmbResponsable.Items.Clear();
        cmbResponsable.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal responsable in Antares.model.Personal.GetPersonalActivo())
        {
            cmbResponsable.Items.Add(new ListItem(responsable.Apellido + ", " + responsable.Nombres, responsable.IdEmpleados.ToString()));
        }
    }

    protected void gvTareas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudTareas t;
        int idSolicitud =int.Parse(gvTareas.DataKeys[e.RowIndex].Value.ToString());
        t = SolicitudTareas.FindFirst(Expression.Eq("Id",idSolicitud ));
        
        t.Delete();
        
        SolicitudArchivoCalidad  S = SolicitudArchivoCalidad.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        if (S != null)
        {
            S.Delete();
            FillAdjuntosCalidad();
        }

        t = SolicitudTareas.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        if (t == null)
        {
            cboSitios.Enabled = true;
        }

        FillTareas();
    }
        
    protected void btnAsignaEmpleadoSolicitud_Click(object sender, EventArgs e)
    {
        int id_sol = BiFactory.Sol.Id_Solicitud;
        int id_empleado;

        if (cmbResponsable.SelectedIndex >= 0)
        {
            id_empleado = int.Parse(cmbResponsable.SelectedValue);

            if (!SolicitudRecursosEmpleados.ExisteEmpleadoEnSolicitud(id_sol, id_empleado))
            {
                SolicitudRecursosEmpleados.QuitaResponsableSolicitud(id_sol);
                SolicitudRecursosEmpleados t = new SolicitudRecursosEmpleados();

                t.IdEmpleado = id_empleado;
                t.IdSolicitud = id_sol;
                t.Responsable = true;
                t.Save();
            }
        }

        for (int i = 0; i < lstEmpleadosSolicitud.Items.Count; i++)
        {
            if (lstEmpleadosSolicitud.Items[i].Selected)
            {
                id_empleado = Int32.Parse(lstEmpleadosSolicitud.Items[i].Value);

                if (!SolicitudRecursosEmpleados.ExisteEmpleadoEnSolicitud(id_sol, id_empleado))
                {
                    SolicitudRecursosEmpleados t = new SolicitudRecursosEmpleados();

                    t.IdEmpleado = id_empleado;
                    t.IdSolicitud = id_sol;
                    t.Responsable = false;
                    t.Save();
                }
            }

        }
        FillSolicitudEmpleados();
    }

    protected void btnAsignaVehiculoSolicitud_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            int id_Sol = BiFactory.Sol.Id_Solicitud;
            int id_Vehiculo;
            for (int i = 0; i < lstVehiculos.Items.Count; i++)
            {
                if (lstVehiculos.Items[i].Selected)
                {
                    id_Vehiculo = int.Parse(lstVehiculos.Items[i].Value);
                    if (!SolicitudRecursosVehiculos.ExisteVehiculoEnSolicitud(id_Sol, id_Vehiculo))
                    {
                        SolicitudRecursosVehiculos t = new SolicitudRecursosVehiculos();
                        t.IdVehiculo = int.Parse(lstVehiculos.Items[i].Value.ToString());
                        t.IdSolicitud = BiFactory.Sol.Id_Solicitud;
                        t.Save();
                    }
                }
            }
            FillSolicitudVehiculos();
        }
    }

    private void FillSolicitudEmpleados()
    {
        DataTable dt = new DataTable();
        gvSolicitudPersonas.DataSource = dt;
        gvSolicitudPersonas.DataBind();

        gvSolicitudPersonas.DataSource = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
        gvSolicitudPersonas.DataKeyNames = new string[] { "Id" };
        gvSolicitudPersonas.DataBind();
    }

    private void FillSolicitudVehiculos()
    {
        gvSolicitudVehiculos.DataSource = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
        gvSolicitudVehiculos.DataKeyNames = new string[] { "Id" };
        gvSolicitudVehiculos.DataBind();
    }
    
    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        if (IsValid && EsSolicitudValida())
        {
            Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            
            sol.IdCliente = int.Parse(cmbClientes.SelectedValue);
            sol.Contacto = txtContactoCliente.Text;
            sol.NroOrdenCte = txtNroOrdenCliente.Text;
            sol.Status = eEstados.Pendiente.ToString();
            sol.ContactoMail = txtMailContacto.Text;
            sol.ContactoTel = txtTelefonoContacto.Text;
            
            SolicitudPreventivo Sol_P = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));

            if (Sol_P == null)
            {
                Sol_P = new SolicitudPreventivo();
                Sol_P.IdSolicitud = sol.Id_Solicitud;
            }

            sol.Status = eEstados.Pendiente.ToString();
            Sol_P.IdSitio = int.Parse(cboSitios.SelectedValue);
            
            SolicitudTareas st = SolicitudTareas.FindOne(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
            Sol_P.FechaFin = st.FechaFin;
            Sol_P.FechaInicio = st.FechaInicio;
            
            if (txtPresupuesto.Text == "")
            {
                Sol_P.Presupuesto = lblGastos.Text.Replace("$","");
            }
            else
            {

                Sol_P.Presupuesto = txtPresupuesto.Text;
            }
            sol.Save();
            Sol_P.Save();
            
            pnlMantenimientoPreventivo.Visible = false;

            ucMantenimientoPreventivo.Numero = Sol_P.IdSolicitud.ToString();
            ucMantenimientoPreventivo.Titulo = sol.Descripcion;
            ucMantenimientoPreventivo.Estado = sol.Status;
            ucMantenimientoPreventivo.Sitio = cboSitios.SelectedItem.Text;
            ucMantenimientoPreventivo.Tareas = SolicitudTareas.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoPreventivo.Personal = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoPreventivo.Vehiculos = SolicitudRecursosVehiculos.GetReader(BiFactory.Sol.Id_Solicitud);
            ucMantenimientoPreventivo.Cliente = cmbClientes.SelectedItem.Text;
            ucMantenimientoPreventivo.ContactoCliente = sol.Contacto;
            ucMantenimientoPreventivo.NroOrden = sol.NroOrdenCte;
            ucMantenimientoPreventivo.TelefonoContacto = sol.ContactoTel;
            ucMantenimientoPreventivo.MailContacto = sol.ContactoMail;
            ucMantenimientoPreventivo.Adjuntos = sol.GetAdjuntos();

            SolicitudArchivoCalidad S = SolicitudArchivoCalidad.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
            if (S != null)
            {
                ucMantenimientoPreventivo.HabilitarArchivoCalidad = true;
                ucMantenimientoPreventivo.Calidad = CalidadArchivos.FindAll(Expression.Eq("Id", S.IdCalidadArchivo)); ;
            }

            ucMantenimientoPreventivo.Monto = Sol_P.Presupuesto;

            ucMantenimientoPreventivo.Visible = true;

            WebAntares.AntaresHelper.NotificaSolicitud(BiFactory.Sol.Id_Solicitud);
        }
    }
        
    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.RowIndex].Value.ToString())));
        p.Delete();
        FillSolicitudEmpleados();

    }

    protected void gvSolicitudVehiculos_rowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos v = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        v.Delete();
        FillSolicitudVehiculos();
    }

    protected void cmbResponsable_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idResponsable = int.Parse(cmbResponsable.SelectedValue);
        if (idResponsable == -1)
        {
            litPersonal.Visible = false;
            lstEmpleadosSolicitud.Visible = false;
            btnAsignaEmpleadoSolicitud.Visible = false;
        }
        else
        {
            lstEmpleadosSolicitud.Items.Clear();
            CargaListaEmpleados(idResponsable);
            litPersonal.Visible = true;
            lstEmpleadosSolicitud.Visible = true;
            btnAsignaEmpleadoSolicitud.Visible = true;
        }
    }

    protected void CargaListaEmpleados(int IdDelResponsable)
    {
        
        foreach (Antares.model.Personal persona in Antares.model.Personal.GetPersonalActivo())
        {
            if (persona.IdEmpleados != IdDelResponsable)
            {
                lstEmpleadosSolicitud.Items.Add(new ListItem(persona.Apellido + ", " + persona.Nombres, persona.IdEmpleados.ToString()));
            }
        }
    }
    
    protected bool EsSolicitudValida()
    {
        bool esValida = true;
        List<string> errores = new List<string>();
        string idSol = BiFactory.Sol.Id_Solicitud.ToString();
        if (gvTareas.Rows.Count == 0)
        {
            esValida = false;
            errores.Add("Debe seleccionar al menos una tarea.");
        }
        if (!Solicitud.TieneResponsable(idSol))
        {
            esValida = false;
            errores.Add("Debe asignar al menos un responsable.");
        }
        if (gvSolicitudVehiculos.Rows.Count == 0)
        {
            esValida = false;
            errores.Add("Debe asignar al menos un vehículo.");
        }
        if (!esValida)
        {
            blErrores.DataSource = errores;
            blErrores.DataBind();
        }
        return esValida;
    }

    protected void cvTareas_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = lstTareas.SelectedIndex >= 0;
    }

    protected void cvVehiculos_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = lstVehiculos.SelectedIndex >= 0;
    }
    
    protected void AdjuntaRegistroCalidad()
    {
        int idSitio = int.Parse(cboSitios.SelectedValue);
        int idSemestre = int.Parse(lstTareas.SelectedValue);
        SolicitudArchivoCalidad adjunto;
        CalidadArchivos  archivoCalidad  =  CalidadArchivos.FindOne(Expression.Eq("IdSitio", idSitio), Expression.Eq("IdTarea", idSemestre)); ;
        if (archivoCalidad != null)
        {
            adjunto =  new SolicitudArchivoCalidad();
            adjunto.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            adjunto.IdCalidadArchivo = archivoCalidad.Id;
            adjunto.Save();
        }

    }

    protected void FillAdjuntosCalidad()
    {
        SolicitudArchivoCalidad  S = SolicitudArchivoCalidad.FindOne(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        if (S != null)
        {
            gvCalidad.DataSource = CalidadArchivos.FindAll(Expression.Eq("Id", S.IdCalidadArchivo));
            gvCalidad.DataKeyNames = new string[] { "Id" };
            gvCalidad.DataBind();
        }
        else
        {
            gvCalidad.DataSource= null;
            gvCalidad.DataBind();


        }

    }
    
    protected void gvCalidad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 Id = Int32.Parse(e.CommandArgument.ToString());
        CalidadArchivos Archivo = CalidadArchivos.FindOne(Expression.Eq("Id", Id));
        
        switch (e.CommandName)
        {
            case "Descargar":
                System.IO.FileInfo file = new System.IO.FileInfo(Archivo.RutaArchivo);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Archivo.NombreArchivo);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
                break;
        }
    }

    protected void tcMantenimientoPreventivo_ActiveTabChanged(object sender, EventArgs e)
    {

    }

    protected void btnAceptarGastos_Click(object sender, ImageClickEventArgs e)
    {
        lblGastos.Visible = true;
        lblGastos.Text = "$" + txtPresupuesto.Text;
        txtPresupuesto.Text = "";

    }

    protected void gvTareas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     
    }
    protected void gvTareas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        string accion ;
        if (Session["Accion"] != null)
        {
            accion  = Session["Accion"].ToString();
            if (accion == "e")
            {
                e.Row.Cells[6].Visible = false;
            }
        }

    }
   
   
}
