using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsLicenses
   {
       public int LicenseID { get; set; }
       public int ApplicationID { get; set; }
       public int DriverID { get; set; }
       public int LicenseClass { get; set; }
       public DateTime IssueDate { get; set; }
       public DateTime ExpirationDate { get; set; }
       public string Notes { get; set; }
       public decimal PaidFees { get; set; }
       public bool IsActive { get; set; }
       public int IssueReason { get; set; }
       public int CreatedByUserID { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsLicenses() { Mode = enMode.AddNew; }

       private clsLicenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
       {
           this.LicenseID = LicenseID;
           this.ApplicationID = ApplicationID;
           this.DriverID = DriverID;
           this.LicenseClass = LicenseClass;
           this.IssueDate = IssueDate;
           this.ExpirationDate = ExpirationDate;
           this.Notes = Notes;
           this.PaidFees = PaidFees;
           this.IsActive = IsActive;
           this.IssueReason = IssueReason;
           this.CreatedByUserID = CreatedByUserID;
           Mode = enMode.Update;
       }

       public static clsLicenses Find(int id)
       {
           DataRow row = clsLicensesDAL.GetByID(id);
           if (row == null) return null;

           return new clsLicenses(row["LicenseID"] != DBNull.Value ? (int)row["LicenseID"] : default, row["ApplicationID"] != DBNull.Value ? (int)row["ApplicationID"] : default, row["DriverID"] != DBNull.Value ? (int)row["DriverID"] : default, row["LicenseClass"] != DBNull.Value ? (int)row["LicenseClass"] : default, row["IssueDate"] != DBNull.Value ? (DateTime)row["IssueDate"] : default, row["ExpirationDate"] != DBNull.Value ? (DateTime)row["ExpirationDate"] : default, row["Notes"] != DBNull.Value ? (string)row["Notes"] : default, row["PaidFees"] != DBNull.Value ? (decimal)row["PaidFees"] : default, row["IsActive"] != DBNull.Value ? (bool)row["IsActive"] : default, row["IssueReason"] != DBNull.Value ? (int)row["IssueReason"] : default, row["CreatedByUserID"] != DBNull.Value ? (int)row["CreatedByUserID"] : default);
       }

       private bool _AddNew()
       {
           this.LicenseID = clsLicensesDAL.Insert(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
           return (this.LicenseID != -1);
       }

       private bool _Update()
       {
           return clsLicensesDAL.Update(this.LicenseID, this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
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
           return clsLicensesDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsLicensesDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsLicensesDAL.GetByID(id) != null;
       }
   }
}
