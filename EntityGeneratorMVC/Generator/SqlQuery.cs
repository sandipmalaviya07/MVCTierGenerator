using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EntityGeneratorMVC.Generator
{
    public static class SqlQuery
    {
        public static SqlConnection cn;
        public static SqlCommand cmd;
        public static SqlDataAdapter da;
        public static void ConnectionString(string ConnectionString)
        {
            cn = new SqlConnection(ConnectionString);
        }
        public static bool Authentication()
        {
            try
            {
                cn.Open();
                cn.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static DataTable GetDataTable(string Query)
        {
            DataTable dt = new DataTable();
            try
            {
                da = new SqlDataAdapter(Query, cn);
                cn.Open();
                da.Fill(dt);
                cn.Close();
            }
            catch
            { }
            cn.Close();
            return dt;
        }

    }
}
