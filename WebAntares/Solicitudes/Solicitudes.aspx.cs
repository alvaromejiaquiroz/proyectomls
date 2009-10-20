﻿using System;
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
using System.Data.Common;

public partial class Solicitudes_Solicitudes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarCombos();
            cboTipoSolicitud1.rendercbo();
            
            habilitarSegunPerfil();
            
        }

    }

    private void FillGrid(int pageIndex)
    {
//        DbDataReader reader = Antares.model.Solicitud.GetReader(IdSolicitud, TipoSolicitud,PerfilUsuario,IdResponsable ,IdEmpleadoUsuario, Estado , Fecha);
        DbDataReader reader = Antares.model.Solicitud.GetReader(IdSolicitud, TipoSolicitud, IdEmpleado.ToString(), Estado, Fecha, PerfilUsuario);
        DataTable table = new DataTable();
        table.Load(reader);
        GridView1.DataSource = table;
        GridView1.PageIndex = pageIndex;
        GridView1.DataBind();
    }

    private void habilitarSegunPerfil()
    {
        PerfilUsuario = BiFactory.User.IdPerfil.ToString();
        IdEmpleadoUsuario = BiFactory.Empleado.IdEmpleados;

        if (BiFactory.User.IdPerfil >3)
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
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
                    Response.Redirect("./intervencion.aspx?id=" + IdSolicitud.ToString() + "&Ac=e");
                    break;
                case "Imprimir":
                    Response.Redirect("~/Reportes/MostrarSolicitud.aspx?id=" + IdSolicitud.ToString());
                    break;
            }
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
            IdSolicitud = txtNroSolicitud.Text;
            TipoSolicitud = cboTipoSolicitud1.value;
            IdEmpleado = cboPersonal.Value;
            Estado = cmbEstados.SelectedValue;
            Fecha = txtDesde.Text;

         //   System.Threading.Thread.Sleep(1000);
    
            FillGrid(0);
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //if (BiFactory.User.IdPerfil == 1)
        //{
        //    GridView1.Columns[11].Visible = true;
        //}
        //else
        //{
        //    GridView1.Columns[11].Visible = false;
        //}
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Solicitud S = Solicitud.FindOne(Expression.Eq("Id_Solicitud", (DataBinder.Eval(e.Row.DataItem, "Solicitud"))));
            HyperLink lnkReporte = (HyperLink)e.Row.FindControl("lnkReporte");
            HyperLink lnkVisualizar = (HyperLink)e.Row.FindControl("lnkReporte");
            DateTime FechaInicio = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Fecha"));

            Image imgEditar = (Image)e.Row.FindControl("imgEdit");
            Image imgEstado = (Image)e.Row.FindControl("imgEstado");
            Image imgCambiarEstado = (Image)e.Row.FindControl("imgCambiarEstado");
            Image imgEstadoCoord = (Image)e.Row.FindControl("imgStatusCoord");
            Image imgEstadoCalidad = (Image)e.Row.FindControl("imgStatusCalidad");
            Image imgEliminar = (Image)e.Row.FindControl("imgEliminar");

            lnkReporte.Visible = false;
            
            //me fijo si la solicitud esta habilitada por calidad o la gerencia tecnica
            string aprobacion;
            aprobacion= DataBinder.Eval(e.Row.DataItem, "A_TEC").ToString();
           
            imgEstadoCoord.ToolTip = "Bloqueado";
            imgEstadoCalidad.ToolTip = "Bloqueado";

            imgEstadoCalidad.ImageUrl = "../images/candado.gif";
            imgEstadoCoord.ImageUrl = "../images/candado.gif";
            if (aprobacion != string.Empty)
            {
                if (bool.Parse(aprobacion))
                {
                    imgEstadoCoord.ImageUrl = "../images/adelante.gif";
                    imgEstadoCoord.ToolTip = "Aprobado";
                }
            } 
            aprobacion = DataBinder.Eval(e.Row.DataItem, "A_CAL").ToString();
            if (aprobacion != string.Empty)
            {
                if (bool.Parse(aprobacion))
                {
                    imgEstadoCalidad.ImageUrl = "../images/adelante.gif";
                    imgEstadoCalidad.ToolTip = "Aprobado";
                }
            }
            
            string valorEstado = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Status"));
            string valorTipoSolicitud  = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Tipo"));
            string valorIdResponsable  = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Id_Responsable"));

            imgEditar.Visible = false;
            imgCambiarEstado.Visible = true;

            if (BiFactory.Empleado.IdEmpleados == int.Parse(valorIdResponsable))
            {
                imgEditar.Visible = true;
                imgEstado.Visible = true;

            }
           
            if (BiFactory.User.IdPerfil < 4)
            {
                imgEditar.Visible = true;
                imgEstado.Visible = true;
            }

            
                        
            switch (valorEstado )
            {
                case "Anulado":
                        e.Row.Cells[4].Font.Bold = true;
                        imgEditar.Visible = false;
                        lnkReporte.Visible = false;
                        imgEstado.ImageUrl = "../images/deshabilitado.gif";
                        imgEstado.ToolTip = "Anulado";
                        break;
                    
                case "Pendiente":
                        imgEstado.ImageUrl = "../images/pendiente.gif";
                        imgEstado.ToolTip = "Pendiente";
                        if (FechaInicio < DateTime.Today)
                        {
                            imgEstado.ImageUrl = "../images/vencido.gif";
                            imgEstado.ToolTip = "VENCIDO: se ha exedido el plazo para la realización de esta Solicitud";

                        }
                        
                        break;
                case "Realizado":
                        imgEditar.Visible = false;
                        imgCambiarEstado.Visible = false;
                        imgEstado.ImageUrl = "../images/realizado.gif";
                        imgEstado.ToolTip = "Realizado";
                        imgEstadoCoord.ImageUrl = "../images/pendiente.gif";
                        imgEstadoCalidad.ImageUrl = "../images/pendiente.gif";
                        imgEstadoCoord.ToolTip = "Pendiente de Aprobación TECNICA";
                        imgEstadoCalidad.ToolTip = "Pendiente de Aprobación de CALIDAD";
                        lnkReporte.Visible = true;
                        break;
                case "Reprogramado":
                        imgEstado.ImageUrl = "../images/reprogramado.gif";
                        imgEstado.ToolTip = "REPROGRAMADO: " + S.Causa;
                        if (FechaInicio < DateTime.Today)
                        {
                            imgEstado.ImageUrl = "../images/vencido.gif";
                            imgEstado.ToolTip = "VENCIDO: se ha exedido el plazo para la realización de esta Solicitud";

                        }
                        break;
                case "Cancelado":
                        imgEditar.Visible = false;
                        imgCambiarEstado.Visible = false;
                        imgEstado.ImageUrl = "../images/cancelado.gif";
                        imgEstado.ToolTip = "CANCELADO: " + S.Causa;
                        break;
                }

            
        }
    }

    public string IdSolicitud
    {
        get { return ViewState["IdSolicitud"].ToString(); }
        set { ViewState["IdSolicitud"] = value; }
    }

    public string TipoSolicitud
    {
        get { return ViewState["TipoSolicitud"].ToString(); }
        set { ViewState["TipoSolicitud"] = value; }
    }

    public string IdEmpleado
    {
        get { return ViewState["IdEmpleado"].ToString(); }
        set { ViewState["IdEmpleado"] = value; }
    }
  
    public string IdUsuario
    {
        get { return ViewState["IdUsuario"].ToString(); }
        set { ViewState["IdUsuario"] = value; }
    }

    public string Estado
    {
        get { return ViewState["Estado"].ToString(); }
        set { ViewState["Estado"] = value; }
    }

    public string Fecha
    {
        get { return ViewState["Fecha"].ToString(); }
        set { ViewState["Fecha"] = value; }
    }
    
    public string PerfilUsuario
    {
        get { return ViewState["PerfilUsuario"].ToString(); }
        set { ViewState["PerfilUsuario"] = value; }
    }
    
    public int IdEmpleadoUsuario
    {
        get { return int.Parse(ViewState["IdEmpleadoUsuario"].ToString()); }
        set { ViewState["IdEmpleadoUsuario"] = value; }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FillGrid(e.NewPageIndex);
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell c in e.Row.Cells)
            {
                if (int.Parse(PerfilUsuario) < 4) 
                {
                    c.Visible = true;
                    switch (c.Text)
                    {
                        //AT  AC  Editar Solicitud Cambiar Estado 
                        case "Cambiar Estado":
                            c.Visible = true;
                            break;

                 
                    }
                }
            }

            
        }
      

    }
}
