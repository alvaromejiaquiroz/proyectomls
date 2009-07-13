using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using NHibernate;
using Castle.ActiveRecord;

namespace Antares.model
{
    public partial class SolicitudServiciosAfectados : ActiveRecordBase<SolicitudServiciosAfectados>
    {
        public static DbDataReader GetServiciosAfectados(int idSolicitud){

            string sSql;
            sSql = @"SELECT sa.[Id]
                          ,[IdServicioAfectado]
	                      ,s.Descripcion
                      FROM [webAntares].[dbo].[Solicitud_Servicios_Afectados] sa 
                    inner join  dbo.Servicios S on sa.idServicioAfectado = s.id
                    where IdSolicitud = " + idSolicitud;

            sSql = "exec dbo.Proc_GetServiciosAfectados @idSolicitud = " + idSolicitud.ToString();
            return ExecuteDbReader(sSql);

        }
        private static DbDataReader ExecuteDbReader(string SSQLQuery)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            oConn.CommandText = SSQLQuery;
            return oConn.ExecuteReader();
        }
    }

}
