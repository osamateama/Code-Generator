using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsTestAppointmentsDAL
   {
       public static int Insert(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO TestAppointments (TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked) VALUES (@TestAppointmentID, @TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@TestAppointmentID", (object)TestAppointmentID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeID", (object)TestTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", (object)LocalDrivingLicenseApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@AppointmentDate", (object)AppointmentDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PaidFees", (object)PaidFees ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsLocked", (object)IsLocked ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE TestAppointments SET TestAppointmentID = @TestAppointmentID, TestTypeID = @TestTypeID, LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, AppointmentDate = @AppointmentDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID, IsLocked = @IsLocked WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@TestAppointmentID", (object)TestAppointmentID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestTypeID", (object)TestTypeID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", (object)LocalDrivingLicenseApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@AppointmentDate", (object)AppointmentDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PaidFees", (object)PaidFees ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsLocked", (object)IsLocked ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM TestAppointments WHERE Id = @Id";
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
               string query = "SELECT * FROM TestAppointments WHERE Id = @Id";
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
               string query = "SELECT * FROM TestAppointments";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
