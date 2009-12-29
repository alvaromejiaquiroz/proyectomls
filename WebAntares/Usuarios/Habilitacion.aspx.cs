using System;
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
using Antares.model;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using Castle.Core;


public partial class Usuarios_Habilitacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
           
        }
    }

    protected void CargarCombos()
    {
        cmbPerfiles.Items.Clear();
        cmbPerfiles.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Perfiles perfil in Antares.model.Perfiles.FindAll()) 
        {
            cmbPerfiles.Items.Add(new ListItem(perfil.Detalle ,perfil.IdPerfil.ToString()));
        }

    }

    protected void FillGridAcciones(int idPerfil)
    {
        gvAcciones.DataSource = Acciones.AccionesHabilitadas_X_Perfil(idPerfil);
        gvAcciones.DataKeyNames = new string[] { "IdAccion" };
        gvAcciones.DataBind();
    }
    
    protected void cmbPerfiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idperfil = int.Parse(cmbPerfiles.SelectedValue);
        FillGridAcciones(idperfil);
        pnlAcciones.Visible = true;
    }
    
    protected void gvAcciones_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string habilitado = DataBinder.Eval(e.Row.DataItem, "Habilitado").ToString();
            CheckBox cb = (CheckBox)e.Row.FindControl("chkHab");
            
            if (habilitado=="1")
            {
                cb.Checked = true;
            }

            
            
        }
    }


    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            CheckBox chk;
            int idAccion;
            int idPerfil = int.Parse(cmbPerfiles.SelectedValue);
            Perfiles p = Perfiles.FindOne(Expression.Eq("IdPerfil",idPerfil));
            

            foreach (GridViewRow row in gvAcciones.Rows)
            {
                idAccion = int.Parse(gvAcciones.DataKeys[row.RowIndex].Value.ToString());
                Acciones acc = Acciones.FindOne(Expression.Eq("IdAccion", idAccion));
                chk = (CheckBox)row.FindControl("chkHab");
                AccionPerfil ap = AccionPerfil.FindOne(Expression.Eq("IdPerfil", idPerfil), Expression.Eq("IdAccion", idAccion));

                if (chk.Checked)
                {
                    if (ap == null)
                    {
                        ap = new AccionPerfil();
                        ap.IdAccion = idAccion;
                        ap.IdPerfil = idPerfil;
                        ap.FechaActualizacion = DateTime.Now;
                        ap.Save();
                        Logger.Log(TipoEvento.AsignaPermisos, "Perfil " + p.Detalle.ToString() + " Accion " + acc.Objeto  + ":"+ acc.Valor);
                    }
                }
                else
                {
                    if (ap != null)
                    {
                        ap.Delete();
                        Logger.Log(TipoEvento.QuitaPermisos, "Perfil " + idPerfil.ToString() + " IdAccion " + idAccion.ToString());
                    }

                }
            }
            FillGridAcciones(idPerfil);
        }

    }
}

