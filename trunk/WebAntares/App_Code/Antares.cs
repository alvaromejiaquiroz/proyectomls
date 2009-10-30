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
            BiFactory.Sol = Solicitud.GetById(idSol);

            string subject = string.Empty;
            string mensaje = "Se creo la Solicitud Nro." + BiFactory.Sol.Id_Solicitud.ToString() +
                "- Tipo " + BiFactory.Sol.Tipo.Descripcion + "- Responsable:" + Solicitud.GetResponsable(BiFactory.Sol.Id_Solicitud.ToString());

            WebAntares.AntaresHelper.EnviaMail(subject, mensaje);


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
    }

    public class AccionValores
    {
        public string Perfil;
        public string Valor;


    }
}
