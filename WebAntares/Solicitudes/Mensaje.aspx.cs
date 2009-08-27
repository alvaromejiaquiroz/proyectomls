using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Antares.model;
using NHibernate.Expression;

public partial class Solicitudes_Mensaje : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Request.QueryString["Id"] != null) && (Request.QueryString["St"] != null))
        {
            int IdSolicitud = int.Parse(Request.QueryString["Id"].ToString());
            bool mensaje = bool.Parse(Request.QueryString["St"].ToString());

            Solicitud Sol = Solicitud.GetById(IdSolicitud);

            if (mensaje == true)
            {
                lblMensaje.Text = "La Solicitud " + IdSolicitud + " " + Sol.Descripcion + " se Genero Correctamente";

            }
            else
            {
                lblMensaje.Text = "La Solicitud " + IdSolicitud + " " + Sol.Descripcion + " No se pudo Generar , Contacte al Administrador";
            }

        
        }


    }
}
