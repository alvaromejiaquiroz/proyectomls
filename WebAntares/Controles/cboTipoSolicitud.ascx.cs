using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antares.model;
using Castle.Core;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using Castle.DynamicProxy;
using NHibernate;
using NHibernate.Expression;
using WebAntares;

public delegate void cboSelected(string aselec);

public partial class Controles_cboEmpresas : System.Web.UI.UserControl
{
    public event cboSelected seleciona;   

    public string value
    {
        get { return ddlTipo.SelectedValue; }
        set{
            if (ddlTipo.Items.FindByValue(value) != null)
            {
                ddlTipo.SelectedValue = value;            
            }
        }
    }

    public void Clear()
    {
        ddlTipo.SelectedIndex = 0;
    }
    public bool Enabled
    {
        get { return ddlTipo.Enabled; }
        set { ddlTipo.Enabled = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    public void rendercbo()
    {
        
        ddlTipo.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (TiposolicitudXPerfil u in TiposolicitudXPerfil.FindAll(Expression.Eq("IdPerfil", BiFactory.User.IdPerfil)))
        {
            TipoSolicitud t = TipoSolicitud.Find(u.IdTiposolicitud);
            
            ddlTipo.Items.Add(new ListItem(t.Descripcion, t.IdTiposolicitud.ToString()));
        }
    }
    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (seleciona != null)
        { // El evento se lanza como cualquier delegado
                   seleciona.Invoke("select");
        }
    }
  
}
