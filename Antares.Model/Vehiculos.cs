using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;
using System.Data.Common;

namespace Antares.model
{
    public partial class Vehiculos : ActiveRecordBase<Vehiculos> 
    {

        public static DbDataReader FindAllMenoTalVehi(int unId)
        {
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Vehiculos));
            DbConnection db = (DbConnection)sess.Connection;
            DbCommand oConn = db.CreateCommand();
            string sSQL = "select  *  from WebAntares.dbo.Personal where id_empleados <> " + unId.ToString();

            oConn.CommandText = sSQL;
            return oConn.ExecuteReader();

        }
        public static Vehiculos GetById(string Id)
        {
            try
            {
                return Vehiculos.FindFirst(Expression.Eq("IdVehiculos", int.Parse(Id)));
            }
            catch (Exception e)
            {
                return new Vehiculos();
            }
        }

    }
}
