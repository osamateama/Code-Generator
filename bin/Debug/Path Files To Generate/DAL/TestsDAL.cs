using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsTestsDAL
   {
       public static int Insert(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO Tests (TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID) VALUES (@TestID, @TestAppointmentID, @TestResult, @Notes, @CreatedByUserID); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@TestID", (object)TestID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestAppointmentID", (object)TestAppointmentID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestResult", (object)TestResult ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE Tests SET TestID = @TestID, TestAppointmentID = @TestAppointmentID, TestResult = @TestResult, Notes = @Notes, CreatedByUserID = @CreatedByUserID WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@TestID", (object)TestID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestAppointmentID", (object)TestAppointmentID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@TestResult", (object)TestResult ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM Tests WHERE Id = @Id";
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
               string query = "SELECT * FROM Tests WHERE Id = @Id";
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
               string query = "SELECT * FROM Tests";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
