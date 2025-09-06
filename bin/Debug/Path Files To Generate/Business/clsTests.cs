using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsTests
   {
       public int TestID { get; set; }
       public int TestAppointmentID { get; set; }
       public bool TestResult { get; set; }
       public string Notes { get; set; }
       public int CreatedByUserID { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsTests() { Mode = enMode.AddNew; }

       private clsTests(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
       {
           this.TestID = TestID;
           this.TestAppointmentID = TestAppointmentID;
           this.TestResult = TestResult;
           this.Notes = Notes;
           this.CreatedByUserID = CreatedByUserID;
           Mode = enMode.Update;
       }

       public static clsTests Find(int id)
       {
           DataRow row = clsTestsDAL.GetByID(id);
           if (row == null) return null;

           return new clsTests(row["TestID"] != DBNull.Value ? (int)row["TestID"] : default, row["TestAppointmentID"] != DBNull.Value ? (int)row["TestAppointmentID"] : default, row["TestResult"] != DBNull.Value ? (bool)row["TestResult"] : default, row["Notes"] != DBNull.Value ? (string)row["Notes"] : default, row["CreatedByUserID"] != DBNull.Value ? (int)row["CreatedByUserID"] : default);
       }

       private bool _AddNew()
       {
           this.TestID = clsTestsDAL.Insert(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
           return (this.TestID != -1);
       }

       private bool _Update()
       {
           return clsTestsDAL.Update(this.TestID, this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
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
           return clsTestsDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsTestsDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsTestsDAL.GetByID(id) != null;
       }
   }
}
