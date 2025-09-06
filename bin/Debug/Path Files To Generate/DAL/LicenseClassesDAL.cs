using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsLicenseClassesDAL
   {
       public static int Insert(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "INSERT INTO LicenseClasses (LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees) VALUES (@LicenseClassID, @ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees); SELECT SCOPE_IDENTITY();";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@LicenseClassID", (object)LicenseClassID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ClassName", (object)ClassName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ClassDescription", (object)ClassDescription ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@MinimumAllowedAge", (object)MinimumAllowedAge ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DefaultValidityLength", (object)DefaultValidityLength ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ClassFees", (object)ClassFees ?? DBNull.Value);

               conn.Open();
               return Convert.ToInt32(cmd.ExecuteScalar());
           }
       }

       public static bool Update(int id, int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "UPDATE LicenseClasses SET LicenseClassID = @LicenseClassID, ClassName = @ClassName, ClassDescription = @ClassDescription, MinimumAllowedAge = @MinimumAllowedAge, DefaultValidityLength = @DefaultValidityLength, ClassFees = @ClassFees WHERE Id = @Id";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@Id", id);
               cmd.Parameters.AddWithValue("@LicenseClassID", (object)LicenseClassID ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ClassName", (object)ClassName ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ClassDescription", (object)ClassDescription ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@MinimumAllowedAge", (object)MinimumAllowedAge ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@DefaultValidityLength", (object)DefaultValidityLength ?? DBNull.Value);
               cmd.Parameters.AddWithValue("@ClassFees", (object)ClassFees ?? DBNull.Value);

               conn.Open();
               return cmd.ExecuteNonQuery() > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
           {
               string query = "DELETE FROM LicenseClasses WHERE Id = @Id";
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
               string query = "SELECT * FROM LicenseClasses WHERE Id = @Id";
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
               string query = "SELECT * FROM LicenseClasses";
               SqlDataAdapter da = new SqlDataAdapter(query, conn);
               DataTable dt = new DataTable();
               da.Fill(dt);
               return dt;
           }
       }
   }
}
