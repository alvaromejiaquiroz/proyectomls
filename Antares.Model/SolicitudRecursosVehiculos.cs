using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudRecursosVehiculos : ActiveRecordBase<SolicitudRecursosVehiculos>
    {
        public static DbDataReader GetReader(int idSolicitud)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudRecursosEmpleados));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select 
                            srv.id,
                            srv.Id_Solicitud as Solicitud,
                            Marca + ',' + Modelo + ' ' + Patente as Vehiculo
                            from  WebAntares.dbo.Solicitud_Recursos_Vehiculos srv
                            join  WebAntares.dbo.Solicitud s on  srv.Id_Solicitud = s.Id_Solicitud
                            join  Vehiculos v on srv.Id_Vehiculo = v.ID_Vehiculos 
                            ";
            sSQL += " where srv.id_solicitud = " + idSolicitud.ToString();
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }
        public static Boolean ExisteVehiculoEnSolicitud(int idSolicitud, int idVehiculo)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudRecursosVehiculos));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select distinct p.ID_Vehiculos ID_Vehiculo
                            from WebAntares.dbo.Solicitud_Recursos_Vehiculos srv
                            join WebAntares.dbo.Solicitud s on  srv.Id_Solicitud = s.Id_Solicitud
                            join WebAntares.dbo.Vehiculos p on srv.Id_Vehiculo = p.ID_Vehiculos ";
            sSQL += " where s.id_solicitud = " + idSolicitud.ToString() + " and p.id_vehiculos = " + idVehiculo.ToString();
            oConn.CommandText = sSQL;
            DbDataReader dr = oConn.ExecuteReader();
            bool retorno = false;

            while (dr.Read())
            {
                
                if ((dr.HasRows) && (int.Parse(dr["ID_Vehiculo"].ToString()) == idVehiculo))
                {
                    retorno = true;
                }
            }
            return retorno;





        }
        public static DbDataReader GetVehiculosKm_Detalle_EnSolicitud(int IdSolicitud, int IdVehiculoRecurso)
        {
            string sSql = @"exec dbo.Prod_GetVehiculosKm_Detalle_EnSolicitud @idSolicitud = " + IdSolicitud.ToString() + ", @idVehiculo = " + IdVehiculoRecurso.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }
        public static DbDataReader GetVehiculosKmEnSolicitud(int IdSolicitud)
        {
            string sSql = @"exec dbo.Prod_GetVehiculosKmEnSolicitud @idSolicitud = " + IdSolicitud.ToString() ;
            return CommonFunctions.ExecuteDbReader(sSql);
        }
        public static DbDataReader GetVehiculosEnSolicitud(int IdSolicitud)
        {
            string sSql = @"exec dbo.Prod_GetVehiculosEnSolicitud @idSolicitud = " + IdSolicitud.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }
    }
    
}
