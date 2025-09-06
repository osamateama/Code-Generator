using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsUsersDAL
   {
       public static int Insert(int UserID, int PersonID, string UserName, string Password, bool IsActive)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO Users (UserID, PersonID, UserName, Password, IsActive) VALUES (@UserID, @PersonID, @UserName, @Password, @IsActive); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@UserID", (object)UserID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@UserName", (object)UserName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Password", (object)Password ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int UserID, int PersonID, string UserName, string Password, bool IsActive)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE Users SET UserID = @UserID, PersonID = @PersonID, UserName = @UserName, Password = @Password, IsActive = @IsActive WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@UserID", (object)UserID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@UserName", (object)UserName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Password", (object)Password ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@IsActive", (object)IsActive ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM Users WHERE Id = @Id";
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
               string query = "SELECT * FROM Users WHERE Id = @Id";
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
               string query = "SELECT * FROM Users";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
