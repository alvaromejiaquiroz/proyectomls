using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NHibernate;
using NHibernate.Expression;
using Antares.model;
using WebAntares;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblFechaCreacionSolicitud.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblUserLogueado.Text = WebAntares.BiFactory.User.Nombre;
            
            CargarCombos();

            if ((Request.QueryString["Ac"] != null) && (Request.QueryString["Ac"].ToString() == "e"))
            {
                cmbTipoSolicitud.Enabled = false;
                txtTitulo.Enabled = false;
                Session["Accion"] = "e"; 
            }
            if (Request.QueryString["id"] != null)
            {
                FillSolicitud(int.Parse(Request.QueryString["id"].ToString()));
            }
            else
            {
                BiFactory.Sol = null;
            }
        }
    }
    private void CargarCombos()
    {

        cmbTipoSolicitud.Items.Clear();
        cmbTipoSolicitud.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (TipoSolicitud ts in Antares.model.TipoSolicitud.FindAll(Expression.Sql("Descripcion not in ('Francos Compensatorios')")))
        {
            ListItem l = new ListItem(ts.Descripcion, ts.IdTiposolicitud.ToString());
            cmbTipoSolicitud.Items.Add(l);
        }

    }

    private void FillSolicitud(int p)
    {
        Solicitud sol = Solicitud.GetById(p);
        BiFactory.Sol = sol;
        
        cmbTipoSolicitud.SelectedValue= sol.IdTipoSolicitud.ToString();
        if (cmbTipoSolicitud.SelectedValue != sol.IdTipoSolicitud.ToString())
        {
            //si el tipo es distinto es porque no tiene perfil para modificar
            Response.Redirect("./Solicitudes.aspx");
        }
        txtTitulo.Text = sol.Descripcion;
        lblUserLogueado.Text = BiFactory.User.LoginName.ToString();
    }
        
    private void GrabarSolicitud()
    {
        Solicitud sol;
        if (BiFactory.Sol == null)
        {
            sol = new Solicitud();
        }
        DateTime fechanula = DateTime.Parse("01/01/1900");
        sol = BiFactory.Sol;
        sol.Descripcion = txtTitulo.Text;
        sol.IdTipoSolicitud = int.Parse(cmbTipoSolicitud.SelectedValue);
        sol.IdUsuarioCreador = BiFactory.User.IdUsuario;
        //Fechas
        sol.FechaCreacion = System.DateTime.Now;
        sol.FechaAprobacionCalidad = fechanula;
        sol.FechaAprobacionTecnica = fechanula;
        sol.FechaReprogramacion = fechanula;
        sol.FechaSuspencion = fechanula;
        sol.ProximaFechaFin = fechanula;
        sol.ProximaFechaInicio = fechanula;
        
        sol.Save();
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            GrabarSolicitud();
            switch (cmbTipoSolicitud.SelectedValue)
            {
                case "1":
                    Response.Redirect("./MantPreventivo.aspx");
                    break;
                case "2":
                    Response.Redirect("./MantCorrectivo.aspx");
                    break;
                case "3":
                    Response.Redirect("./TareasGenerales.aspx");
                    break;
                case "4":
                    Response.Redirect("./FrancosCompensatorios.aspx");
                    break;
                case "5":
                    Response.Redirect("./Licencias.aspx");
                    break;
                case "6":
                    Response.Redirect("./Obras.aspx");
                    break;
                case "7":
                    Response.Redirect("./Capacitacion.aspx");
                    break;
            }
        }
    }

    protected void cvTipoSolicitud_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !cmbTipoSolicitud.SelectedValue.Equals("-1");
    }
}
