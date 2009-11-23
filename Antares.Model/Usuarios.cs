using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;
using NHibernate.Expression;
using Castle.ActiveRecord;

namespace Antares.model
{
   
    public partial class Usuarios : ActiveRecordBase<Usuarios>
    {

        public static Usuarios CheckLogin(string pUser, string pPass)
        {
            return FindFirst(Expression.Eq("LoginName", pUser), Expression.Eq("Password", pPass));
        }


    }
}
