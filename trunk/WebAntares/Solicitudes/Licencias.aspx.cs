using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using WebAntares;
using Antares.model;
using NHibernate.Expression;

public partial class Solicitudes_Licencias : System.Web.UI.Page
{
    static int IdEmpleado;
    static int IdLicencia;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                IdLicencia = int.Parse(Request.QueryString["Id"].ToString());
                FillLicencia(IdLicencia);
            }

            CargarCombos();
            FillDataComplementaria();
            FillGrid(0);
        }
    }

    private void FillLicencia(int Id)
    {
        BiFactory.Sol = Solicitud.GetById(Id);
        SolicitudLicencias sol_Lic = SolicitudLicencias.FindFirst(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));

        cmbEmpleado.SelectedIndex = sol_Lic.IdEmpleado;
        cmbTipoLicencia.SelectedIndex = sol_Lic.IdTipolicencia;
        txtDescripcion.Text = sol_Lic.Descripcion;
        pnlContenido.Visible = true;
        FillGrid(0);

    }

    private void FillDataComplementaria()
    {
        txtInicio.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
        txtDuracion.Text = "9";
        pnlContenido.Visible = false;
    }
    
    private void CargarCombos()
    {
        cmbTipoLicencia.Items.Clear();
        cmbTipoLicencia.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.TipoLicencia Tl in Antares.model.TipoLicencia.FindAll())
        {
            cmbTipoLicencia.Items.Add(new ListItem(Tl.Descripcion, Tl.Id.ToString()));
        }

        cmbEmpleado.Items.Clear();
        cmbEmpleado.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Personal per in Antares.model.Personal.GetPersonalActivo())
        {
            cmbEmpleado.Items.Add(new ListItem(per.Apellido + "," + per.Nombres ,per.IdEmpleados.ToString()));
        }
    }

    protected void cmbEmpleado_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
    protected void FillGrid(int PageIndex)
    {
        gvLicencias.DataSource = SolicitudLicencias.FindAll(Expression.Eq("IdSolicitud", BiFactory.Sol.Id_Solicitud));
        gvLicencias.DataKeyNames = new string[] { "Id" };
        gvLicencias.PageIndex = PageIndex;
        gvLicencias.DataBind();

        //table = new DataTable();
        //DbDataReader reader = SolicitudGastos.GetGastosSolicitud(Sol.Id_Solicitud);
        //table.Load(reader);
        
        //gvGastos.DataSource = table;
        //gvGastos.PageIndex = pageIndex;
        //gvGastos.DataKeyNames = new string[] { "Id" };
        //gvGastos.DataBind();

    }
    
    protected void gvLicencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int32 Id = int.Parse(gvLicencias.DataKeys[e.RowIndex].Value.ToString());
        SolicitudLicencias item;
        item = SolicitudLicencias.FindFirst(Expression.Eq("Id", Id));
        item.Delete();
        FillGrid(0);

    }

    protected void gvLicencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(e.NewPageIndex);

    }

    protected void IniciaPanelLicencia()
    {
        txtInicio.Text = string.Empty;
        txtDuracion.Text = "9";


    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        IdEmpleado = int.Parse(cmbEmpleado.SelectedValue);
        Personal per = Personal.FindOne(Expression.Eq("IdEmpleados", IdEmpleado));

        if (IdEmpleado > 0 && IsValid)
        {

            Solicitud Sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            SolicitudLicencias sol_Lic = SolicitudLicencias.FindFirst(Expression.Eq("IdSolicitud", Sol.Id_Solicitud),Expression.Eq("FechaInicio",DateTime.Parse(txtInicio.Text)));
            if (sol_Lic == null)
            {
                sol_Lic = new Antares.model.SolicitudLicencias();
                sol_Lic.IdSolicitud = BiFactory.Sol.Id_Solicitud;
            

            sol_Lic.Descripcion = txtDescripcion.Text;
            sol_Lic.FechaInicio = DateTime.Parse(txtInicio.Text);
            sol_Lic.FechaFin = DateTime.Parse("1999-12-31");
            sol_Lic.Duracion = decimal.Parse(txtDuracion.Text);
            sol_Lic.IdTipolicencia = int.Parse(cmbTipoLicencia.SelectedItem.Value);

            sol_Lic.IdEmpleado = per.IdEmpleados;
            sol_Lic.Save();
            }
            FillGrid(0);
            
            txtInicio.Text = string.Empty;
            txtDuracion.Text = "9";

            //pnlLicencias.Visible = false;

            //ucLicencias.Numero = sol_Lic.IdSolicitud.ToString();
            //ucLicencias.Titulo = BiFactory.Sol.Descripcion;
            //ucLicencias.Tipo = cmbTipoLicencia.SelectedItem.Text;
            //ucLicencias.FechaInicio = sol_Lic.FechaInicio.ToString("dd/MM/yyyy"); 
            //ucLicencias.FechaFin = sol_Lic.FechaFin.ToString("dd/MM/yyyy"); 
            //ucLicencias.Duracion = sol_Lic.Duracion.ToString();
            //ucLicencias.Descripcion = sol_Lic.Descripcion;
            //ucLicencias.Visible = true;
        }

    }
   
    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        CargarCombos();
        IniciaPanelLicencia();
        pnlContenido.Visible = false;
        txtDescripcion.Text = string.Empty;
        cmbTipoLicencia.Enabled = true;
        cmbEmpleado.Enabled = true;
        txtDescripcion.Enabled = true;
        btnIniciarCarga.Enabled = true;
        BiFactory.Sol = null;
        Response.Redirect("~/Solicitudes/ListaLicencias.aspx");
        
    }

    protected void btnIniciarCarga_Click(object sender, EventArgs e)
    {
        pnlContenido.Visible = true;
        txtDescripcion.Enabled = false;
        cmbEmpleado.Enabled = false;
        cmbTipoLicencia.Enabled = false;
        btnIniciarCarga.Enabled = false;


    }

    protected void btnCancelarCarga_Click(object sender, EventArgs e)
    {
        BiFactory.Sol.Delete();
        BiFactory.Sol = null;
        Response.Redirect("~/default.aspx");

    }
}
