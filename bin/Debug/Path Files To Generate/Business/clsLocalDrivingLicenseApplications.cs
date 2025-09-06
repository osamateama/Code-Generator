using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsLocalDrivingLicenseApplications
   {
       public int LocalDrivingLicenseApplicationID { get; set; }
       public int ApplicationID { get; set; }
       public int LicenseClassID { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsLocalDrivingLicenseApplications() { Mode = enMode.AddNew; }

       private clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
       {
           this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
           this.ApplicationID = ApplicationID;
           this.LicenseClassID = LicenseClassID;
           Mode = enMode.Update;
       }

       public static clsLocalDrivingLicenseApplications Find(int id)
       {
           DataRow row = clsLocalDrivingLicenseApplicationsDAL.GetByID(id);
           if (row == null) return null;

           return new clsLocalDrivingLicenseApplications(row["LocalDrivingLicenseApplicationID"] != DBNull.Value ? (int)row["LocalDrivingLicenseApplicationID"] : default, row["ApplicationID"] != DBNull.Value ? (int)row["ApplicationID"] : default, row["LicenseClassID"] != DBNull.Value ? (int)row["LicenseClassID"] : default);
       }

       private bool _AddNew()
       {
           this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsDAL.Insert(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
           return (this.LocalDrivingLicenseApplicationID != -1);
       }

       private bool _Update()
       {
           return clsLocalDrivingLicenseApplicationsDAL.Update(this.LocalDrivingLicenseApplicationID, this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
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
           return clsLocalDrivingLicenseApplicationsDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsLocalDrivingLicenseApplicationsDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsLocalDrivingLicenseApplicationsDAL.GetByID(id) != null;
       }
   }
}
