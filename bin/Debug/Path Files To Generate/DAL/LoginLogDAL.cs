using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsLoginLogDAL
   {
       public static int Insert(int LogID, string UserName, string Password, DateTime? LoginDate)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO LoginLog (LogID, UserName, Password, LoginDate) VALUES (@LogID, @UserName, @Password, @LoginDate); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@LogID", (object)LogID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@UserName", (object)UserName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Password", (object)Password ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LoginDate", (object)LoginDate ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int LogID, string UserName, string Password, DateTime? LoginDate)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE LoginLog SET LogID = @LogID, UserName = @UserName, Password = @Password, LoginDate = @LoginDate WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@LogID", (object)LogID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@UserName", (object)UserName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Password", (object)Password ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LoginDate", (object)LoginDate ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM LoginLog WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static DataRow GetByID(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "SELECT * FROM LoginLog WHERE Id = @Id";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               da.SelectCommand.Parameters.AddWithValue("@Id", id);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt.Rows.Count > 0 ? dt.Rows[0] : null;
           }
       }

       public static DataTable GetAll()
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "SELECT * FROM LoginLog";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
