using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsTestTypesDAL
   {
       public static int Insert(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO TestTypes (TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees) VALUES (@TestTypeID, @TestTypeTitle, @TestTypeDescription, @TestTypeFees); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@TestTypeID", (object)TestTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeTitle", (object)TestTypeTitle ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeDescription", (object)TestTypeDescription ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeFees", (object)TestTypeFees ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE TestTypes SET TestTypeID = @TestTypeID, TestTypeTitle = @TestTypeTitle, TestTypeDescription = @TestTypeDescription, TestTypeFees = @TestTypeFees WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@TestTypeID", (object)TestTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeTitle", (object)TestTypeTitle ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeDescription", (object)TestTypeDescription ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeFees", (object)TestTypeFees ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM TestTypes WHERE Id = @Id";
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
               string query = "SELECT * FROM TestTypes WHERE Id = @Id";
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
               string query = "SELECT * FROM TestTypes";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
