using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsApplications
   {
       public int ApplicationID { get; set; }
       public int ApplicantPersonID { get; set; }
       public DateTime ApplicationDate { get; set; }
       public int ApplicationTypeID { get; set; }
       public int ApplicationStatus { get; set; }
       public DateTime LastStatusDate { get; set; }
       public decimal PaidFees { get; set; }
       public int CreatedByUserID { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsApplications() { Mode = enMode.AddNew; }

       private clsApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
       {
           this.ApplicationID = ApplicationID;
           this.ApplicantPersonID = ApplicantPersonID;
           this.ApplicationDate = ApplicationDate;
           this.ApplicationTypeID = ApplicationTypeID;
           this.ApplicationStatus = ApplicationStatus;
           this.LastStatusDate = LastStatusDate;
           this.PaidFees = PaidFees;
           this.CreatedByUserID = CreatedByUserID;
           Mode = enMode.Update;
       }

       public static clsApplications Find(int id)
       {
           DataRow row = clsApplicationsDAL.GetByID(id);
           if (row == null) return null;

           return new clsApplications(row["ApplicationID"] != DBNull.Value ? (int)row["ApplicationID"] : default, row["ApplicantPersonID"] != DBNull.Value ? (int)row["ApplicantPersonID"] : default, row["ApplicationDate"] != DBNull.Value ? (DateTime)row["ApplicationDate"] : default, row["ApplicationTypeID"] != DBNull.Value ? (int)row["ApplicationTypeID"] : default, row["ApplicationStatus"] != DBNull.Value ? (int)row["ApplicationStatus"] : default, row["LastStatusDate"] != DBNull.Value ? (DateTime)row["LastStatusDate"] : default, row["PaidFees"] != DBNull.Value ? (decimal)row["PaidFees"] : default, row["CreatedByUserID"] != DBNull.Value ? (int)row["CreatedByUserID"] : default);
       }

       private bool _AddNew()
       {
           this.ApplicationID = clsApplicationsDAL.Insert(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
           return (this.ApplicationID != -1);
       }

       private bool _Update()
       {
           return clsApplicationsDAL.Update(this.ApplicationID, this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
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
           return clsApplicationsDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsApplicationsDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsApplicationsDAL.GetByID(id) != null;
       }
   }
}
