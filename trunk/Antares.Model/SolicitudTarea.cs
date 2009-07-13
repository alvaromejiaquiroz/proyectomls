using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudTareas : ActiveRecordBase<SolicitudTareas>
    {
        public static DbDataReader GetReader(int idSolicitud)
        {
            // Expects a root type
//            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudTareas));
//            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
//            DbCommand oConn = db.CreateCommand();
//            string sSQL = @"select st.Id
//                            , convert( varchar(10),FechaInicio,103) Inicio
//                            , convert( varchar(10),FechaFin,103) Fin
//                            , ltrim(op.Descripcion) Sitio
//                            , t.tarea
//                             from solicitud_tareas st 
//                            inner join op_sitios op on st.Id_Sitio = op.id_sitio
//                            inner join Tareas t on st.id_tarea = t.id";
//            sSQL += " where st.id_solicitud = " + idSolicitud.ToString();
//            oConn.CommandText = sSQL;
//            return oConn.ExecuteReader();

            string sSql = @"exec dbo.Prod_GetSolicitud_Tareas @idSolicitud = " + idSolicitud.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }
        public static Boolean Existe_Tarea_en_Solicitud(int idSolicitud , string IdTarea , string idSitio)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudTareas));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = "";
            return true;

            /*
                              *
@"select * from master.dbo.Solicitud_Tareas
where 
	id_solicitud = 
and id_sitio = 
and Id_Tarea
            sSQL += " where st.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
                              * 
                           * */
            

        }
        public static Boolean ExisteTareaEnSolicitud(int idSolicitud, int idTarea)
        {
            
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudTareas));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select  distinct  st.id_tarea
                            from Solicitud_Tareas st
                            join WebAntares.dbo.Solicitud s on  st.Id_Solicitud = s.Id_Solicitud
                            join  WebAntares.dbo.Tareas t on st.id_tarea  = t.id";
            sSQL += " where s.id_solicitud = " + idSolicitud.ToString(); //+ "and st.Id_Sitio = " + idSitio.ToString();
            sSQL += " and t.id = " + idTarea.ToString();
            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();
            bool retorno = false;

            while (dr.Read())
            {

                if ((dr.HasRows) && (int.Parse(dr["id_tarea"].ToString()) == idTarea))
                {
                    retorno = true;
                }
            }
            return retorno;
        }

    }
}
