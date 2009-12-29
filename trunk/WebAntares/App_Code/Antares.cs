using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using System.Net.Mail;
using Antares.model;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using WebAntares;
using Castle.ActiveRecord.Framework;
using System.Collections;

namespace WebAntares
{

    public class AntaresHelper
    {
        
        public AntaresHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string EnviaMail(string subject, string mensaje)
        {
            string resultado = "OK";
            try
            {
                System.Net.Mail.SmtpClient s = new System.Net.Mail.SmtpClient();
                MailMessage msg = new MailMessage();
                MailAddress from = new MailAddress("adm.reg@antares.com.ar");


                msg.From = from;
                msg.IsBodyHtml = true;
                msg.Subject = subject.ToString();
                msg.Body = mensaje;
                //msg.To.Add("1154215946@sms.ctimovil.com.ar");
                msg.To.Add("daniela.pina@gmail.com");
                msg.To.Add("matias.lapera@gmail.com");
                s.Send(msg);
                
            }
            catch (SmtpException Ex)
            {
                return resultado = (Ex.Message);
            }

            return resultado;
        }

        public static string EnviaSMS( string mensaje)
        {
            string resultado = "OK";
            try
            {
                System.Net.Mail.SmtpClient s = new System.Net.Mail.SmtpClient();
                MailMessage msg = new MailMessage();
                MailAddress from = new MailAddress("adm.reg@antares.com.ar");

                msg.From = from;
                msg.IsBodyHtml = false;
                msg.Body = mensaje;
                msg.To.Add("1154215959@sms.ctimovil.com.ar");
                msg.To.Add("1154215955@sms.ctimovil.com.ar");
                s.Send(msg);
            }
            catch (SmtpException Ex)
            {
                return resultado = (Ex.Message);
            }

            return resultado;
        }

        public static void SendMail()
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.From = new MailAddress("lapera.matias@gmail.com");


            foreach (MailAddress unTo in TraeContactos_DeLista_Correo("minga"))
            {
                mailMessage.CC.Add(unTo);
            }
            mailMessage.Subject =  "Testing" + DateTime.Today.ToString();
            mailMessage.Body = DateTime.Now.ToString();

            SmtpClient smtpClient = new SmtpClient();
            Object userState = mailMessage;

            //Attach event handler for async callback
            smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);

            try
            {
                //Send the email asynchronously
                smtpClient.SendAsync(mailMessage, userState);
            }
            catch (SmtpException smtpEx)
            {
                //Error handling here
                string mensaje = smtpEx.Message;
                if (smtpEx.InnerException != null)
                {
                    mensaje +=  " " + smtpEx.InnerException.Message;
                }
                Logger.LogToFile("EMAIL", mensaje);
            }
            catch (Exception ex)
            {
                //Error handling here
            }
        }

        public static void SQLSendMail_a_Lista(string lista, string mensaje, string subject)
        {
            string destinatarios;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            foreach (MailAddress unTo in TraeContactos_DeLista_Correo(lista))
            {
                mailMessage.To.Add(unTo);
            }

            destinatarios = mailMessage.To.ToString();
            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@para", SqlDbType.VarChar);
            command.Parameters.Add("@cc", SqlDbType.VarChar);
            command.Parameters.Add("@bcc", SqlDbType.VarChar);
            command.Parameters.Add("@mensaje", SqlDbType.VarChar);
            command.Parameters.Add("@titulo", SqlDbType.VarChar);

            command.Parameters["@para"].Value = destinatarios.Replace(",",";").Trim(); ;
            command.Parameters["@mensaje"].Value = mensaje;
            command.Parameters["@titulo"].Value = subject;

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "sp_EnviaEmail";

            try
            {
                db.Open();
                command.ExecuteNonQuery();
                db.Close();
            }
            catch (SqlException ex)
            {
                string ex_mensaje;
                ex_mensaje = ex.Message;
                if ( ex.InnerException != null)
                {
                    ex_mensaje += ex.InnerException.Message;
                }
                Logger.LogToFile("SQL", ex_mensaje);
            }
           
           }

        /// <summary>
        /// Event handler for processing completion information after asynchronous email sent.
        /// </summary>
        private static  void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            //Get UserState as MailMessage instance from SendMail()
            MailMessage mailMessage = e.UserState as MailMessage;
            
            if (e.Cancelled)
            {
              Logger.LogToFile("EMAIL","El envio de mails fue cancelado. Address=" + mailMessage.To[0].Address);
            }

            if (e.Error != null)
            {
                
                Logger.LogToFile("EMAIL", "Ocurrio un Error ,info=" + e.Error.Message);
            }
            else
            {
                Logger.LogToFile("EMAIL", "Email Enviado Exitosamente " +  mailMessage.To[0].Address);
            }

        }

        private  static MailAddressCollection  TraeContactos_DeLista_Correo(string lista)
        {
            MailAddressCollection listaMails = new MailAddressCollection();
            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@lista", SqlDbType.VarChar);
            command.Parameters["@lista"].Value = lista;

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Proc_Mails_En_Lista";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {   

                    if (dr["email"] != null && dr["nombre"] != null)
                    {
                        MailAddress unContacto = new MailAddress(dr["email"].ToString(), dr["nombre"].ToString());
                        listaMails.Add(unContacto);
                    }
                    
                   
                }
            }
            db.Close();

            return listaMails;

            
        }
      
        public static void NotificaSolicitud(int idSol)
        {
            string subject = AntaresHelper.Get_Config_Ambiente();
            string mensaje = "Creacion Solicitud";
            string mensaje_sms = "SMS";
           // Personal p = Personal.FindOne(Expression.Eq("idempleados",BiFactory.Empleado.IdEmpleados));

            if (idSol > 0)
            {
                BiFactory.Sol = Solicitud.GetById(idSol);

                mensaje = BiFactory.User.Nombre + " creo la Solicitud Nro. " + BiFactory.Sol.Id_Solicitud.ToString() + " - Tipo " + BiFactory.Sol.Tipo.Descripcion;
                mensaje_sms = "Nueva Solicitud " + BiFactory.Sol.Id_Solicitud.ToString() + " - " + BiFactory.Sol.Tipo.DescripcionCorta+  " - " + BiFactory.User.Nombre;
                                 
                WebAntares.AntaresHelper.EnviaMail(subject, mensaje);
                WebAntares.AntaresHelper.EnviaSMS(mensaje_sms);
            }
            else
            {
                WebAntares.AntaresHelper.EnviaMail(subject, "Este es un Mensaje de prueba");
            }
            Loguea_Evento(mensaje);

        }

        public static void Loguea_Evento(string texto)
        {
            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameter p = new SqlParameter("@qry", SqlDbType.VarChar);
            p.Value = texto;
            p.Direction = ParameterDirection.Input;
            command.Parameters.Add(p);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "sp_logger";
            db.Open();
            command.ExecuteNonQuery();
            db.Close();

            
        }

        public static DbDataReader GetListaEstadosAutorizados(string Perfil)
        {
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            
            SqlParameterCollection p;
            
            oConn.Parameters.Add(new SqlParameter("@perfil",Perfil));
            oConn.Parameters.Add(new SqlParameter("@objeto","Estados"));
            oConn.CommandType = CommandType.StoredProcedure;
            oConn.CommandText = "Proc_GetAcciones";
            DbDataReader dr =  oConn.ExecuteReader();
            return dr;

            //string[] listaEstados;
            //ArrayList lista = new ArrayList();
            
            //AccionValores[] Lista = new AccionValores();

            //while (dr.Read())
            //{
            //    if (dr.HasRows)
            //    {
            //        AccionValores r = new AccionValores();
            //        if (dr["Detalle"] != System.DBNull.Value)
            //        {
            //            r.Perfil = dr["Detalle"].ToString();

            //        }

            //        if (dr["valor"] != System.DBNull.Value)
            //        {
            //            r.Valor = dr["valor"].ToString();

            //        }
            //        lista.Add(r);

            //    }

            //}
            //return lista;


        }

        public static bool GetPuedeAprobacionCalidad(string Perfil)
        {
            bool valor= false;
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();

            SqlParameterCollection p;

            oConn.Parameters.Add(new SqlParameter("@perfil", Perfil));
            oConn.Parameters.Add(new SqlParameter("@objeto", "AprobacionCalidad"));
            oConn.CommandType = CommandType.StoredProcedure;
            oConn.CommandText = "Proc_GetAcciones";
            DbDataReader dr = oConn.ExecuteReader();
            while(dr.Read())
            {
                if (dr.HasRows)
                {

                    valor = bool.Parse(dr["valor"].ToString());
                }

            }

            return valor;




        }
    
        public static bool GetPuedeAprobacionTecnica(string Perfil)
        {
            bool valor=false;
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();

            SqlParameterCollection p;

            oConn.Parameters.Add(new SqlParameter("@perfil", Perfil));
            oConn.Parameters.Add(new SqlParameter("@objeto", "AprobacionTecnica"));
            oConn.CommandType = CommandType.StoredProcedure;
            oConn.CommandText = "Proc_GetAcciones";
            DbDataReader dr = oConn.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {

                    valor = bool.Parse(dr["valor"].ToString());
                }

            }

            return valor;

            return valor;




        }

        public static bool GetPuedeBuscar_Listado_Capacitacion(string Perfil)
        {
            bool valor = false;
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();

            SqlParameterCollection p;

            oConn.Parameters.Add(new SqlParameter("@perfil", Perfil));
            oConn.Parameters.Add(new SqlParameter("@objeto", "BuscarCapacitacion"));
            oConn.CommandType = CommandType.StoredProcedure;
            oConn.CommandText = "Proc_GetAcciones";
            DbDataReader dr = oConn.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {

                    valor = bool.Parse(dr["valor"].ToString());
                }

            }

            return valor;

            return valor;




        }
        
        public static bool GetPuedeBuscar_Listado_TareasGenerales(string Perfil)
        {
            bool valor = false;
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();

            SqlParameterCollection p;

            oConn.Parameters.Add(new SqlParameter("@perfil", Perfil));
            oConn.Parameters.Add(new SqlParameter("@objeto", "BuscarTareasGenerales"));
            oConn.CommandType = CommandType.StoredProcedure;
            oConn.CommandText = "Proc_GetAcciones";
            DbDataReader dr = oConn.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {

                    valor = bool.Parse(dr["valor"].ToString());
                }

            }

            return valor;

            return valor;




        }
      
        public static bool PuedeVer_Tiempos_X_Persona(string Perfil)
        {
            bool valor = false;
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();

            SqlParameterCollection p;

            oConn.Parameters.Add(new SqlParameter("@perfil", Perfil));
            oConn.Parameters.Add(new SqlParameter("@objeto", "Ver_Tiempos_X_Persona"));
            oConn.CommandType = CommandType.StoredProcedure;
            oConn.CommandText = "Proc_GetAcciones";
            DbDataReader dr = oConn.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {

                    valor = bool.Parse(dr["valor"].ToString());
                }

            }

            return valor;

            return valor;




        }

        public static string Get_Config_HorasPersonaSemana()
        {
            
            string valor = ConfigurationSettings.AppSettings["HorasPersonaSemana"];
            return valor;
        }
        
        public static decimal Get_Config_HorasPersonaSemana_Fecha(DateTime fecha)
        {

            decimal valor = 0;
            
            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@fecha", SqlDbType.DateTime);
            command.Parameters["@fecha"].Value = fecha.ToShortDateString();

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Prc_Get_InfoSemana";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["horassemana"] != null)
                    {
                        valor = decimal.Parse(dr["horassemana"].ToString());
                    }
                }
            }
            db.Close();

            return valor;
            
        }

        public static string Get_Config_HorasPersonaDia()
        {

            string valor = ConfigurationSettings.AppSettings["HorasPersonaDia"];
            return valor;
        }

        public static string Get_Config_MailCoordinacionTecnica()
        {

            string valor = ConfigurationSettings.AppSettings["MailCoordinacionTecnica"];
            return valor;
        }

        public static string Get_Config_VersionSistema()
        {

            string valor = ConfigurationSettings.AppSettings["VersionSistema"];
            return valor;
        }

        public static string Get_Config_NombreSistema()
        {

            string valor = ConfigurationSettings.AppSettings["NombreSistema"];
            return valor;
        }

        public static string Get_Config_Ambiente()
        {

            string valor = ConfigurationSettings.AppSettings["AmbienteSistema"];
            return valor;
        }

        public static DateTime UltimoDiaSemana(DateTime fecha)
        {
            DateTime ultimodia = DateTime.MaxValue;

            SqlConnection db =  WebAntares.CustomDAL.Get_Connection();
            SqlCommand  command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@fecha", SqlDbType.DateTime);
            command.Parameters["@fecha"].Value = fecha.ToShortDateString();
            
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Prc_Get_InfoSemana";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["fechafin"] != null)
                    {
                        ultimodia = DateTime.Parse(dr["fechafin"].ToString());
                    }
                }
            }
            db.Close();

            return ultimodia;

        }

        public static DateTime UltimoDiaSemana(int semana)
        {
            DateTime ultimodia = DateTime.MaxValue;

            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@semana_buscar", SqlDbType.Int);
            command.Parameters["@semana_buscar"].Value = semana;

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Prc_Get_InfoSemana";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["fechafin"] != null)
                    {
                        ultimodia = DateTime.Parse(dr["fechafin"].ToString());
                    }
                }
            }
            db.Close();

            return ultimodia;

        }

        public static DateTime UltimoDiaSemanaSiguiente(DateTime fecha)
        {
            DateTime ultimodia = DateTime.MaxValue;

            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@fecha", SqlDbType.DateTime);
            command.Parameters["@fecha"].Value = fecha.ToShortDateString();

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Prc_Get_UltimoDiaSemanaSiguiente";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["fechafin"] != null)
                    {
                        ultimodia = DateTime.Parse(dr["fechafin"].ToString());
                    }
                }
            }
            db.Close();

            return ultimodia;

        }

        public static DateTime PrimerDiaSemana(DateTime fecha)
        {
            DateTime primerdia = DateTime.MaxValue;

            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@fecha", SqlDbType.DateTime);
            command.Parameters["@fecha"].Value = fecha.ToShortDateString();

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Prc_Get_InfoSemana";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["fechainicio"] != null)
                    {
                        primerdia = DateTime.Parse(dr["fechainicio"].ToString());
                    }
                }
            }
            db.Close();

            return primerdia;

        }

        public static DateTime PrimerDiaSemana(int semana)
        {
            DateTime primerdia = DateTime.MaxValue;

            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@semana_buscar", SqlDbType.Int);
            command.Parameters["@semana_buscar"].Value = semana;

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Prc_Get_InfoSemana";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["fechainicio"] != null)
                    {
                        primerdia = DateTime.Parse(dr["fechainicio"].ToString());
                    }
                }
            }
            db.Close();

            return primerdia;

        }

        public static DateTime FechaNula()
        {
            DateTime dt = DateTime.Parse("1900-01-01");
            return dt;
        }

        public static int Semana(DateTime fecha)
        {
            int semana = 0;
            SqlConnection db = WebAntares.CustomDAL.Get_Connection();
            SqlCommand command = db.CreateCommand();

            SqlParameterCollection p;
            command.Parameters.Add("@fecha", SqlDbType.DateTime);
            command.Parameters["@fecha"].Value = fecha.ToShortDateString();

            command.CommandType = CommandType.StoredProcedure;
            command.Connection = db;
            command.CommandText = "Prc_Get_InfoSemana";
            db.Open();
            DbDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    if (dr["semana"] != null)
                    {
                        semana = int.Parse(dr["semana"].ToString());
                    }
                }
            }
            db.Close();

            return semana;

        }

        public static bool PuedeGenerarReporte(DateTime fecha)
        {
            bool puede =true;
            DateTime primerDiaSemana;
            DateTime ultimoDiaSemana;
            int semana_a_cargar;
            int semana_hoy;
            primerDiaSemana = PrimerDiaSemana(fecha);
            ultimoDiaSemana = UltimoDiaSemana(fecha);
            //semana_hoy = Semana(fecha);
            semana_hoy = Semana(DateTime.Today);
            semana_a_cargar = Semana(fecha);

            if (semana_a_cargar == semana_hoy)
            {
                ultimoDiaSemana = DateTime.Today;
            }
            else
            {
                if (primerDiaSemana > DateTime.Today)
                {
                    puede = false;
                }
                else
                {
                    puede = true;
                }

                if (DateTime.Today <= AntaresHelper.UltimoDiaSemana(DateTime.Today))
                {
                    puede = true;

                }
                else
                {
                    puede = false;
                }


            }
            return puede;

            //ultimoDiaReporte = AntaresHelper.UltimoDiaSemanaSiguiente(fechaFin);
            //DateTime doce = DateTime.Today.AddHours(12);
            //DateTime ahora = DateTime.Now;

            //if (ultimoDiaReporte <= DateTime.Today || DateTime.Now <= DateTime.Today.AddHours(12))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public static bool EsCorrecta_Fecha_a_Cargar(DateTime fecha)
        {
            bool correcto = false;
            
            DateTime primerDia_fecha_a_Cargar = AntaresHelper.PrimerDiaSemana(fecha);
            DateTime fechainiciosemana_pasada = AntaresHelper.PrimerDiaSemana(DateTime.Today).AddDays(-7);
            if (fechainiciosemana_pasada > primerDia_fecha_a_Cargar)
            {
                correcto = false;
                //cvHorasPersonalDia.ErrorMessage = "Solo puede cargar horas en la semana anterior on en la semana actual hasta a el dia de hoy";
            }
            else
            {
                if (fecha >= fechainiciosemana_pasada && fecha < AntaresHelper.PrimerDiaSemana(DateTime.Today))
                {
                    //aca estaria cargando en la semana pasada
                    correcto = true;
                }
                
                if (fecha >= AntaresHelper.PrimerDiaSemana(DateTime.Today) && fecha <= DateTime.Today)
                {
                    //aca estaria cargando en la semana actual
                    correcto = true;
                }
                
            }
            return correcto;
        }

        
       
  
    }

    public class AccionValores
    {
        public string Perfil;
        public string Valor;


    }
}
