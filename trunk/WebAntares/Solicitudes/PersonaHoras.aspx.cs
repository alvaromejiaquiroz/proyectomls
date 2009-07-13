using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Antares.model;
using NHibernate.Expression;

public partial class Solicitudes_PersonaHoras : System.Web.UI.Page
{
    static Personal p;
    static int IdSolicitudRecurso;
    static int IdSolicitud;
    static int IdPersona;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!Page.IsPostBack)
        {
            if ((Request.QueryString["Id"] != null))
            {
                IdSolicitudRecurso = int.Parse(Request.QueryString["Id"].ToString());

                SolicitudRecursosEmpleados s = SolicitudRecursosEmpleados.FindFirst(Expression.Eq("Id", IdSolicitudRecurso));
                //SolicitudRendicionPersonalHoras ph = SolicitudRendicionPersonalHoras.fin

                IdSolicitud = s.IdSolicitud;
                IdPersona = s.IdEmpleado;
                FechaRango r = Solicitud.PeriodoDesdeHasta(s.IdSolicitud);
                makeCboFecha(r);
                p = Personal.GetById(s.IdEmpleado.ToString());
                txtPersona.Text = p.Apellido + "," + p.Nombres;
            }
        }
        fillGrid();
        

    }

    private void fillGrid()
    {
        GridView1.DataSource = SolicitudRendicionPersonalHoras.GetPersonasHorasEnSolicitud(IdSolicitud,IdPersona);
        GridView1.DataBind();

    }

    private void makeCboFecha(FechaRango r)
    {

        DateTime fecha_Inicio;
        DateTime fecha_Fin;

        Solicitud sol = Solicitud.GetById(IdSolicitud);
        fecha_Fin = DateTime.MaxValue;
        fecha_Inicio = sol.FechaCreacion;
        switch (sol.Tipo.Descripcion)
        {
            case "Mantenimiento Correctivo":
                SolicitudCorrectivo sol_Cor = SolicitudCorrectivo.FindFirst(Expression.Eq("IdSolicitud", int.Parse(sol.RelacionadaCon)));
                    fecha_Inicio = sol_Cor.FechanotificacionCliente;


                break;

            case "Mantenimiento Preventivo":
                SolicitudPreventivo sol_Pre = SolicitudPreventivo.FindFirst(Expression.Eq("IdSolicitud", int.Parse(sol.RelacionadaCon)));
                    fecha_Inicio = DateTime.Parse(sol_Pre.FechaInicio);
                    
                break;
            case "Obras e Instalaciones":
                    SolicitudObra sol_Obr = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", int.Parse(sol.RelacionadaCon)));
                    fecha_Inicio= DateTime.Parse(sol_Obr.FechaInicio);
                    
                break;

                case "Tareas Generales":
                break;
                case "Francos Compensatorios":
                break;
                case "Licencias":
                break;
                
                case "Capacitacion":
                break;
                case "Otras":
                break;
                default:
                break;
        }
        
        //fecha = r.Inicio;

        

//        jDatePick1.CustomScript = @" function(input) {
//            return { minDate: new Date("+ r.Inicio.Year +","+ (r.Inicio.Month -1).ToString("00")  +","+ r.Inicio.Day.ToString("00") +"), maxDate: new Date("+ r.Fin.Year +","+( r.Fin.Month-1).ToString("00") +"," +r.Fin.Day.ToString("00")+")}; } ";

        jDatePick1.CustomScript = @" function(input) {

            return { minDate: new Date(" + fecha_Inicio.Year + "," + (fecha_Inicio.Month - 1).ToString("00") + "," + fecha_Inicio.Day.ToString("00") + "), maxDate: new Date(" + fecha_Fin.Year + "," + (fecha_Fin.Month - 1).ToString("00") + "," + fecha_Fin.Day.ToString("00") + ")}; } ";


        //while (fecha <= r.Fin)
        //{
        //    ddlFecha.Items.Add(new ListItem(fecha.ToString("dd/MM/yyyy")));
        //    fecha = fecha.AddDays(1);
        //}
    }
    protected void cmdGuardar_Click(object sender, EventArgs e)
    {
        DateTime fecha ;

        if (jDatePick1.Text != "")
        {

            fecha = DateTime.Parse(jDatePick1.Text);

            SolicitudRendicionPersonalHoras ph = SolicitudRendicionPersonalHoras.FindFirst(
                Expression.Eq("IdSolicitud", IdSolicitud),
                Expression.Eq("IdPersona", IdPersona),
                Expression.Eq("Fecha", fecha));


            if (ph == null)
            {
                ph = new SolicitudRendicionPersonalHoras();
            }

            ph.IdPersona = IdPersona;
            ph.IdSolicitud = IdSolicitud;
            ph.Fecha = fecha;
            ph.Horas = decimal.Parse(Tiempo1.Value);
            ph.Descripcion = txtDescripcion.Text;
            ph.SaveAndFlush();
            fillGrid();
        }


        ////if (jDatePick1.Text != "")
        ////{

        ////    fecha = jDatePick1.Text;
        ////    SolicitudRecursosEmpleados.DeleteAll("id_Solicitud=" + IdSolicitud + " and id_empleado = " + IdPersona+ " and fecha is null");
        ////    SolicitudRecursosEmpleados ph = SolicitudRecursosEmpleados.FindFirst(
        ////            Expression.Eq("IdEmpleado", p.IdEmpleados),
        ////            Expression.Eq("IdSolicitud", IdSolicitud),
        ////            Expression.Eq("Fecha", fecha));
        ////    if (ph == null)
        ////    {
        ////        ph = new SolicitudRecursosEmpleados();
        ////    }
        ////    ph.IdEmpleado = p.IdEmpleados;
        ////    //ph.IdSolicitudRecursoPersona = IdPersonalRecurso;
        ////    ph.IdSolicitud = IdSolicitud;
        ////    ph.Fecha = DateTime.Parse(jDatePick1.Text).ToString();
        ////    ph.Horas = decimal.Parse(Tiempo1.Value);
        ////    ph.Detalle = txtDescripcion.Text;
        ////    ph.SaveAndFlush();
        ////    fillGrid();
        ////}
        ////else
        ////{
        ////    lblMSG.Text = "Debe fijar una Fecha";
        ////}

        
    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRendicionPersonalHoras R = SolicitudRendicionPersonalHoras.FindFirst(Expression.Eq("Id", int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString())));
        R.Delete();
        fillGrid();
    }
}
