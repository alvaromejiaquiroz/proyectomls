using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class TipoSolicitud : ActiveRecordBase<TipoSolicitud>
    {

        #region TiposDeSolicitud
        public static int M_Preventivo { get { return 1; } }
        public static int M_Correctivo { get { return 2; } }
        #endregion


        /* public static Boolean Existe_Tarea_en_Solicitud(int idSolicitud , string IdTarea , string idSitio)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(TipoSolicitud));
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
                        

        }
       * */

        public static DbDataReader TiposAutorizadosXPerfil(int idPerfil)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(TipoSolicitud));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select  t.Id_TipoSolicitud ,  t. Descripcion
                            from  Usuarios_TipoSolicitud a
                            join  Tipo_Solicitud t	on a.Id_TipoSolicitud = t.Id_TipoSolicitud
                            join  usuarios  u		on a.idUsuario = u.IdUsuario ";

            sSQL += " where u.idPerfil = " + idPerfil.ToString();
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();



        }



    }
}
