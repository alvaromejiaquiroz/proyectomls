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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillGrid(0);

        }
    }

    protected void FillGrid(int pageIndex)
    {
        MailListas[] reader = MailListas.FindAll();
        GridView1.DataSource = reader;
        GridView1.PageIndex = pageIndex;
        GridView1.DataBind();
       
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
        {
            Int32 id = Int32.Parse(e.CommandArgument.ToString());
            MailListas ml = MailListas.FindOne(Expression.Eq("id", id));
            if (ml != null)
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        //txtDescripcion.Text = ml.Descripcion;
                        //txtNombreLista.Text = ml.Nombre;
                        Response.Redirect("~/usuarios/PersonalListaCorreo.aspx?id=" + ml.Id.ToString());
                        break;
                    case "Eliminar":
                        ml.Delete();
                        FillGrid(0);
                        break;
                }
            }
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            MailListas ml = new MailListas();
            ml.Nombre = txtNombreLista.Text;
            ml.Descripcion = txtDescripcion.Text;
            ml.Save();
            FillGrid(0);
        }
    }
}
