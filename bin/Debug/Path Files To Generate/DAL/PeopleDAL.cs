using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsPeopleDAL
   {
       public static int Insert(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO People (PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath) VALUES (@PersonID, @NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@NationalNo", (object)NationalNo ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@FirstName", (object)FirstName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@SecondName", (object)SecondName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ThirdName", (object)ThirdName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LastName", (object)LastName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DateOfBirth", (object)DateOfBirth ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Gender", (object)Gender ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Address", (object)Address ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Phone", (object)Phone ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@NationalityCountryID", (object)NationalityCountryID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE People SET PersonID = @PersonID, NationalNo = @NationalNo, FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, Address = @Address, Phone = @Phone, Email = @Email, NationalityCountryID = @NationalityCountryID, ImagePath = @ImagePath WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@NationalNo", (object)NationalNo ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@FirstName", (object)FirstName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@SecondName", (object)SecondName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ThirdName", (object)ThirdName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@LastName", (object)LastName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DateOfBirth", (object)DateOfBirth ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Gender", (object)Gender ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Address", (object)Address ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Phone", (object)Phone ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@NationalityCountryID", (object)NationalityCountryID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM People WHERE Id = @Id";
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
               string query = "SELECT * FROM People WHERE Id = @Id";
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
               string query = "SELECT * FROM People";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
