using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class Facturas : ActiveRecordBase<Facturas>
    {

        public static DbDataReader ListadoFacturas()
        {
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Facturas));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = "exec PROC_ADM_Get_Facturas";

            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();

        }
        public static Facturas GetById(string Id)
        {
            try
            {
                return Facturas.FindFirst(Expression.Eq("IdFacturas", int.Parse(Id)));
            }
            catch (Exception e)
            {
                return new Facturas();
            }
        }

    }
}
