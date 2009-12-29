using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Antares.model;
using WebAntares;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;

public partial class Solicitudes_TiempoXPersona : System.Web.UI.Page
{
    static Antares.model.Personal _persona;
    static string Fin;
    static string Horas;


    protected Personal persona
    {
        get { return _persona; }
        set { _persona = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDesde.Text = DateTime.Today.ToString("dd/MM/yyyy");
            if (AntaresHelper.PuedeVer_Tiempos_X_Persona(BiFactory.Perfil.Detalle))
            {
                CargarCombos();
            }
            else
            {
                CargarCombos(BiFactory.Empleado.IdEmpleados);
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
    
    protected void CargarCombos(int IdEmpleado)
    {

        persona = Personal.FindOne(Expression.Eq("IdEmpleados", IdEmpleado));
        cmbPersonal.Items.Clear();
        cmbPersonal.Items.Add(new ListItem(persona.Apellido + "," + persona.Nombres, persona.IdEmpleados.ToString()));
    }
    
    protected void FillGrid()
    {
        int Semana = 0;
        DateTime fecha = DateTime.Parse("1900-01-01");

        if (txtSemanaAño.Text != string.Empty)
        {
            Semana = int.Parse(txtSemanaAño.Text);
        }
        if (txtDesde.Text != string.Empty)
        {

            fecha = DateTime.Parse(txtDesde.Text);
            
        }
       

        gvTiemposPreventivo.DataSource = null;
        gvTiemposPreventivo.DataBind();
        gvTiemposPreventivo.DataSource = Personal.GetHorasSolicitudes(persona.IdEmpleados.ToString(), fecha.ToString("yyyy-MM-dd"), Semana, 1);
        gvTiemposPreventivo.DataBind();

        gvTiemposCorrectivo.DataSource = null;
        gvTiemposCorrectivo.DataBind();
        gvTiemposCorrectivo.DataSource = Personal.GetHorasSolicitudes(persona.IdEmpleados.ToString(), fecha.ToString("yyyy-MM-dd"), Semana, 2);
        gvTiemposCorrectivo.DataBind();

        gvTiemposObra.DataSource = null;
        gvTiemposObra.DataBind();
        gvTiemposObra.DataSource = Personal.GetHorasSolicitudes(persona.IdEmpleados.ToString(), fecha.ToString("yyyy-MM-dd"), Semana, 6);
        gvTiemposObra.DataBind();

        gvCapacitacion.DataSource = Personal.GetHorasSolicitudes(persona.IdEmpleados.ToString(), fecha.ToString("yyyy-MM-dd"), Semana, 7);
        gvTareasGenerales.DataSource = Personal.GetHorasSolicitudes(persona.IdEmpleados.ToString(), fecha.ToString("yyyy-MM-dd"), Semana, 3);
        gvLicencias.DataSource = Personal.GetHorasSolicitudes(persona.IdEmpleados.ToString(), fecha.ToString("yyyy-MM-dd"), Semana, 5);

        gvCapacitacion.DataBind();
        gvTareasGenerales.DataBind();
        gvLicencias.DataBind();


        if (gvTiemposPreventivo.Rows.Count > 0)
        {
            pnlSolicitudesPreventivas.Visible = true;
        }

        if (gvTiemposCorrectivo.Rows.Count > 0)
        {
            pnlSolicitudesCorrectivas.Visible = true;
        }


        if (gvTiemposObra.Rows.Count > 0)
        {
            pnlObras.Visible = true;
        }

        if (gvCapacitacion.Rows.Count > 0)
        {
            pnlCapa.Visible = true;
        }

        if (gvLicencias.Rows.Count > 0)
        {
            pnlLicencias.Visible = true;
        }

        if (gvTareasGenerales.Rows.Count > 0)
        {
            pnlTG.Visible = true;
        }
        FillResumen();

    }

    protected void FillResumen()
    {
        DateTime fecha;
        int semana;
        if (txtDesde.Text != string.Empty)
        {
            fecha = DateTime.Parse(txtDesde.Text);
            lblTotalHorasTrabajadas.Text = Personal.GetHorasCargadas_Semana(persona.IdEmpleados, fecha).ToString();
            lblSemana.Text = AntaresHelper.Semana(fecha).ToString();
            lblInicioSemana.Text = AntaresHelper.PrimerDiaSemana(fecha).ToString("dd/MM/yyyy");
            lblUltimoDia.Text = AntaresHelper.UltimoDiaSemana(fecha).ToString("dd/MM/yyyy");
            pnlResumen.Visible = true;
        }

        if (txtSemanaAño.Text != string.Empty)
        {
            semana = int.Parse(txtSemanaAño.Text);
            lblTotalHorasTrabajadas.Text = Personal.GetHorasCargadas_Semana(persona.IdEmpleados, semana).ToString();
            lblSemana.Text = semana.ToString();
            lblInicioSemana.Text = AntaresHelper.PrimerDiaSemana(semana).ToString("dd/MM/yyyy");
            lblUltimoDia.Text = AntaresHelper.UltimoDiaSemana(semana).ToString("dd/MM/yyyy");
            pnlResumen.Visible = true;
        }
        
        
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (int.Parse(cmbPersonal.SelectedValue) > 0 )
            {
                persona = Personal.FindOne(Expression.Eq("IdEmpleados", int.Parse(cmbPersonal.SelectedValue)));
                FillGrid();
            }
            
        }
        
    }

    protected void cvSemanaFecha_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;

        if (cmbPersonal.SelectedIndex > 0)
        {
            args.IsValid = true;
        }
        if (txtDesde.Text != string.Empty || txtSemanaAño.Text != string.Empty)
        {
            args.IsValid = true;
        }
        else
        {
            cvSemanaFecha.ErrorMessage = "Debe seleccionar o la SEMANA o la FECHA de la Semana";
            args.IsValid = false;
        }
    }

    protected void gvTiemposPreventivo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (gvTiemposPreventivo.Rows.Count > 0)
        //{
        //    //gvTiemposPreventivo.HeaderRow.Cells[0].Visible = true;
        //   //e.Row.Controls[0].Visible = false;// column 2
        //}



    }

    protected void gvTiemposPreventivo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    TableCell cell = e.Row.Cells[1];
        //    cell.Visible = false;
        //    cell.BorderWidth = 0;
        //    cell.BorderStyle = BorderStyle.None;

        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Attributes.CssStyle.Add("display", "none");

            foreach (TableCell c in e.Row.Cells)
            {   
                c.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                //c.ForeColor = System.Drawing.Color.Orange;    
            }
            
        }
    }
}
