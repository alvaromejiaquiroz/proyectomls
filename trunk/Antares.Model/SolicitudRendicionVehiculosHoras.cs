using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;


namespace Antares.model
{
    public partial class SolicitudRendicionVehiculosHoras : ActiveRecordBase<SolicitudRendicionVehiculosHoras>
    {
        public static  DbDataReader GetReader(int IdSolicitud){


            string sSql = @" select 
                             t.id
                            ,t.id_solicitud
                            ,v.Patente  + ' ' + v.Marca  + ' ' + v.Modelo as Vehiculo
                            ,KM_Recorridos as KmRecorridos
                            ,Duracion	
                            from  dbo.Solicitud_Reporte_Traslados t
                            join vehiculos v on t.Id_Vehiculos = v.Id_Vehiculos
                            where t.id_solicitud = " + IdSolicitud.ToString() ;
                            

            return CommonFunctions.ExecuteDbReader(sSql);

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
        public static void GeneraRendicionVehiculos_Solicitud(int IdSolicitud)
        {
            string sSql = @"exec dbo.GeneraRendicion_Solicitud @idSolicitud = " + IdSolicitud.ToString();
            CommonFunctions.ExecuteDbReader(sSql);

        }
        
    }
}
