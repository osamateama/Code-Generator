using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clssysdiagramsDAL
   {
       public static int Insert(string name, int principal_id, int diagram_id, int? version, byte[] definition)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO sysdiagrams (name, principal_id, diagram_id, version, definition) VALUES (@name, @principal_id, @diagram_id, @version, @definition); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@name", (object)name ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@principal_id", (object)principal_id ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@diagram_id", (object)diagram_id ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@version", (object)version ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@definition", (object)definition ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, string name, int principal_id, int diagram_id, int? version, byte[] definition)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE sysdiagrams SET name = @name, principal_id = @principal_id, diagram_id = @diagram_id, version = @version, definition = @definition WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@name", (object)name ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@principal_id", (object)principal_id ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@diagram_id", (object)diagram_id ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@version", (object)version ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@definition", (object)definition ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM sysdiagrams WHERE Id = @Id";
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
               string query = "SELECT * FROM sysdiagrams WHERE Id = @Id";
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
               string query = "SELECT * FROM sysdiagrams";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
