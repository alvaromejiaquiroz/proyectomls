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
using Castle.ActiveRecord;

public partial class Solicitudes_SolicitudesCambioEstado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            OcultaPanel();
            LlenaCombo();
        }

    }
    protected void LlenaCombo()
    {
        cmbEstados.Items.Add(new ListItem("Seleccione...", "-1"));
        foreach (Estados e in Estados.FindAll(Expression.Sql("detalle not in ('Pendiente','Anulado')")) )
        {
            cmbEstados.Items.Add(new ListItem(e.Detalle, e.IdEstado.ToString()));
        }

    }
    protected void OcultaPanel()
    {
        pnlReprogramado.Visible = false;

    }
    protected void IniciaPanel()
    {
        txtAprobador.Text = "";
        txtCausa.Text = "";

            switch ((eEstados)int.Parse(cmbEstados.SelectedValue))
            {
                case eEstados.Reprogramado:
                    lblFecha.Text = "Fecha de Pedido de Reprogramacion";
                    DatePicker_Desde.Text = "";
                    DatePicker_Hasta.Text = "";
                    DatePicker_Desde.Visible = true;
                    DatePicker_Hasta.Visible = true;

                    break;

                case eEstados.Suspendido:
                    DatePicker_Desde.Visible = false;
                    DatePicker_Hasta.Visible = false;
                    lblFecha.Visible = false;
                    lblFecha.Text = "Fecha de Suspencion";
                    break;

                case eEstados.Realizado:
                    MakeRendicion(BiFactory.Sol.Id_Solicitud);
                    break;
                
            }
            dtpFechaProgramacion.Text = "";

        }
    
    private void MakeRendicion(int idSolOrg)
    {
        TransactionScope _transaction = new TransactionScope();
        Solicitud sol;
        sol = Solicitud.GetById(idSolOrg);
        try
        {
            //guarda la solicitud con la relacion
            Solicitud_Relacion SolRel = new Solicitud_Relacion();

            // antes de crear una copia verifico que no exista ya una
            SolRel = Solicitud_Relacion.FindOne(Expression.Eq("IdSolicitud_Relacionada", idSolOrg));
            if (SolRel == null)
            {
                SolRel = new Solicitud_Relacion();
                sol = Solicitud.GetById(idSolOrg);
                sol.Status = eEstados.Realizado.ToString();
                sol.Id_Solicitud = 0;
                sol.Reporte = "SI";
                sol.SaveCopy();
                BiFactory.Sol = sol;

                TipoSolicitud tipo = sol.Tipo;

                if (tipo.IdTiposolicitud == 1) ///Mantenimiento Preventivo
                {
                    SolicitudPreventivo Sol_Prev = SolicitudPreventivo.FindOne(Expression.Eq("IdSolicitud", idSolOrg));
                    Sol_Prev.Id = 0;
                    Sol_Prev.IdSolicitud = sol.Id_Solicitud;
                    Sol_Prev.SaveCopy();


                    //copio las tareas
                    SolicitudTareas[] tt = SolicitudTareas.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
                    foreach (SolicitudTareas t in tt)
                    {
                        t.Id = 0; //para que guarde una copia
                        t.IdSolicitud = sol.Id_Solicitud;
                        t.SaveCopy();
                    }


                    //copio los servicios afectados
                    SolicitudServiciosAfectados[] ss = SolicitudServiciosAfectados.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
                    foreach (SolicitudServiciosAfectados s in ss)
                    {
                        s.Id = 0;
                        s.IdSolicitud = sol.Id_Solicitud;
                        s.SaveCopy();
                    }

                }



                if (tipo.IdTiposolicitud == 2) ///Mantenimiento Correctivo
                {
                    SolicitudCorrectivo Sol_Corr = SolicitudCorrectivo.FindOne(Expression.Eq("IdSolicitud", idSolOrg));
                    Sol_Corr.Id = 0;
                    Sol_Corr.IdSolicitud = sol.Id_Solicitud;
                    Sol_Corr.SaveCopy();

                    
                    //copio los servicios afectados
                    SolicitudServiciosAfectados[] ss = SolicitudServiciosAfectados.FindAll(Expression.Eq("IdSolicitud", idSolOrg));
                    foreach (SolicitudServiciosAfectados s in ss)
                    {
                        s.Id = 0;
                        s.IdSolicitud = sol.Id_Solicitud;
                        s.SaveCopy();
                    }
                    
                }

                if (tipo.IdTiposolicitud == 6) ///Obras e Instalaciones
                {

                    
                        SolicitudObra Sol_Obra = SolicitudObra.FindOne(Expression.Eq("IdSolicitud",idSolOrg));
                        Sol_Obra.Id = 0;
                        Sol_Obra.IdSolicitud = sol.Id_Solicitud;
                        Sol_Obra.SaveCopy();

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
                    v.Horas = 0;
                    v.Km = 0;
                    v.SaveCopy();
                 
                }
            }
            else
            {
                sol = Solicitud.GetById(SolRel.IdSolicitud);

                BiFactory.Sol = sol;
            }


            SolRel.IdSolicitud = sol.Id_Solicitud;
            SolRel.IdSolicitud_Relacionada = idSolOrg;
            SolRel.Save();



        }
        catch (Exception oEx)
        {
            _transaction.VoteRollBack();
            throw;
        }
        finally
        {
            _transaction.Dispose();

            switch (sol.IdTipoSolicitud)
            {
                case 1:
                    Response.Redirect("./MantPreventivoRendicion.aspx");
                    break;
                case 2:
                    Response.Redirect("./MantCorrectivoRendicion.aspx");
                    break;
                case 6:
                    Response.Redirect("./Reporte_Obras.aspx");
                    break;
                default:
                    Response.Write("No se con que rendir");
                    break;
            }



        }



    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (cmbEstados.SelectedValue != "-1")
        {
                IniciaPanel();
                pnlReprogramado.Visible = true;
        }


    }

    public void btnAceptar_S_Click(object sender, EventArgs e)
        {
            Solicitud sol = Solicitud.GetById(BiFactory.Sol.Id_Solicitud);
            sol.Causa = txtCausa.Text;
            sol.AprobadorReprosusp = txtAprobador.Text;

            if (int.Parse(cmbEstados.SelectedValue) > 0)
            {

                switch (cmbEstados.SelectedItem.ToString())
                {

                    case "Reprogramado":
                        //sol.Status = "REPROGRAMADO";
                        sol.Status = eEstados.Reprogramado.ToString();
                        sol.FechaReprogramacion = dtpFechaProgramacion.Text;
                        sol.ProximaFechaInicio = DatePicker_Desde.Text;
                        sol.ProximaFechaFin = DatePicker_Hasta.Text;

                        break;

                    case "Cancelado":
                        sol.Status = eEstados.Suspendido.ToString();
                        sol.FechaSuspencion = dtpFechaProgramacion.Text;
                        break;

                }
                sol.Update();
                pnlReprogramado.Visible = false;
                Response.Redirect("./Solicitudes.aspx");


                /*
                sol.IdPlazoAtencion = int.Parse(cmbPlazoAtencion.SelectedValue);
                sol.IdCliente = int.Parse(cmbClientes.SelectedValue);
                sol.Contacto = txtContactoCliente.Text;
                sol.NroOrdenCte = txtNroOrdenCliente.Text;
                sol.Status = "PENDIENTE";
                sol.Update();
            */         
            }

        }
    
    protected void btnCancelar0_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Solicitudes.aspx");
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Solicitudes.aspx");
    }

}
