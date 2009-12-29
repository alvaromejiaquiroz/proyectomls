using System;
using System.Data.Common;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using Castle.Core;
using Antares.model;

public partial class Usuarios_ListasCorreo : System.Web.UI.Page
{
    static int id;
    static MailListas ml;
    static Personal persona;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"].ToString());
                ml = MailListas.FindOne(Expression.Eq("id",id));
                lblLista.Text = ml.Nombre;
                FillGrid(0,id);
            }

        }
    }

    protected void CargarCombos()
    {

        cmbPersonal.Items.Clear();
        cmbPersonal.Items.Add(new ListItem("Seleccione...", "-1"));

        foreach (Antares.model.Personal p in Antares.model.Personal.GetPersonalActivo())
        {
            cmbPersonal.Items.Add(new ListItem(p.Apellido + "," + p.Nombres, p.IdEmpleados.ToString()));
        }
    }
  
    protected void FillGrid(int pageIndex,int id)
    {
        DbDataReader reader = MailListasPersonal.PersonalEnLista_Mail(id);
        DataTable table = new DataTable();
        table.Load(reader);
        GridView1.DataSource = table;
        GridView1.PageIndex = pageIndex;
        GridView1.DataBind();
       
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
        {
            Int32 id = Int32.Parse(e.CommandArgument.ToString());
            MailListasPersonal l = MailListasPersonal.FindOne(Expression.Eq("id", id));
            if (ml != null)
            {
                switch (e.CommandName)
                {
                    case "Eliminar":
                        l.Delete();
                        FillGrid(0,ml.Id);
                        break;
                }
            }
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {

        if (IsValid)
        {

            MailListasPersonal mlp = MailListasPersonal.FindOne(Expression.Eq("IdMailLista", ml.Id), Expression.Eq("IdEmpleados", persona.IdEmpleados));
            if (mlp == null)
            {
                mlp = new MailListasPersonal();
                mlp.IdEmpleados = persona.IdEmpleados;
                mlp.IdMailLista = ml.Id;
                mlp.FechaActualizacion = DateTime.Now;
                mlp.Save();
            }
            FillGrid(0, ml.Id);
        }
    }
    protected void cvPersona_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;
        persona = Personal.GetById(cmbPersonal.SelectedValue);
        if (persona.email != null)
        {

            args.IsValid = true;

        }
        else
        {
            args.IsValid = false;
            cvPersona.ErrorMessage = persona.Apellido + "," + persona.Nombres + " no tiene un email valido ";
        }
        }
    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/usuarios/ListasCorreo.aspx");
    }
}
