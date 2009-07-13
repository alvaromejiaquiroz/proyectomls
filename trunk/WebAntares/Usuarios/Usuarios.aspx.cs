using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Antares.model;

public partial class Admin_Usuarios : System.Web.UI.Page
{

    static Gos.Usuarios.Usuarios oUsuario = new Gos.Usuarios.Usuarios();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { FillGrid(); }
    }
    protected void cmdAccion_Click(object sender, EventArgs e)
    {
        Save();
        FillGrid();
        Clear();
    }
    private void FillGrid()
    {
        GridView1.DataSource = Gos.Usuarios.Usuarios.FindAll();
        GridView1.DataKeyNames = new string[] { "IdUsuario" };
        GridView1.AutoGenerateColumns = false;
        GridView1.DataBind();
        cmdAccion.Text = "Nuevo";

    }
    void Save()
    {
        switch (cmdAccion.Text)
        {
            case "Nuevo":
                FormToObjet();
                oUsuario.IdUsuario = 0;
                oUsuario.SaveAndFlush();
                UpdateUsuarioEmpleado();
                break;
            case "Modificar":
                FormToObjet();
                oUsuario.SaveAndFlush();
                UpdateUsuarioEmpleado();
                break;
            case "Eliminar":
                oUsuario.Delete();
                UsuariosEmpleados.DeleteAll("Id_Usuarios = " + oUsuario.IdUsuario.ToString());
                break;
        }
    }

    private void UpdateUsuarioEmpleado()
    {
        UsuariosEmpleados ue = UsuariosEmpleados.FindFirst(Expression.Eq("IdUsuarios", oUsuario.IdUsuario));
        if (ue == null)
        {
            ue = new UsuariosEmpleados();
            ue.IdUsuarios = oUsuario.IdUsuario;
            
        }
        ue.IdEmpleados = cboEmpleados.ValueInt;
        ue.Fecha = System.DateTime.Now;
        ue.SaveAndFlush();
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        oUsuario = Gos.Usuarios.Usuarios.FindFirst(Expression.Eq("IdUsuario", int.Parse(GridView1.SelectedValue.ToString())));
        showData();
        cmdAccion.Text = "Modificar";
    }

    void showData()
    {
        ObjetToForm();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        oUsuario = Gos.Usuarios.Usuarios.FindFirst(Expression.Eq("IdUsuario", int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString())));
        showData();
        cmdAccion.Text = "Eliminar";
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Clear();
    }

    void FormToObjet()
    {
        oUsuario.Nombre = txtNombre.Text;
        oUsuario.IdPerfil = cboPerfil.ValueInt;
        oUsuario.LoginName = txtLogin.Text;
        oUsuario.Password = txtPass.Text;
    }
    void ObjetToForm()
    {
        txtIdUsuario.Text = oUsuario.IdUsuario.ToString();
        txtNombre.Text = oUsuario.Nombre;
        cboPerfil.Value = oUsuario.IdPerfil.ToString();
        txtLogin.Text = oUsuario.LoginName;
        txtPass.Text = oUsuario.Password;

        Antares.model.UsuariosEmpleados ue = UsuariosEmpleados.FindFirst(Expression.Eq("IdUsuarios", oUsuario.IdUsuario));
        if (ue != null)
        {
            cboEmpleados.Value = ue.IdEmpleados.ToString();
        }
        else
        {
            cboEmpleados.Value = "-1";
        }
    }
    void Clear()
    {
        txtIdUsuario.Text = "";
        txtNombre.Text = "";
        txtLogin.Text = "";
        txtPass.Text = "";
        cboPerfil.Value = "-1";
        cboEmpleados.Value = "-1";
    }



}
