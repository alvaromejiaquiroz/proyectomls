using System;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Expression;

namespace WebAntares
{
    public static class CustomDAL
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["WebAntares"].ConnectionString;
        }

        public static SqlConnection Get_Connection()
        {
            
            string cstring = GetConnectionString();
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = cstring;
            return conn;

        }
        public static SqlDataReader ExecQuery()
        {
            SqlConnection conn = Get_Connection();
            SqlCommand cmd = new SqlCommand("select apellido, nombres, zona, base  from personal where nombres like '%daniel%'", conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
//            conn.Close();
            return rd;


        }


       
    }

}