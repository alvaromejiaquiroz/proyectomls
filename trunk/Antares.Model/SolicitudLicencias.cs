using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudLicencias : ActiveRecordBase<SolicitudLicencias>
    {

        public static DbDataReader GetReader()
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudLicencias));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"select 
                         s.ID_Solicitud
                        ,c.FechaInicio
                        ,c.FechaFin
                        ,c.Descripcion
                        ,c.Duracion
                        ,tl.Descripcion Tipo_Licencia
                        from WebAntares.dbo.Solicitud s
                        join Solicitud_Licencias c on s.Id_Solicitud = c.idsolicitud
                        join Tipo_Licencia tl on  tl.id  = c.Id_TipoLicencia
                        order by fechaInicio desc";

            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }

        public static DbDataReader GetLicenciasEmpleado(string IdEmpleado)
        {
            string sSql = @"exec dbo.Proc_Get_Licencias_Empleado @IdEmpleado = " + IdEmpleado.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }

        
    }
}
