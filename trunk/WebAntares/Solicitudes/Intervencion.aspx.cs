using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Antares.model;
using Gos.Usuarios;
using WebAntares;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblFechaCreacionSolicitud.Text = DateTime.Now.ToString("dd/MM/yyyy");
        lblUserLogueado.Text = WebAntares.BiFactory.User.Nombre;
        
        if (!Page.IsPostBack)
        {
            cboTipoSolicitud1.rendercbo();

            if ((Request.QueryString["Ac"] != null) && (Request.QueryString["Ac"].ToString() == "e"))
            {
                cboTipoSolicitud1.Enabled = false;
                txtTitulo.Enabled = false;
                Session["Accion"] = "e"; 


            }
            if (Request.QueryString["id"] != null)
            {
                fillSolicitud(int.Parse(Request.QueryString["id"].ToString()));
            }
            else
            {
                //limpiamos la solicitud actual
                BiFactory.Sol = null;
            }
        }


    }

    private void fillSolicitud(int p)
    {
        Solicitud sol = Solicitud.GetById(p);
        BiFactory.Sol = sol;
        
        cboTipoSolicitud1.value = sol.IdTipoSolicitud.ToString();
        if (cboTipoSolicitud1.value != sol.IdTipoSolicitud.ToString())
        {
            //si el tipo es distinto es porque no tiene perfil para modificar
            Response.Write("no tiene permisos para modificar");
            Response.End();
        }
        txtTitulo.Text = sol.Descripcion;
        lblUserLogueado.Text = sol.IdUsuarioCreador.ToString();
    }
    
    private bool Valid()
    {
        string sFalta = "";
       
        if (cboTipoSolicitud1.value == "-1")
        {
            sFalta += "<br>Falta Ingresar el tipo de solicitud";
        }
        if (txtTitulo.Text == "")
        {
            sFalta += "<br>Falta Ingresar el Titulo";
        }
        lblMensaje.Text = sFalta;
        return (sFalta.Length == 0);

    }

    private void grabarSolicitud()
    {
        
        Solicitud sol = BiFactory.Sol;
        sol.Descripcion = txtTitulo.Text;
        sol.FechaCreacion = System.DateTime.Now;
        sol.IdTipoSolicitud = int.Parse(cboTipoSolicitud1.value);
        sol.IdUsuarioCreador = BiFactory.User.IdUsuario;
        sol.Save();


    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (Valid())
        {
            grabarSolicitud();
            switch (cboTipoSolicitud1.value)
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
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
    }
    private void CargarCombos()
    {
        //TiposolicitudXPerfil Perfil = TiposolicitudXPerfil.FindAll("
        //BiFactory.User.IdPerfil
    }

}
