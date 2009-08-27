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

public partial class Solicitudes_MantPreventivo : System.Web.UI.Page
{

    static SolicitudTareas tarea;
    static SolicitudRecursosVehiculos vehiculos;

    protected override void OnInitComplete(EventArgs e)
    {
        Adjuntos1.sol = BiFactory.Sol;
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
            FillTareasHoras();
            FillDatosClientes();

        }
    }
    private void FillDatosClientes()
    {
        SolicitudPreventivo Sp = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        cmbClientes.SelectedValue = BiFactory.Sol.IdCliente.ToString();
        txtContactoCliente.Text = BiFactory.Sol.Contacto;
        txtNroOrdenCliente.Text = BiFactory.Sol.NroOrdenCte;
        txtMailContacto.Text = BiFactory.Sol.ContactoMail;
        txtTelefonoContacto.Text = BiFactory.Sol.ContactoTel;
        txtGastos.Text = Sp.Presupuesto;

        if (Sp!= null)
        {
            
            Sitios unSitio = Sitios.FindOne(Expression.Eq("IdSitio", Sp.IdSitio));
            cboSitios.Enabled = false;
            cboSitios.SelectedIndex = unSitio.IdSitio;
            cboSitios.SelectedValue = unSitio.Descripcion;
            //Adjuntos1.ListaAdjuntos(sol.Id_Solicitud.ToString());
        }


    }
    protected void btnAgregarTarea_Click(object sender, EventArgs e)
    {
       
        SolicitudTareas t;
        
        {
            for (int i = 0; i < lstTareasaRealizar.Items.Count; i++)
            {
                if (lstTareasaRealizar.Items[i].Selected)
                {

                    t = SolicitudTareas.FindOne(
                                        Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud),
                                        Expression.Eq("IdTarea", int.Parse(lstTareasaRealizar.Items[i].Value.ToString())),
                                        Expression.Eq("FechaInicio", jDatePick1.Fecha),
                                        Expression.Eq("FechaFin", jDatePick2.Fecha));
                    if (t == null)
                    {
                        t = new SolicitudTareas();
                        t.IdTarea = int.Parse(lstTareasaRealizar.Items[i].Value.ToString());
                        t.FechaInicio = jDatePick1.Fecha;
                        t.FechaFin = jDatePick2.Fecha;
                        t.IdSolicitud = BiFactory.Sol.Id_Solicitud;
                        t.Save();
                    }
                }
            }
        }
        FillTareas();

    }
    public bool validar()
    {
        string stext = "";

        if (cboSitios.SelectedIndex == -1)
        {
            stext = System.Environment.NewLine + "Debe seleccionar un Sitio";
        }

        if (jDatePick1.Text == "")
        {
            stext += System.Environment.NewLine + "Falta ingresar la Fecha de inicio";
        }

        if (jDatePick2.Text == "")
        {
            stext += System.Environment.NewLine + "Falta ingresar la Fecha de hasta";
        }

        HtmlGenericControl lb = (HtmlGenericControl)Master.FindControl("divMensajes");
        if (lb != null)
        {
            lb.InnerText = stext;
        }

        return stext.Length == 0;
    }
    private void FillTareas()
    {
        gvTareas.DataSource = SolicitudTareas.GetReader(BiFactory.Sol.Id_Solicitud);
        gvTareas.DataKeyNames = new string[] { "Id" };
        gvTareas.DataBind();
    }
    public void CargaComboPersonal()
    {

        string sql;
        cmbEmpleado.Items.Clear();
        cmbEmpleado.Items.Add(new ListItem("Seleccione...", "-1"));
        sql = " Id_Empleados not in (select Id_Empleado from dbo.Solicitud_Recursos_Empleados where id_solicitud = " + BiFactory.Sol.Id_Solicitud.ToString() + ")";

        foreach (Antares.model.Personal p in Antares.model.Personal.FindAll(Expression.Sql(sql)))
        {
            cmbEmpleado.Items.Add(new ListItem(p.Apellido + "," + p.Nombres, p.IdEmpleados.ToString()));
        }
    }
    public void CargaComboVehiculos()
    {

        string sql;
        cmbVehiculo.Items.Clear();
        cmbVehiculo.Items.Add(new ListItem("Seleccione...", "-1"));
        sql = " Id_Vehiculos not in (select Id_Vehiculo from dbo.Solicitud_Recursos_Vehiculos where id_solicitud = " + BiFactory.Sol.Id_Solicitud.ToString() + ")";

        foreach (Antares.model.Vehiculos v in Antares.model.Vehiculos.FindAll(Expression.Sql(sql)))
        {
            cmbVehiculo.Items.Add(new ListItem(v.Marca + " " + v.Modelo + " " + v.Patente, v.IdVehiculos.ToString()));
        }
    }
    public void CargaComboSitios()
    {
        cboSitios.Enabled = true;
        cboSitios.Items.Clear();
        cboSitios.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Sitios sitio in Antares.model.Sitios.FindAll())
        {
            cboSitios.Items.Add(new ListItem(sitio.Descripcion, sitio.IdSitio.ToString()));
        }


    }

    public void CargarCombos()
    {

        CargaComboSitios();
        CargaComboPersonal();
        CargaComboVehiculos();


        foreach (Antares.model.Tareas t in Antares.model.Tareas.FindAll())
        {
            lstTareasaRealizar.Items.Add(new ListItem(t.Tarea, t.Id.ToString()));
        }


        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre + "(" + emp.Localidad + ")", emp.IdEmpresa.ToString()));
        }


    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudTareas t;
        t = SolicitudTareas.FindFirst(Expression.Eq("Id", int.Parse(gvTareas.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        t = null;

        t = SolicitudTareas.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        if (t == null)
        {
            cboSitios.Enabled = true;
        }

        FillTareas();
    }
    protected void gvTareas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idTarea = int.Parse(gvTareas.SelectedValue.ToString());
        tarea = SolicitudTareas.FindFirst(Expression.Eq("Id", idTarea));



        //leno el combo de emplados con las personas de la solicitud
       /*
        cboEmpleados.SourceData = "proc_getEmpleadosFromSolicitud " + BiFactory.Sol.Id_Solicitud;
        cboEmpleados.TipoOrigen = Controles_cboGeneric.TipoSource.Procedure;
        cboEmpleados.NameId = "Id_empleado";
        cboEmpleados.NameDescript = "Empleado";
        cboEmpleados.BindCBO();
        divtareasHoras.Style["display"] = "";
        * */




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
   
    private void FillSolicitudEmpleados()
    {
        gvSolicitudPersonas.DataSource = SolicitudRecursosEmpleados.GetReader(BiFactory.Sol.Id_Solicitud);
        gvSolicitudPersonas.DataKeyNames = new string[] { "Id" };
        gvSolicitudPersonas.DataBind();
    }
    private void FillSolicitudVehiculos()
    {
        gvSolicitudVehiculos.DataSource = SolicitudRecursosVehiculos.GetVehiculosEnSolicitud(BiFactory.Sol.Id_Solicitud);
        gvSolicitudVehiculos.DataKeyNames = new string[] { "Id" };
        gvSolicitudVehiculos.DataBind();
    }
    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        //Solicitud Sol_Original = Solicitud.FindFirst(Expression.Eq("IdSolicitudInicial",BiFactory.Sol.Id_Solicitud));
        Solicitud Sol_Original = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        Sol_Original.Status = eEstados.Realizado.ToString();
        Sol_Original.Update();

        //Solicitud Reporte = Solicitud.GetById(BiFactory.Sol.IdSolicitudInicial);
        Solicitud Reporte = Solicitud.FindFirst(Expression.Eq("IdSolicitudInicial", Sol_Original.Id_Solicitud));
        SolicitudPreventivo preventivo = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", Reporte.Id_Solicitud));
        
            SolicitudPreventivo r = new SolicitudPreventivo();
            r.IdSolicitud = preventivo.IdSolicitud;
            r.Presupuesto = txtGastos.Text; ;
            r.FechaInicio = jDatePick1.Text;
            r.FechaFin = jDatePick2.Text;
            r.IdSitio = cboSitios.SelectedIndex;
            //r.fechaactualizacion = DateTime.Now;

            //Agregar la fecha de solicitud a la solicitud del tipo preventivo, asi tambien a correctivo
            Reporte.Status = eEstados.Realizado.ToString();
            r.Save();

            Response.Redirect("./Mensaje.aspx?Id=" + Sol_Original.Id_Solicitud.ToString() + "&St=true");

    }
    private void SaveSolicitud()
    {
        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        sol.IdCliente = int.Parse(cmbClientes.SelectedValue);
        sol.Contacto = txtContactoCliente.Text;
        sol.NroOrdenCte = txtNroOrdenCliente.Text;
        sol.Status = eEstados.Realizado.ToString();
        sol.Update();
        Response.Redirect("./Solicitudes.aspx");
    }
    protected void btmSaveTarea_Click(object sender, EventArgs e)
    {
        if (tarea != null)
        {
            /*
            TareasHoras t = new TareasHoras();
            t.IdTarea = tarea.Id;
            t.IdTipotarea = tarea.IdTarea;
           // t.IdEmpleados = cboEmpleados.ValueInt;
            t.Duracion = Tiempo1.ValueDbl;
            t.Fecha = jdpFechaTarea.Fecha;
            t.Comentarios = txtComentario.Text;
            t.Save();
           // divtareasHoras.Style["display"] = "none";
            FillTareasHoras();
            */

        }
    }
    private void FillTareasHoras()
    {
        /*
        gvTareasRendidas.DataSource = BiFactory.Sol.GetTareasRendidas();
        gvTareasRendidas.DataBind();
         */
          
    }
    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos t = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        t.Delete();
        FillSolicitudVehiculos();
    }
 
    protected void gvSolicitudVehiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = int.Parse(gvSolicitudVehiculos.SelectedValue.ToString());
        vehiculos = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", id));

       /// divVehiculosHoras.Style["display"] = "";
    }
    protected void gvSolicitudPersonas_selected(object sender, EventArgs e)
    {
        int id = int.Parse(gvSolicitudPersonas.SelectedValue.ToString());
        Personal p = Personal.FindFirst(Expression.Eq("IdEmpleados", id));
       
        ///divVehiculosHoras.Style["display"] = "";
    }
    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.RowIndex].Value.ToString())));
        p.Delete();
        FillSolicitudEmpleados();

    }
}
