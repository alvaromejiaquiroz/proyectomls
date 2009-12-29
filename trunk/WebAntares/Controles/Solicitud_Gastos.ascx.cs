using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antares.model;
using NHibernate.Expression;
using WebAntares;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Collections.Generic;
using System.Data;

public partial class Controles_Gastos : System.Web.UI.UserControl
{

    private string _modo;
        
    private Solicitud _solicitud;
    
    public Solicitud Sol 
    {
        get { return _solicitud; }
        set { _solicitud = value; }
    }

    public string Modo
    {
        get { return _modo; }
        set { _modo = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargaCombos();
            FillGrid(0);
            
            if (Sol.Reporte == "SI")
            {
                FillGridSolicitudInicial(0,Sol.IdSolicitudInicial);

                if (gvGastosSolicitudInicial.Rows.Count > 0)
                {
                    pnlGastosSolInicial.Visible = true;
                }
            }
            if (Modo == "E")
            {
                Deshabilita_Gastos();
            }
        }
    }

    private void CargaCombos()
    {
        cmbTipo.Items.Clear();
        cmbTipo.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.TipoGasto tg in Antares.model.TipoGasto.FindAll())
        {
            cmbTipo.Items.Add(new ListItem(tg.Descripcion , tg.Id.ToString()));
        }
        
    }

    public void MuestraGastos()
    {
        FillGrid(0);
    }
  
    private void FillGrid(int pageIndex)
    {
    
//        gvGastos.DataSource = SolicitudGastos.FindAll(Expression.Eq("IdSolicitud", Sol.Id_Solicitud)); ;
        
        DataTable table = new DataTable();
        DbDataReader reader = SolicitudGastos.GetGastosSolicitud(Sol.Id_Solicitud);
        table.Load(reader);
        
        gvGastos.DataSource = table;
        gvGastos.PageIndex = pageIndex;
        gvGastos.DataKeyNames = new string[] { "Id" };
        gvGastos.DataBind();
        lblTotal.Text = "$" + Solicitud.Valida_Gastos_Ingresados_Solicitud(Sol.Id_Solicitud).ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Sol != null)
            {
                try
                {
                    SolicitudGastos gasto = new SolicitudGastos();
                    gasto.IdSolicitud = Sol.Id_Solicitud;
                    if (txtDetalle.Text.Length == 0)
                    {
                        gasto.Detalle = string.Empty;
                    }
                    else
                    {
                        gasto.Detalle = txtDetalle.Text;
                    }
                    gasto.IdTipoGasto = int.Parse(cmbTipo.SelectedValue);
                    gasto.Fecha = DateTime.Now;
                    gasto.Total = decimal.Parse(txtTotal.Text);
                    gasto.Save();
                    LimpiaForm();
                    FillGrid(0);
                }
                catch (Exception info)
                {
                    throw info;
                }
            }
        }

    }

    protected void LimpiaForm()
    {
        txtDetalle.Text = string.Empty;
        txtTotal.Text = string.Empty;

    }
   
    protected void gvGastos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int32 Id = int.Parse(gvGastos.DataKeys[e.RowIndex].Value.ToString());
        SolicitudGastos t;
        t = SolicitudGastos.FindFirst(Expression.Eq("Id", Id));
        t.Delete();
        FillGrid(0);
            
     

    }

    protected void gvGastos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(e.NewPageIndex);

    }
   
    public void Deshabilita_Gastos()
    {
        SolicitudGastos g = SolicitudGastos.FindFirst(Expression.Eq("IdSolicitud", Sol.Id_Solicitud));
        if (g != null)
        {
            txtDetalle.Enabled = false;
            txtTotal.Enabled = false;
            cmbTipo.Enabled = false;
            Button1.Enabled = false;
            pnlGastos.Visible = false;
            this.Modo = "E";
        }

    }

    protected void gvGastos_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (Modo == "E")
        {
            int i = e.Row.Cells.Count;
            e.Row.Cells[i - 1].Visible = false;
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[i - 1].Visible = true;
        //}
        //else if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[i - 1].Visible = false;
        //}

        

        
    }

    protected void FillGridSolicitudInicial(int pageIndex, int IdSol)
    {
        DataTable table = new DataTable();
        DbDataReader reader = SolicitudGastos.GetGastosSolicitud(IdSol);
        table.Load(reader);

        gvGastosSolicitudInicial.DataSource = table;
        gvGastosSolicitudInicial.PageIndex = pageIndex;
        gvGastosSolicitudInicial.DataKeyNames = new string[] { "Id" };
        gvGastosSolicitudInicial.DataBind();
        lblGastosSolicitudInicial.Text = "$" + Solicitud.Valida_Gastos_Ingresados_Solicitud(IdSol).ToString();
    }


    protected void gvGastosSolicitudInicial_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGridSolicitudInicial(e.NewPageIndex,Sol.IdSolicitudInicial);

    }
}
