using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsLicensesDAL
   {
       public static int Insert(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO Licenses (LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID) VALUES (@LicenseID, @ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@LicenseID", (object)LicenseID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LicenseClass", (object)LicenseClass ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssueDate", (object)IssueDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ExpirationDate", (object)ExpirationDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PaidFees", (object)PaidFees ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssueReason", (object)IssueReason ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE Licenses SET LicenseID = @LicenseID, ApplicationID = @ApplicationID, DriverID = @DriverID, LicenseClass = @LicenseClass, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, Notes = @Notes, PaidFees = @PaidFees, IsActive = @IsActive, IssueReason = @IssueReason, CreatedByUserID = @CreatedByUserID WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@LicenseID", (object)LicenseID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LicenseClass", (object)LicenseClass ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssueDate", (object)IssueDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ExpirationDate", (object)ExpirationDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PaidFees", (object)PaidFees ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssueReason", (object)IssueReason ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM Licenses WHERE Id = @Id";
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
               string query = "SELECT * FROM Licenses WHERE Id = @Id";
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
               string query = "SELECT * FROM Licenses";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
