using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsTestAppointments
   {
       public int TestAppointmentID { get; set; }
       public int TestTypeID { get; set; }
       public int LocalDrivingLicenseApplicationID { get; set; }
       public DateTime AppointmentDate { get; set; }
       public decimal PaidFees { get; set; }
       public int CreatedByUserID { get; set; }
       public bool IsLocked { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsTestAppointments() { Mode = enMode.AddNew; }

       private clsTestAppointments(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
       {
           this.TestAppointmentID = TestAppointmentID;
           this.TestTypeID = TestTypeID;
           this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
           this.AppointmentDate = AppointmentDate;
           this.PaidFees = PaidFees;
           this.CreatedByUserID = CreatedByUserID;
           this.IsLocked = IsLocked;
           Mode = enMode.Update;
       }

       public static clsTestAppointments Find(int id)
       {
           DataRow row = clsTestAppointmentsDAL.GetByID(id);
           if (row == null) return null;

           return new clsTestAppointments(row["TestAppointmentID"] != DBNull.Value ? (int)row["TestAppointmentID"] : default, row["TestTypeID"] != DBNull.Value ? (int)row["TestTypeID"] : default, row["LocalDrivingLicenseApplicationID"] != DBNull.Value ? (int)row["LocalDrivingLicenseApplicationID"] : default, row["AppointmentDate"] != DBNull.Value ? (DateTime)row["AppointmentDate"] : default, row["PaidFees"] != DBNull.Value ? (decimal)row["PaidFees"] : default, row["CreatedByUserID"] != DBNull.Value ? (int)row["CreatedByUserID"] : default, row["IsLocked"] != DBNull.Value ? (bool)row["IsLocked"] : default);
       }

       private bool _AddNew()
       {
           this.TestAppointmentID = clsTestAppointmentsDAL.Insert(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);
           return (this.TestAppointmentID != -1);
       }

       private bool _Update()
       {
           return clsTestAppointmentsDAL.Update(this.TestAppointmentID, this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);
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
           return clsTestAppointmentsDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsTestAppointmentsDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsTestAppointmentsDAL.GetByID(id) != null;
       }
   }
}
