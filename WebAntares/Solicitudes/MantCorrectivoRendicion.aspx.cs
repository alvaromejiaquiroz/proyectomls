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
/// Mantenimiento CORRECTIVO
/// </summary>
public partial class Solicitudes_MantPreventivo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            CargarCombos();
            FillServicios();
            FillSolicitudEmpleados();
            FillSolicitudVehiculos();
            LoadDataComplementaria();
            FillCorrectiva();
        }
    }

    private void FillCorrectiva()
    {
        Solicitud_Relacion r = Solicitud_Relacion.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));



        Solicitud sol = Solicitud.GetById(r.IdSolicitud_Relacionada);
        try
        {

            SolicitudCorrectivo Sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
            cmbPlazoAtencion.SelectedValue = Sol_Cor.IdPlazoAtencion.ToString();
            txtReportoFalla.Text = Sol_Cor.PersonaReportoFalla;
            txtFalla.Text = Sol_Cor.FallaReportada;
            FechayHora1.Text = Sol_Cor.FechanotificacionCliente.ToString();
            txtCausa.Text = Sol_Cor.CausaPosible;
            cmbPlazoAtencion.SelectedValue = Sol_Cor.IdPlazoAtencion.ToString();
            txtPresupuesto.Text = Sol_Cor.Presupuesto;
        }
        catch (Exception e)
        {

        }
    }

    private void LoadDataComplementaria()
    {
        Solicitud sol = BiFactory.Sol;
        try
        {
            
            txtContactoCliente.Text = sol.Contacto;
            txtNroOrdenCliente.Text = sol.NroOrdenCte;
            cmbClientes.SelectedValue = sol.IdCliente.ToString();
            txtMailContacto.Text = sol.ContactoMail;
            txtTelefonoContacto.Text = sol.ContactoTel;
            

        }
        catch (Exception e)
        {

        }
    }

    private void FillServicios()
    {

        gvServicios.DataSource = SolicitudServiciosAfectados.GetServiciosAfectados(BiFactory.Sol.Id_Solicitud);
        gvServicios.DataKeyNames = new string[] { "Id" };
        gvServicios.DataBind();
        CargaComboServicios();
    }

    public bool validar()
    {


        return true;
    }



    public void AgregarRegistroaGrilla()
    {
    }
    public void CargaComboServicios()
    {

        cmbServiciosAfectados.Items.Clear();
        cmbServiciosAfectados.Items.Add(new ListItem("Seleccione...", "-1"));
        string sql = " Id not in (select IdServicioAfectado from dbo.Solicitud_Servicios_Afectados where idsolicitud = " + BiFactory.Sol.Id_Solicitud.ToString() + ")";
        foreach (Antares.model.Servicios serv in Antares.model.Servicios.FindAll(Expression.Sql(sql)))
        {
            cmbServiciosAfectados.Items.Add(new ListItem(serv.Descripcion, serv.Id.ToString()));
        }
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
    public void CargarCombos()
    {
        // btnAceptarSolicitud.Enabled = false;
        CargaComboServicios();
        CargaComboPersonal();
        CargaComboVehiculos();

        foreach (Antares.model.PlazoRealizacion plazo in Antares.model.PlazoRealizacion.FindAll())
        {
            cmbPlazoAtencion.Items.Add(new ListItem(plazo.Descripcion, plazo.Id.ToString()));
        }
      


        foreach (Antares.model.Empresas emp in Antares.model.Empresas.FindAll())
        {
            cmbClientes.Items.Add(new ListItem(emp.Nombre + "(" + emp.Localidad + ")", emp.IdEmpresa.ToString()));
        }

   
    }

        
    private void FillSolicitudEmpleados()
    {
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
    private void FillSolicitudServicios()
    {
        gvServicios.DataSource = SolicitudServiciosAfectados.GetServiciosAfectados(BiFactory.Sol.Id_Solicitud);
        gvServicios.DataKeyNames = new string[] { "Id" };
        gvServicios.DataBind();
    }


    protected void gvSolicitudVehiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosVehiculos v = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudVehiculos.DataKeys[e.RowIndex].Value.ToString())));
        v.Delete();
        FillSolicitudVehiculos();
    }
    protected void gvServicios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudServiciosAfectados s = SolicitudServiciosAfectados.FindFirst(Expression.Eq("Id", int.Parse(gvServicios.DataKeys[e.RowIndex].Value.ToString())));
        s.Delete();
        FillServicios();
    }
    protected void gvPersonas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRecursosEmpleados p = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", int.Parse(gvSolicitudPersonas.DataKeys[e.RowIndex].Value.ToString())));
        p.Delete();
        FillSolicitudEmpleados();

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
        //for (int i = 0; i < lstServiciosAfectados.Items.Count; i++)
        //{
        //    if (lstServiciosAfectados.Items[i].Selected)
        //    {
        //        hay = true;
        //        i = lstServiciosAfectados.Items.Count;
        //    }

        //}


        return hay;

    }
    protected Boolean ValidaSolicitud()
    {
        bool Valida = true;
        string Msg = "";
        string idSol = BiFactory.Sol.Id_Solicitud.ToString();


        if (!Solicitud.TieneServiciosAfectados(idSol) && (!HayServiciosAfectadosSeleccionados()))
        {
            Valida = false;
            Msg = System.Environment.NewLine + "Para Confirmar una Solicitud debe asignar al menos un Servicio Afectado";
        }


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

    protected void btnAsignaVehiculoSolicitud_Click(object sender, EventArgs e)
    {
        int idVehiculo = int.Parse(cmbVehiculo.SelectedValue);
        if (idVehiculo > 0)
        {
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
            CargaComboVehiculos();
            FillSolicitudVehiculos();
        }
    }
    protected void btnAsignaEmpleadoSolicitud_Click(object sender, EventArgs e)
    {
        int idEmpleado = int.Parse(cmbEmpleado.SelectedValue);
        if (idEmpleado > 0)
        {
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
    }   

    protected void btnAceptarSolicitud_Click(object sender, EventArgs e)
    {
        Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);

        if (ValidaSolicitud())
        {


            

                Solicitud_Relacion Rela = Solicitud_Relacion.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
                Solicitud Sol_Original = Solicitud.GetById(Rela.IdSolicitud_Relacionada);
                Sol_Original.Status = eEstados.Realizado.ToString();
                Sol_Original.Update();

                Solicitud Reporte = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);

                SolicitudCorrectivo Corr = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));



                TransactionScope TX = new TransactionScope();
                try
                {


                    Reporte.Status = eEstados.Realizado.ToString();

                    Reporte.Save();


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


            
            Response.Redirect("./Solicitudes.aspx");

        }        
    
    }
    protected void btnAgregarServicio_Click(object sender, EventArgs e)
    {

        int idServicio = int.Parse(cmbServiciosAfectados.SelectedValue);
        if (idServicio > 0 )
        {
            Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            if (!SolicitudServiciosAfectados.Exists(" idSolicitud = ? and IdServicioAfectado = ?", sol.Id_Solicitud, idServicio)) 
            {

                SolicitudServiciosAfectados t = new SolicitudServiciosAfectados();
                t.IdSolicitud = sol.Id_Solicitud;
                t.IdServicioAfectado = int.Parse(cmbServiciosAfectados.SelectedValue);
                t.Save();

            }
            FillServicios();
        }
    }
}

