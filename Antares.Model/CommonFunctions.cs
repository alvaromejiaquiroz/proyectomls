﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using NHibernate;
using Castle.ActiveRecord;

namespace Antares.model
{
    class CommonFunctions
    {

        public static DbDataReader ExecuteDbReader(string SSQLQuery)
        {
            // Expects a root type
            //ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(Solicitud));
            ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(CommonFunctions));
            DbConnection db = (DbConnection)sess.Connection;// ActiveRecordMediator.GetSessionFactoryHolder().GetSessionFactory().GetCurrentSession().Connection;
            DbCommand oConn = db.CreateCommand();
            oConn.CommandText = SSQLQuery;
            DbDataReader dr = null ;
            return  oConn.ExecuteReader();
            
            

        }
    }
}
