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
   public partial class SolicitudGastos : ActiveRecordBase<SolicitudGastos> {

        public static  DbDataReader GetGastosSolicitud(int IdSolicitud){


            string sSql = @" select  sg.Id ,tg.descripcion as Tipo ,Detalle ,Total from dbo.Solicitud_Gastos sg 
            join WebAntares.dbo.Tipo_Gasto tg on sg.IdTipoGasto = tg.id
            where sg.IdSolicitud = " + IdSolicitud.ToString() + " Order by sg.id ";

            return CommonFunctions.ExecuteDbReader(sSql);

        }

    }
}
