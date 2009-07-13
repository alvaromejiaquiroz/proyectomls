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
   public partial class SolicitudRendicionPersonalHoras : ActiveRecordBase<SolicitudRendicionPersonalHoras> {
        public static  DbDataReader GetPersonaRendicion(int IdSolicitud, int IdPersonaRecurso){


            string sSql = @"select id_solicitud_rendicion_personas Id,
                             convert(varchar(10),Fecha,103) Fecha,
                            Horas,
                            Descripcion
                            from dbo.Solicitud_Rendicion_Personal_Horas
                            where id_solicitud = " + IdSolicitud.ToString() +
                            "and id_solicitud_recurso_persona = " + IdPersonaRecurso;

            return CommonFunctions.ExecuteDbReader(sSql);

        }
        public static DbDataReader GetPersonasHorasEnSolicitud(int IdSolicitud, int IdEmpleado)
        {
            string sSql = @"exec dbo.Prod_GetPersonasHorasEnSolicitud @idSolicitud = " + IdSolicitud.ToString() + " , @idEmpleado = " + IdEmpleado.ToString();
            return CommonFunctions.ExecuteDbReader(sSql);
        }
    }
}
