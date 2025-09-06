using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsApplicationsDAL
   {
       public static int Insert(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO Applications (ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID) VALUES (@ApplicationID, @ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicantPersonID", (object)ApplicantPersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationDate", (object)ApplicationDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationTypeID", (object)ApplicationTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationStatus", (object)ApplicationStatus ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LastStatusDate", (object)LastStatusDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PaidFees", (object)PaidFees ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE Applications SET ApplicationID = @ApplicationID, ApplicantPersonID = @ApplicantPersonID, ApplicationDate = @ApplicationDate, ApplicationTypeID = @ApplicationTypeID, ApplicationStatus = @ApplicationStatus, LastStatusDate = @LastStatusDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicantPersonID", (object)ApplicantPersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationDate", (object)ApplicationDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationTypeID", (object)ApplicationTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationStatus", (object)ApplicationStatus ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LastStatusDate", (object)LastStatusDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PaidFees", (object)PaidFees ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM Applications WHERE Id = @Id";
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
               string query = "SELECT * FROM Applications WHERE Id = @Id";
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
               string query = "SELECT * FROM Applications";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
