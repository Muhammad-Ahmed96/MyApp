using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace PMS.DAL
{
    internal class DBHelper : IDisposable
    {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
        SqlConnection con = null;
        public DBHelper()
        {
            con = new SqlConnection(connString);
            con.Open();
        }

        public int ExecuteQuery(string query, ArrayList list)
        {
            SqlCommand command = new SqlCommand(query, con);
            command.CommandType = CommandType.StoredProcedure;
            for(int i=0;i<list.Count;i++)
            {
                string par = list[i++].ToString();
                string val=null;
                if (list[i] != null)
                {
                    val = list[i].ToString();
                }
                if(val!=null)
                    command.Parameters.AddWithValue(par,val.ToString());
                else
                    command.Parameters.AddWithValue(par, DBNull.Value);
            }
            var count = command.ExecuteNonQuery();
            return count;
        }

        public Object ExecuteScalar(string query)
        {
            SqlCommand command = new SqlCommand(query, con);
            return command.ExecuteScalar();
        }

        public SqlDataReader ExecuteReader(string query,ArrayList list)
        {
            SqlCommand command = new SqlCommand(query, con);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < list.Count;i++)
            {
                string par = list[i++].ToString();
                string val = null;
                if (list[i] != null)
                {
                    val = list[i].ToString();
                }
                if (val != null)
                    command.Parameters.AddWithValue(par, val.ToString());
                else
                    command.Parameters.AddWithValue(par, DBNull.Value);
            }
                return command.ExecuteReader();
        }


        public void Dispose()
        {
            if (con != null & con.State == System.Data.ConnectionState.Open)
                con.Close();
        }
    }
}
