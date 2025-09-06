using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsInternationalLicensesDAL
   {
       public static int Insert(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO InternationalLicenses (InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID) VALUES (@InternationalLicenseID, @ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@InternationalLicenseID", (object)InternationalLicenseID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", (object)IssuedUsingLocalLicenseID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssueDate", (object)IssueDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ExpirationDate", (object)ExpirationDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE InternationalLicenses SET InternationalLicenseID = @InternationalLicenseID, ApplicationID = @ApplicationID, DriverID = @DriverID, IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, IsActive = @IsActive, CreatedByUserID = @CreatedByUserID WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@InternationalLicenseID", (object)InternationalLicenseID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ApplicationID", (object)ApplicationID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DriverID", (object)DriverID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", (object)IssuedUsingLocalLicenseID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IssueDate", (object)IssueDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ExpirationDate", (object)ExpirationDate ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM InternationalLicenses WHERE Id = @Id";
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
               string query = "SELECT * FROM InternationalLicenses WHERE Id = @Id";
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
               string query = "SELECT * FROM InternationalLicenses";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
