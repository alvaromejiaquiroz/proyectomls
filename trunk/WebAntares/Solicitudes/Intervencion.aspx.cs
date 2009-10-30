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

            ucTipoSolicitud.rendercbo();

            if ((Request.QueryString["Ac"] != null) && (Request.QueryString["Ac"].ToString() == "e"))
            {
                ucTipoSolicitud.Enabled = false;
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

    private void FillSolicitud(int p)
    {
        Solicitud sol = Solicitud.GetById(p);
        BiFactory.Sol = sol;
        
        ucTipoSolicitud.value = sol.IdTipoSolicitud.ToString();
        if (ucTipoSolicitud.value != sol.IdTipoSolicitud.ToString())
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

        sol = BiFactory.Sol;
        sol.Descripcion = txtTitulo.Text;
        sol.FechaCreacion = System.DateTime.Now;
        sol.IdTipoSolicitud = int.Parse(ucTipoSolicitud.value);
        sol.IdUsuarioCreador = BiFactory.User.IdUsuario;
        sol.Save();
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            GrabarSolicitud();
            switch (ucTipoSolicitud.value)
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
        args.IsValid = !ucTipoSolicitud.value.Equals("-1");
    }
}
