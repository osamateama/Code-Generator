using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsCountriesDAL
   {
       public static int Insert(int CountryID, string CountryName)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO Countries (CountryID, CountryName) VALUES (@CountryID, @CountryName); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@CountryID", (object)CountryID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CountryName", (object)CountryName ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int CountryID, string CountryName)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE Countries SET CountryID = @CountryID, CountryName = @CountryName WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@CountryID", (object)CountryID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CountryName", (object)CountryName ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM Countries WHERE Id = @Id";
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
               string query = "SELECT * FROM Countries WHERE Id = @Id";
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
               string query = "SELECT * FROM Countries";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
