using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Antares.model;
using WebAntares;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;



public partial class Administracion_Facturas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
        }
    }
    public void CargarCombos()
    {


        foreach (Antares.model.Proveedores prov in Antares.model.Proveedores.FindAll())
        {
            cmbProveedor.Items.Add(new ListItem( prov.Descripcion,prov.Id.ToString() ));
        }
    }


    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Facturas f = new Facturas();
        f.Descripcion = txtDescripcion.Text;
        f.FechaRecepcionFactura = dtFechaRecepcion.Fecha;
        f.Fecha = dtFechaFactura.Fecha;
        //Decimal.Parse(
        f.IdProveedor = cmbProveedor.SelectedIndex;
        f.ImporteGravado = Decimal.Parse(txtImporteGravado.Text);
        f.ImporteNoGravado = Decimal.Parse(txtImporteNOGravado.Text);
        f.ImporteTotal = Decimal.Parse(txtImporteTotal.Text);
        f.MontoRetencion = Decimal.Parse("0,00"); //"falta!!!";
        if (txtPorcentajeIVa.Text == "")
        {
            f.PorcentajeIva = Decimal.Parse("0,00");
        }
        else
        { f.PorcentajeIva = Decimal.Parse(txtPorcentajeIVa.Text); }

        
        f.PlazoPago = txtPlazoPago.Text;
        f.IdFacturaEstados = FacturaEstados.FindFirst(Expression.Eq("Descripcion", "INGRESADO")).Id;
        f.NFactura = txtCabecera_NroFactura.Text +"-" + txtNroFactura.Text;
        f.SaveAndFlush();
   
    }
}
