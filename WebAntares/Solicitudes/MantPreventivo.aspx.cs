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
/// <summary>
/// Mantenimiento PREVENTIVO
/// 
/// </summary>
public partial class Solicitudes_MantPreventivo : System.Web.UI.Page
{
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
            LoadDataComplementaria();


            string accion;
            if (Session["Accion"] != null)
            {
                accion = Session["Accion"].ToString();
                if (accion == "e")
                {
                    Solicitud sol = BiFactory.Sol;
                    SolicitudPreventivo sol_p = SolicitudPreventivo.FindOne(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
                   
                    cboSitios.Enabled = false;

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
            txtContactoCliente.Text = sol.Contacto;
            txtMailContacto.Text = sol.ContactoMail;
            txtNroOrdenCliente.Text = sol.NroOrdenCte;
            txtPresupuesto.Text = sol_p.Presupuesto;
            Adjuntos1.ListaAdjuntos(sol.Id_Solicitud.ToString());
        }

    }

    protected void btnAgregarTarea_Click(object sender, EventArgs e)
    {
        if (validar())
        {
            int id_Sol = BiFactory.Sol.Id_Solicitud;
            int id_Tarea;

            for (int i = 0; i < lstTareasaRealizar.Items.Count; i++)
            {
                if (lstTareasaRealizar.Items[i].Selected)
                {
                    id_Tarea = int.Parse(lstTareasaRealizar.Items[i].Value);

                     if (!SolicitudTareas.ExisteTareaEnSolicitud( id_Sol , id_Tarea ))
                    
                    {

                        SolicitudTareas t = new SolicitudTareas();
                        t.IdTarea = id_Tarea;
                        t.FechaInicio = jDatePick1.Fecha;
                        t.FechaFin = jDatePick2.Fecha;
                        t.IdSolicitud = id_Sol;
                        t.Save();
                        cboSitios.Enabled = false;

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
   
        foreach (Antares.model.Tareas t in Antares.model.Tareas.FindAll())
        {
            lstTareasaRealizar.Items.Add(new ListItem(t.Tarea, t.Id.ToString()));
        }


        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.FindAll())
        {
            lstVehiculos.Items.Add(new ListItem(vehi.Marca + " " + vehi.Modelo + " " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }
        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre + "(" + emp.Localidad + ")", emp.IdEmpresa.ToString()));
        }

        cmbResponsable.Items.Clear();
        cmbResponsable.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal responsable in Antares.model.Personal.FindAll())
        {
            cmbResponsable.Items.Add(new ListItem(responsable.Apellido + "," + responsable.Nombres, responsable.IdEmpleados.ToString()));
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

    }
    protected void btnAsignaEmpleadoSolicitud_Click(object sender, EventArgs e)
    {
        int id_sol = BiFactory.Sol.Id_Solicitud;
        int id_empleado;

        if (cmbResponsable.SelectedIndex > 0)
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

        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);

        if (ValidaSolicitud())
        {


            TransactionScope TX = new TransactionScope();
            try
            {
                
                            

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
                Sol_P.IdSitio = cboSitios.SelectedIndex;
                Sol_P.FechaFin = jDatePick2.Text;
                Sol_P.FechaInicio = jDatePick1.Text;
                Sol_P.Presupuesto = txtPresupuesto.Text;
                sol.Save();
                Sol_P.Save();
                TX.VoteCommit();

            }
            catch (Exception oEx)
            {

                TX.VoteRollBack();
                throw;
            }
            finally
            {
                TX.Dispose();
            }


        }
        Response.Redirect("./Solicitudes.aspx");
    

    }

    private void SaveSolicitud()
    {
        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
        sol.IdCliente = int.Parse(cmbClientes.SelectedValue);
        sol.Contacto = txtContactoCliente.Text;
        sol.NroOrdenCte = txtNroOrdenCliente.Text;
        sol.Status = eEstados.Pendiente.ToString();
        //sol.IdResponsable = cboResponsable.ValueInt;
        sol.Update();
        Response.Redirect("./Solicitudes.aspx");
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
        int IdDelResponsable = Int32.Parse(cmbResponsable.SelectedValue);
        lstEmpleadosSolicitud.Items.Clear();
        CargaListaEmpleados(IdDelResponsable);

        lstEmpleadosSolicitud.Enabled = true;
    }
    protected void CargaListaEmpleados(int IdDelResponsable)
    {

        //foreach (Antares.model.Personal persona in Antares.model.Personal.FindAll(Expression.Sql(" Id_Empleados <> " + IdDelResponsable.ToString())))
        foreach (Antares.model.Personal persona in Antares.model.Personal.FindAll())
        {
            if (persona.IdEmpleados != IdDelResponsable)
            {
                lstEmpleadosSolicitud.Items.Add(new ListItem(persona.Apellido + "," + persona.Nombres, persona.IdEmpleados.ToString()));
            }

        }


    }
    protected void gvSolicitudPersonas_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int valorResponsable = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Responsable"));
            if (valorResponsable == 1)
            {
                //Image imgResponsable = (Image)e.Row.FindControl("imgResponsable");
                //imgResponsable.Visible = true;
                e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[2].Text = "R";

            }
            else
            {
                e.Row.Cells[2].Text = string.Empty;

            }

        }

    }
    protected Boolean ValidaSolicitud()
    {
        bool Valida = true;
        string Msg = "";
        string idSol = BiFactory.Sol.Id_Solicitud.ToString();


        if (!Solicitud.TieneResponsable(idSol))
        {
            Valida = false;
            Msg = System.Environment.NewLine + "Para Confirmar una Solicitud debe Asignar un Responsable";
        }


        if (!Solicitud.TieneVehiculosAsignados(idSol))
        {
            Valida = false;
            Msg = System.Environment.NewLine + "Para Confirmar una Solicitud debe asignar al menos un Vehiculo";
        }

        HtmlGenericControl lb = (HtmlGenericControl)Master.FindControl("divMensajes");
        if (lb != null)
        {
            lb.InnerText = Msg;
        }

        return Valida;

    }
    
}
