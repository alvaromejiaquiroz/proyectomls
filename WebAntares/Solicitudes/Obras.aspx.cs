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

public partial class Solicitudes_Obras : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            FillSolicitudEmpleados();
            FillSolicitudVehiculos();


        }

    }
    public bool validar()
    {


        return true;
    }
    public void AgregarRegistroaGrilla()
    {
    }
    public void CargarCombos()
    {
        // btnAceptarSolicitud.Enabled = false;

        lstEmpleadosSolicitud.Enabled = false;

        /*  
          foreach (Antares.model.Personal persona in Antares.model.Personal.FindAll())
          {
              lstEmpleadosSolicitud.Items.Add(new ListItem(persona.Apellido + "," + persona.Nombres, persona.IdEmpleados.ToString()));
          }
          */

        foreach (Antares.model.Vehiculos vehi in Antares.model.Vehiculos.FindAll())
        {
            lstVehiculos.Items.Add(new ListItem(vehi.Marca + " " + vehi.Modelo + " " + vehi.Patente, vehi.IdVehiculos.ToString()));
        }

        /////

        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbCliente.Items.Add(new ListItem(emp.Nombre + "(" + emp.Localidad + ")", emp.IdEmpresa.ToString()));
        }


        foreach (Antares.model.Personal responsable in Antares.model.Personal.FindAll())
        {
            cmbResponsable.Items.Add(new ListItem(responsable.Apellido + "," + responsable.Nombres, responsable.IdEmpleados.ToString()));
        }




    }
    protected void btnAsignarEmpleadoSolicitud_Click(object sender, EventArgs e)
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
        //Responable

        //cboResponsable.BindCBO();
        //cboResponsable.Value = BiFactory.Sol.IdResponsable.ToString();

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
        if (ValidaSolicitud() && !(Solicitud.ExisteObra(sol.Id_Solicitud.ToString())) )
        {            
            TransactionScope TX = new TransactionScope();
            try
            {
                

                sol.IdCliente = int.Parse(cmbCliente.SelectedValue);
                sol.Contacto = txtContacto.Text;
                sol.NroOrdenCte = txtnroOC.Text;
                sol.Status = eEstados.Pendiente.ToString();
                sol.ContactoMail = txtMail.Text;
                sol.ContactoTel = txtTelefonoContacto.Text;
                //sol.IdResponsable = cboResponsable.ValueInt;

                SolicitudObra Sol_Ob = new SolicitudObra();
                Sol_Ob.IdSolicitud = sol.Id_Solicitud;
                Sol_Ob.DescripcionTareas  = txtDescripcionTareas.Text;
                Sol_Ob.FechaInicio = dtpInicio.Text;
                Sol_Ob.FechaFin = dtpFin.Text;
                Sol_Ob.RequisitosAprovacion = txtRequisitoAprova.Text;
                Sol_Ob.RequisitosIngreso = txtRequisitoIngreso.Text;
                Sol_Ob.Presupuesto = txtPresupuesto.Text;
                sol.Save();
                Sol_Ob.Save();
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
    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos v = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        v.Delete();
        FillSolicitudVehiculos();
    }
    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.RowIndex].Value.ToString())));
        p.Delete();
        FillSolicitudEmpleados();

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
    protected Boolean HayServiciosAfectadosSeleccionados()
    {
        bool hay = false;
       


        return hay;

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
