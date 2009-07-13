using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Antares.model;
using NHibernate.Expression;

public partial class Solicitudes_VehiculosHoras : System.Web.UI.Page
{
    static Vehiculos p;
    static int IdSolicitud;
    static int IdVehiculoRecurso;
    static int IdVehiculo;

    protected void Page_Load(object sender, EventArgs e)
    {



        if ( (Request.QueryString["Id"] != null))
        {
            IdVehiculoRecurso = int.Parse(Request.QueryString["Id"].ToString());
            

            SolicitudRecursosVehiculos s = SolicitudRecursosVehiculos.FindFirst(Expression.Eq("Id",IdVehiculoRecurso));
            IdVehiculo = s.IdVehiculo;
            IdSolicitud = s.IdSolicitud;
            FechaRango r = Solicitud.PeriodoDesdeHasta(s.IdSolicitud);
            makeCboFecha(r);
            p = Vehiculos.GetById(s.IdVehiculo.ToString());
            txtVehiculo.Text =  p.Patente + " - " +  p.Marca + "," + p.Modelo;
        }

        fillGrid();

    }

    private void fillGrid()
    {
        GridView1.DataSource = SolicitudRendicionVehiculosHoras.GetVehiculosKm_Detalle_EnSolicitud(IdSolicitud, IdVehiculo);
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
                    SolicitudObra sol_Obr = SolicitudObra.FindFirst(Expression.Eq("IdSolicitud", sol.Id_Solicitud));
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


            SolicitudRendicionVehiculosHoras ph = SolicitudRendicionVehiculosHoras.FindFirst(
            Expression.Eq("IdSolicitud", IdSolicitud),
            Expression.Eq("IdVehiculo", IdVehiculo),
            Expression.Eq("Fecha", fecha));


            if (ph == null)
            {
                ph = new SolicitudRendicionVehiculosHoras();
            }
            ph.IdVehiculo = p.IdVehiculos;
            ph.IdSolicitud = IdSolicitud;
            ph.Fecha = fecha;
            ph.Horas = decimal.Parse(Tiempo1.Value);
            ph.Descripcion = txtDescripcion.Text;
            //ph.Kilometros = decimal.Parse(txtKilometros.Text);
            ph.Kilometros = txtKilometros.Text;
            ph.SaveAndFlush();
            fillGrid();

        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SolicitudRendicionVehiculosHoras R = SolicitudRendicionVehiculosHoras.FindFirst(Expression.Eq("Id", int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString())));
        R.Delete();
        fillGrid();

    }
}
