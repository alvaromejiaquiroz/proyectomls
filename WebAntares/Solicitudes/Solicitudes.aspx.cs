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
using Castle.ActiveRecord;

public partial class Solicitudes_Solicitudes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            cboTipoSolicitud1.rendercbo();
            
            habilitarSegunPerfil();
            
            fillGrid();
        }

    }

    private void habilitarSegunPerfil()
    {
        if (BiFactory.User.IdPerfil >1)
        {
            cboPersonal.BindCBO();
            cboPersonal.Value = BiFactory.Empleado.IdEmpleados.ToString();
            cboPersonal.Enabled = false;

        }
    }
    
    protected void CargarCombos()
    {


        cmbEstados.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Antares.model.Estados e in Antares.model.Estados.FindAll())
        {
            ListItem l = new ListItem(e.Detalle, e.IdEstado.ToString());
            cmbEstados.Items.Add(l);

        }

    }

    private void fillGrid()
    {
        System.Data.Common.DbDataReader reader = Antares.model.Solicitud.GetReader(txtNroSolicitud.Text.ToString(), cboTipoSolicitud1.value, cboPersonal.Value, cmbEstados.SelectedValue.ToString());

        GridView1.DataSource = reader;
      
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 IdSolicitud = Int32.Parse(e.CommandArgument.ToString());
        BiFactory.Sol = Solicitud.GetById(IdSolicitud);

        switch (e.CommandName)
        {
            case "CambiarEstado":
                Response.Redirect("./SolicitudesCambioEstado.aspx");
                break;
            case "Visualizar_Reporte":
                Response.Redirect("~/Reportes/MostrarSolicitud.aspx?id=" + IdSolicitud.ToString());
                break;
            case "Editar":
               //switch (BiFactory.Sol.Tipo.)
               //{
               // case "
                Response.Redirect("./intervencion.aspx?id=" + IdSolicitud.ToString() +"&Ac=e");
                break;
            case "Imprimir":
                Response.Redirect("~/Reportes/MostrarSolicitud.aspx?id=" + IdSolicitud.ToString());
                //Response.Redirect("./Reportes.aspx?id=" + IdSolicitud.ToString());
                break;
        }




    }
    
    private void CrearClone(int idSolOrg)
    {
        TransactionScope _transaction = new TransactionScope();
        try
        {
            //guarda la solicitud con la relacion
            Solicitud_Relacion SolRel = new Solicitud_Relacion();
            Solicitud sol;
            // antes de crear una copia verifico que no exista ya una
            SolRel = Solicitud_Relacion.FindOne(Expression.Eq("IdSolicitud_Relacionada", idSolOrg));
            if (SolRel == null)
            {
                SolRel = new Solicitud_Relacion();
                sol = Solicitud.GetById(idSolOrg);
                sol.Status = "Rendicion";
                sol.Id_Solicitud = 0;
                sol.SaveCopy();
                BiFactory.Sol = sol;
            }
            else
            {
                sol = Solicitud.GetById(SolRel.IdSolicitud);

                BiFactory.Sol = sol;
            }


            SolRel.IdSolicitud = sol.Id_Solicitud;
            SolRel.IdSolicitud_Relacionada = idSolOrg;
            SolRel.Save();

            //copio las tareas
            SolicitudTareas[] tt = SolicitudTareas.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
            foreach (SolicitudTareas t in tt)
            {
                t.Id = 0; //para que guarde una copia
                t.IdSolicitud = sol.Id_Solicitud;
                t.SaveCopy();
            }


            //copio los agentes
            SolicitudRecursosEmpleados[] ee = SolicitudRecursosEmpleados.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
            foreach (SolicitudRecursosEmpleados e in ee)
            {
                e.Id = 0;
                e.IdSolicitud = sol.Id_Solicitud;
                e.SaveCopy();
            }

            //copio los vehiculos
            SolicitudRecursosVehiculos[] vv = SolicitudRecursosVehiculos.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
            foreach (SolicitudRecursosVehiculos v in vv)
            {
                v.Id = 0;
                v.IdSolicitud = sol.Id_Solicitud;
                v.SaveCopy();
            }

        }
        catch (Exception oEx)
        {
            _transaction.VoteRollBack();
            throw;
        }
        finally
        {
            _transaction.Dispose();

            Response.Redirect("./MantPreventivoRendicion.aspx");
        }


    }
    
    protected void cmdBuscar_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            GridView1.DataSource = Antares.model.Solicitud.GetReader(txtNroSolicitud.Text.ToString(), cboTipoSolicitud1.value, cboPersonal.Value, cmbEstados.SelectedValue.ToString());
            GridView1.DataBind();
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (BiFactory.User.IdPerfil == 1)
        {
            GridView1.Columns[11].Visible = true;
        }
        else
        {
            GridView1.Columns[11].Visible = false;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
            negro: anulado
            rojo: pendiente
            naranja: suspendido
          naranja #FFAF6E (original)#FFAA66 (websafe)#C0C0C0 (greyscale)
             para anulado clavá este gris: #646464 (original)#666666 (websafe)#646464 (greyscale)

         *  negro = "#000000", 
            rojo= "#FF0000" , 
            naranja = "#FFAF6E", suspendido
            gris = "#646464", 
            verde = "#66FF99"
        
        */

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink lnkReporte = (HyperLink)e.Row.FindControl("lnkReporte");
            Image imgEditar = (Image)e.Row.FindControl("imgEdit");
            Image imgEstado = (Image)e.Row.FindControl("imgEstado");
            Image imgEliminar = (Image)e.Row.FindControl("imgEliminar");
            lnkReporte.Visible = false;

            if (BiFactory.User.IdPerfil == 1)
            {
                imgEliminar.Visible = true;

            }
            Image imgStatus = (Image)e.Row.FindControl("imgStatus");
            
            string valorResponsable = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Status"));
            switch (valorResponsable)
            {

                case "Anulada":
                    e.Row.Cells[4].Font.Bold = true;
                        //e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                        imgEditar.Visible = false;
                        imgEstado.Visible = false;
                        lnkReporte.Visible = false;
                        imgStatus.ImageUrl = "../images/gray.gif";
                        imgStatus.ToolTip = "Anulado";
                        break;
                    
                case "Pendiente":
                        //e.Row.Cells[4].Font.Bold = true;
                        //e.Row.Cells[4].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFAF6E");
                        //e.Row.Cells[4].ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                        imgStatus.ImageUrl = "../images/orange.gif";
                        imgStatus.ToolTip = "Pendiente";
                        break;

                case "Realizado":
                        //e.Row.Cells[4].Font.Bold = true;
                        imgEditar.Visible = false;
                        imgEstado.Visible = false;
                        //e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                        if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id_Reporte")) != null)
                        {
                            lnkReporte.Visible = true;
                        }
                        imgStatus.ImageUrl = "../images/green.gif";
                        imgStatus.ToolTip = "Realizado";
                        break;
                    
                case "Reprogramado":
                        //e.Row.Cells[4].Font.Bold = true;
                        //e.Row.Cells[4].ForeColor = System.Drawing.Color.Blue;
                        imgStatus.ImageUrl = "../images/yellow.gif";
                        imgStatus.ToolTip = "Reprogramado";
                        break;
                case "Suspendido":
                        //e.Row.Cells[4].Font.Bold = true;
                        imgEditar.Visible = false;
                        imgEstado.Visible = false;
                        //e.Row.Cells[4].ForeColor = System.Drawing.Color.Gray;
                        imgStatus.ImageUrl = "../images/red.gif";
                        imgStatus.ToolTip = "Suspendido";
                        break;
            }
        }
    }

    //    if (valorResponsable == eEstados.Realizado.ToString())
        //    {



        //        imgEditar.Visible = false;
        //        imgEstado.Visible = false;


        //        e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#66FF99");

        //        if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id_Reporte")) != null)
        //        {

        //            imgReporte.Visible = true;
        //        }


        //    }
        //    if (valorResponsable == eEstados.Suspendido.ToString())
        //    {


        //        imgEditar.Visible = false;
        //        imgEstado.Visible = false;

        //        e.Row.Cells[4].BackColor = System.Drawing.Color.LightGray;

        //    }

        //    if (valorResponsable == eEstados.Reprogramado.ToString())
        //    {

        //        e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF66");

        //    }
        //    if (valorResponsable == eEstados.Anulada.ToString() )
        //    if (valorResponsable == eEstados.Suspendido.ToString()
        //    {
        //        e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
        //        imgEditar.Visible = false;
        //        imgEstado.Visible = false;
        //        imgReporte.Visible = false;
        //    }

        //    if (valorResponsable == eEstados.Pendiente.ToString() )
        //    {
        //        e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        //        e.Row.Cells[4].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //    }
        //}




    
}
