using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsLocalDrivingLicenseApplicationsDAL
   {
       public static int Insert(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO LocalDrivingLicenseApplications (LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID) VALUES (@LocalDrivingLicenseApplicationID, @ApplicationID, @LicenseClassID); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", (object)LocalDrivingLicenseApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LicenseClassID", (object)LicenseClassID ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE LocalDrivingLicenseApplications SET LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", (object)LocalDrivingLicenseApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LicenseClassID", (object)LicenseClassID ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM LocalDrivingLicenseApplications WHERE Id = @Id";
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
               string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE Id = @Id";
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
               string query = "SELECT * FROM LocalDrivingLicenseApplications";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
