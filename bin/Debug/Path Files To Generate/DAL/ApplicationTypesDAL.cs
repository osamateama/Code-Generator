using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsApplicationTypesDAL
   {
       public static int Insert(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO ApplicationTypes (ApplicationTypeID, ApplicationTypeTitle, ApplicationFees) VALUES (@ApplicationTypeID, @ApplicationTypeTitle, @ApplicationFees); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@ApplicationTypeID", (object)ApplicationTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationTypeTitle", (object)ApplicationTypeTitle ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationFees", (object)ApplicationFees ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE ApplicationTypes SET ApplicationTypeID = @ApplicationTypeID, ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@ApplicationTypeID", (object)ApplicationTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationTypeTitle", (object)ApplicationTypeTitle ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationFees", (object)ApplicationFees ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM ApplicationTypes WHERE Id = @Id";
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
               string query = "SELECT * FROM ApplicationTypes WHERE Id = @Id";
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
               string query = "SELECT * FROM ApplicationTypes";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
