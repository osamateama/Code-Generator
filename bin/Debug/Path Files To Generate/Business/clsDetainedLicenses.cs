using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsDetainedLicenses
   {
       public int DetainID { get; set; }
       public int LicenseID { get; set; }
       public DateTime DetainDate { get; set; }
       public decimal FineFees { get; set; }
       public int CreatedByUserID { get; set; }
       public bool IsReleased { get; set; }
       public DateTime? ReleaseDate { get; set; }
       public int? ReleasedByUserID { get; set; }
       public int? ReleaseApplicationID { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsDetainedLicenses() { Mode = enMode.AddNew; }

       private clsDetainedLicenses(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
       {
           this.DetainID = DetainID;
           this.LicenseID = LicenseID;
           this.DetainDate = DetainDate;
           this.FineFees = FineFees;
           this.CreatedByUserID = CreatedByUserID;
           this.IsReleased = IsReleased;
           this.ReleaseDate = ReleaseDate;
           this.ReleasedByUserID = ReleasedByUserID;
           this.ReleaseApplicationID = ReleaseApplicationID;
           Mode = enMode.Update;
       }

       public static clsDetainedLicenses Find(int id)
       {
           DataRow row = clsDetainedLicensesDAL.GetByID(id);
           if (row == null) return null;

           return new clsDetainedLicenses(row["DetainID"] != DBNull.Value ? (int)row["DetainID"] : default, row["LicenseID"] != DBNull.Value ? (int)row["LicenseID"] : default, row["DetainDate"] != DBNull.Value ? (DateTime)row["DetainDate"] : default, row["FineFees"] != DBNull.Value ? (decimal)row["FineFees"] : default, row["CreatedByUserID"] != DBNull.Value ? (int)row["CreatedByUserID"] : default, row["IsReleased"] != DBNull.Value ? (bool)row["IsReleased"] : default, row["ReleaseDate"] != DBNull.Value ? (DateTime?)row["ReleaseDate"] : default, row["ReleasedByUserID"] != DBNull.Value ? (int?)row["ReleasedByUserID"] : default, row["ReleaseApplicationID"] != DBNull.Value ? (int?)row["ReleaseApplicationID"] : default);
       }

       private bool _AddNew()
       {
           this.DetainID = clsDetainedLicensesDAL.Insert(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
           return (this.DetainID != -1);
       }

       private bool _Update()
       {
           return clsDetainedLicensesDAL.Update(this.DetainID, this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
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
           return clsDetainedLicensesDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsDetainedLicensesDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsDetainedLicensesDAL.GetByID(id) != null;
       }
   }
}
