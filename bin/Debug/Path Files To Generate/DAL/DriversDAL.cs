using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsDriversDAL
   {
       public static int Insert(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO Drivers (DriverID, PersonID, CreatedByUserID, CreatedDate) VALUES (@DriverID, @PersonID, @CreatedByUserID, @CreatedDate); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedDate", (object)CreatedDate ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE Drivers SET DriverID = @DriverID, PersonID = @PersonID, CreatedByUserID = @CreatedByUserID, CreatedDate = @CreatedDate WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedDate", (object)CreatedDate ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM Drivers WHERE Id = @Id";
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
               string query = "SELECT * FROM Drivers WHERE Id = @Id";
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
               string query = "SELECT * FROM Drivers";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
