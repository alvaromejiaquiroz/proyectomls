using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudRecursosEmpleados : ActiveRecordBase<SolicitudRecursosEmpleados>
    {
        public static DbDataReader GetReader(int idSolicitud)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudRecursosEmpleados));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select 
                            sre.Id
                            ,Apellido + ',' + Nombres  as Empleado
                            --,case when Responsable  = 1 then 'R' else 'N' end as Responsable
                            ,Responsable
                            from WebAntares.dbo.Solicitud_Recursos_Empleados sre
                            join  WebAntares.dbo.Solicitud s on  sre.Id_Solicitud = s.Id_Solicitud
                            join personal p on sre.id_empleado = p.Id_Empleados 
                            ";
            sSQL += " where sre.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }
        public static Boolean ExisteEmpleadoEnSolicitud(int idSolicitud, int idEmpleado )
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudRecursosEmpleados));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select distinct p.Id_Empleados
                            from WebAntares.dbo.Solicitud_Recursos_Empleados sre
                            join  WebAntares.dbo.Solicitud s on  sre.Id_Solicitud = s.Id_Solicitud
                            join personal p on sre.id_empleado = p.Id_Empleados 
                            ";
            sSQL += " where sre.id_solicitud = " + idSolicitud.ToString() + " and p.id_empleados = " + idEmpleado.ToString();
            oConn.CommandText = sSQL;
            DbDataReader  dr  = oConn.ExecuteReader();
            bool retorno = false;

            while (dr.Read())
	            {
	               // if (dr["Id_Empleados"] != null )
                if ((dr.HasRows) && ( int.Parse(dr["Id_Empleados"].ToString() )==  idEmpleado ))
                        {
                            retorno = true;
                        }
                   }
            return retorno;
            
          
            

            
        }
        public static void QuitaResponsableSolicitud(int idSolicitud)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudRecursosEmpleados));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"PRC_EliminaResponsablesSolicitud";
            sSQL += " @idSol = " + idSolicitud.ToString();

            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();
            
        }
        public static DbDataReader GetPersonaHoras_Detalle_EnSolicitud(int IdSolicitud, int IdPersona)
        {
            string sSql = @"exec dbo.Prod_GetPersonaHoras_Detalle_EnSolicitud @idSolicitud = " + IdSolicitud.ToString() + ", @idPersona = " + IdPersona.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }

        public static DbDataReader GetPersonasEnSolicitud(int IdSolicitud)
        {
            string sSql = @"exec dbo.Prod_GetPersonasEnSolicitud @idSolicitud = " + IdSolicitud.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }
    }
}
