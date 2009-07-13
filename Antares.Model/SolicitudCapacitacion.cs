using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudCapacitacion : ActiveRecordBase<SolicitudCapacitacion>
    {
        public static DbDataReader GetReader()
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudCapacitacion));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"
                            select 
                            s.ID_Solicitud
                            ,c.FechaInicio
                            ,c.FechaFin
                            ,c.Descripcion
                            ,c.Nivel
                            ,c.Duracion
                            ,c.Area_Estudio
                            ,c.Instructor
                            from WebAntares.dbo.Solicitud s
                            join Solicitud_capacitacion c on s.Id_Solicitud = c.idsolicitud
                            order by fechaInicio desc
                            ";
            
            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }
    }
}
