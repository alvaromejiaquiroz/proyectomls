using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Expression;
using Antares.model;
using WebAntares;


public partial class Solicitudes_MantRendicion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            WebAntares.BiFactory.Sol = Antares.model.Solicitud.GetById(int.Parse(Request.QueryString["id"].ToString()));

        }
        fillForm();
    }

    private void fillForm()
    {
        FillTareas();
    }

    #region TAREAS
    private void FillTareas()
    {

        gvTareas.DataSource = SolicitudTareas.GetReader(BiFactory.Sol.Id_Solicitud);
        gvTareas.DataKeyNames = new string[] { "Id" };
        gvTareas.DataBind();
    }

    protected void gvTareas_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int idTarea = int.Parse(gvTareas.SelectedValue.ToString());
        //TareasRealizadas tr = TareasRealizadas.getById(idTarea);

        //txtIdTarea.Text = tr.Id.ToString();
        //txtTarea.Text = tr.TipoTarea;



        //oUsuario= Usuario.Buscar("IdUsuario=" + GridView1.SelectedValue.ToString());
        //showData();
        //cmdAccion.Text = "Modificar";
    }
    #endregion
}
