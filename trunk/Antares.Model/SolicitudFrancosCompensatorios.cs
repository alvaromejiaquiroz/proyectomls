using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class SolicitudFrancosCompensatorios : ActiveRecordBase<SolicitudFrancosCompensatorios>
    {

        public static DbDataReader GetReader(string IdUsuarioCreador)
        {
            // Expects a root type
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(SolicitudFrancosCompensatorios));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = @"
                            select 
                             s.iDSolicitud
                            ,c.FechaInicio
                            ,c.FechaFin
                            ,c.Descripcion
                            ,c.Duracion
                            ,c.ConElConsentimiento ConElConsentimiento
                            from WebAntares.dbo.Solicitud s
                            join Solicitud_Francos_Compensatorios c on s.Id_Solicitud = c.idsolicitud";
            sSQL = sSQL + " where s.IdUsuarioCreador = " + IdUsuarioCreador;
            sSQL = sSQL + "  order by fechaInicio desc";

            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();
        }
    }
}
