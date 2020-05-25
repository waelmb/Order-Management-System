//
// Data Access Tier: uses ADO.NET to execute SQL against an underlying
// SQL Server database.
//

using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataBaseAccess
{

  public static class DataBaseAdaptor
  {
        private static string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\wafm2\\Documents\\OrderManagementSys.mdf;Integrated Security=True;Connect Timeout=30";

        public static void insertData()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(conString);
                string ins = "Insert into [Inventory](Id, Name, Price) values(3, 'testItem', 5)";
                SqlCommand command = new SqlCommand(ins, con);
                con.Open();
                command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();  // rethrow while preserving stack
                throw;  // avoid compiler warnings
            }
            finally
            {
                con.Close();
            }
        }

        public static DataSet ExecuteNonScalarQuery(string sql)
        {
            MySqlConnection db = null;

            try
            {
                db = new MySqlConnection(conString);
                db.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = db;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                cmd.CommandText = sql;
                adapter.Fill(ds);

                db.Close();

                return ds;
            }
            catch (Exception ex)
            {
                //
                // something failed, so rethrow the exception so caller knows:
                //
                ExceptionDispatchInfo.Capture(ex).Throw();  // rethrow while preserving stack
                throw;  // avoid compiler warnings
            }
            finally
            {
                //
                // close connection:
                //
                if (db != null && db.State != ConnectionState.Closed)
                    db.Close();
            }
        }
    }//class

}//namespace
