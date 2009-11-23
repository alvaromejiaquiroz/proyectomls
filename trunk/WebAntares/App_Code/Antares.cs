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
using Antares.model;
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

        public static void NotificaSolicitud(int idSol)
        {
            string subject = AntaresHelper.Get_Config_Ambiente();
            string mensaje = "Creacion Solicitud";
            if (idSol > 0)
            {
                BiFactory.Sol = Solicitud.GetById(idSol);

                
                 mensaje = BiFactory.User.Nombre.ToString() + "Se creo la Solicitud Nro." + BiFactory.Sol.Id_Solicitud.ToString() +
                    "- Tipo " + BiFactory.Sol.Tipo.Descripcion + "- Responsable:" + Solicitud.GetResponsable(BiFactory.Sol.Id_Solicitud.ToString());

                WebAntares.AntaresHelper.EnviaMail(subject, mensaje);
            }
            else
            {
                WebAntares.AntaresHelper.EnviaMail(subject, "Este es un Mensaje de prueba");
            }
            Loguea_Evento(mensaje);

        }

        public static void Loguea_Evento(string texto)
        {
            //Logger.Write("Testing");

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

        public static string Get_Config_HorasPersonaSemana()
        {
            
            string valor = ConfigurationSettings.AppSettings["HorasPersonaSemana"];
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
    }

    public class AccionValores
    {
        public string Perfil;
        public string Valor;


    }
}
