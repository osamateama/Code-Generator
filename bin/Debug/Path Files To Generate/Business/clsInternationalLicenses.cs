using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsInternationalLicenses
   {
       public int InternationalLicenseID { get; set; }
       public int ApplicationID { get; set; }
       public int DriverID { get; set; }
       public int IssuedUsingLocalLicenseID { get; set; }
       public DateTime IssueDate { get; set; }
       public DateTime ExpirationDate { get; set; }
       public bool IsActive { get; set; }
       public int CreatedByUserID { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsInternationalLicenses() { Mode = enMode.AddNew; }

       private clsInternationalLicenses(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
       {
           this.InternationalLicenseID = InternationalLicenseID;
           this.ApplicationID = ApplicationID;
           this.DriverID = DriverID;
           this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
           this.IssueDate = IssueDate;
           this.ExpirationDate = ExpirationDate;
           this.IsActive = IsActive;
           this.CreatedByUserID = CreatedByUserID;
           Mode = enMode.Update;
       }

       public static clsInternationalLicenses Find(int id)
       {
           DataRow row = clsInternationalLicensesDAL.GetByID(id);
           if (row == null) return null;

           return new clsInternationalLicenses(row["InternationalLicenseID"] != DBNull.Value ? (int)row["InternationalLicenseID"] : default, row["ApplicationID"] != DBNull.Value ? (int)row["ApplicationID"] : default, row["DriverID"] != DBNull.Value ? (int)row["DriverID"] : default, row["IssuedUsingLocalLicenseID"] != DBNull.Value ? (int)row["IssuedUsingLocalLicenseID"] : default, row["IssueDate"] != DBNull.Value ? (DateTime)row["IssueDate"] : default, row["ExpirationDate"] != DBNull.Value ? (DateTime)row["ExpirationDate"] : default, row["IsActive"] != DBNull.Value ? (bool)row["IsActive"] : default, row["CreatedByUserID"] != DBNull.Value ? (int)row["CreatedByUserID"] : default);
       }

       private bool _AddNew()
       {
           this.InternationalLicenseID = clsInternationalLicensesDAL.Insert(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
           return (this.InternationalLicenseID != -1);
       }

       private bool _Update()
       {
           return clsInternationalLicensesDAL.Update(this.InternationalLicenseID, this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
       }

       public bool Save()
       {
           switch (Mode)
           {
               case enMode.AddNew:
                   if (_AddNew()) { Mode = enMode.Update; return true; }
                   else return false;
               case enMode.Update:
                   return _Update();
           }
           return false;
       }

       public static bool Delete(int id)
       {
           return clsInternationalLicensesDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsInternationalLicensesDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsInternationalLicensesDAL.GetByID(id) != null;
       }
   }
}
