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
    private static string id;
    protected void Page_Load(object sender, EventArgs e)
    {
         

        if (!Page.IsPostBack)
        {
           
            if (Session["Factura"] != null )
            {
                id = Session["Factura"].ToString();
            }

            if (id == null)
            {
                CargarCombos();
            }
            else
            {
                Facturas f = Facturas.FindFirst(Expression.Eq("Id", Int32.Parse(id)));
                
                string temp_factura = f.NFactura;
                string[] c_factura = temp_factura.Split('-');
                txtCabecera_NroFactura.Text = c_factura[0];
                txtNroFactura.Text = c_factura[1];
                
                txtDescripcion.Text = f.Descripcion;
                txtImporteGravado.Text = f.ImporteGravado.ToString() ;
                txtImporteNOGravado.Text = f.ImporteNoGravado.ToString();
                txtImporteTotal.Text = f.ImporteTotal.ToString();
                txtPlazoPago.Text = f.PlazoPago;
                txtPorcentajeIVa.Text = f.PorcentajeIva.ToString();
                dtFechaFactura.Text = f.Fecha.ToString("dd/MM/yyyy");
                dtFechaRecepcion.Text = f.FechaRecepcionFactura.ToString("dd/MM/yyyy");

                Proveedores p = Proveedores.FindFirst(Expression.Eq("IdProveedor", f.IdProveedor));
                TipoProveedor tp = TipoProveedor.FindFirst(Expression.Eq("Id", p.IdCategoriaProveedor));
                TipoFactura tf = TipoFactura.FindFirst(Expression.Eq("Id",f.IdTipoFactura));

                CargarCombos();
                cmbTipoFactura.SelectedIndex = tf.Id;
                cmbTipoProveedor.SelectedIndex = tp.Id;

                cmbProveedor.Items.Clear();
                cmbProveedor.Items.Add(new ListItem("Seleccione...", "-1"));

                foreach (Antares.model.Proveedores prov in Antares.model.Proveedores.FindAllByProperty("IdCategoriaProveedor",tp.Id))
                {
                    cmbProveedor.Items.Add(new ListItem(prov.Denominacion, prov.IdProveedor.ToString()));
                }
                //cmbProveedor.Enabled = true;
                //cmbProveedor.SelectedIndex = f.IdProveedor;
                cmbProveedor.SelectedItem.Value = f.IdProveedor.ToString();
                cmbProveedor.SelectedItem.Text = p.Denominacion;
                                
            }

           
        }
    }
    public void CargarCombos()
    {
        dtFechaFactura.Text = DateTime.Today.ToString("dd/MM/yyyy");
        cmbTipoFactura.Items.Clear();
        cmbTipoFactura.Items.Add(new ListItem("Seleccione...", "-1"));

        foreach (Antares.model.TipoFactura tf in Antares.model.TipoFactura.FindAll())
        {
            cmbTipoFactura.Items.Add(new ListItem(tf.Descripcion, tf.Id.ToString()));
        }

        cmbTipoProveedor.Items.Clear();
        cmbTipoProveedor.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.TipoProveedor tp in Antares.model.TipoProveedor.FindAll())
        {
            cmbTipoProveedor.Items.Add(new ListItem(tp.Descripcion, tp.Id.ToString()));
        }

        cmbProveedor.Items.Clear();
        cmbProveedor.Items.Add(new ListItem("Seleccione...", "-1"));

        
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Facturas f;
        
        if (id == null)
        {
            f = new Facturas();
        }
        else
        {
            f = Facturas.FindFirst(Expression.Eq("Id", Int32.Parse(id)));
        }

        f.IdTipoFactura = int.Parse(cmbTipoFactura.SelectedValue);
        f.Descripcion = txtDescripcion.Text;
        f.FechaRecepcionFactura = dtFechaRecepcion.Fecha;
        f.Fecha = dtFechaFactura.Fecha;
        f.IdProveedor = int.Parse(cmbProveedor.SelectedValue);
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
        
        f.Save();
        
        LimpiaForm();
        
    }
    protected void cmbTipoProveedor_SelectedIndexChanged(object sender, EventArgs e)
    {

       int tipo_proveedor = int.Parse(cmbTipoProveedor.SelectedValue);
       cmbProveedor.Items.Clear();
       cmbProveedor.Items.Add(new ListItem("Seleccione...", "-1"));

       foreach (Antares.model.Proveedores prov in Antares.model.Proveedores.FindAllByProperty("IdCategoriaProveedor", tipo_proveedor))
        {
            cmbProveedor.Items.Add(new ListItem(prov.Denominacion, prov.IdProveedor.ToString()));
        }
        cmbProveedor.Enabled = true;
    }
    protected void LimpiaForm()
    {
        dtFechaFactura.Text = "";
        dtFechaRecepcion.Text = "";
        txtCabecera_NroFactura.Text = "";
        txtDescripcion.Text = "";
        txtImporteGravado.Text = "";
        txtImporteNOGravado.Text = "";
        txtImporteTotal.Text = "";
        txtNroFactura.Text = "";
        txtPlazoPago.Text = "";
        txtPorcentajeIVa.Text = "";
        CargarCombos();
    }

}
