using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsDrivers
   {
       public int DriverID { get; set; }
       public int PersonID { get; set; }
       public int CreatedByUserID { get; set; }
       public DateTime CreatedDate { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsDrivers() { Mode = enMode.AddNew; }

       private clsDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
       {
           this.DriverID = DriverID;
           this.PersonID = PersonID;
           this.CreatedByUserID = CreatedByUserID;
           this.CreatedDate = CreatedDate;
           Mode = enMode.Update;
       }

       public static clsDrivers Find(int id)
       {
           DataRow row = clsDriversDAL.GetByID(id);
           if (row == null) return null;

           return new clsDrivers(row["DriverID"] != DBNull.Value ? (int)row["DriverID"] : default, row["PersonID"] != DBNull.Value ? (int)row["PersonID"] : default, row["CreatedByUserID"] != DBNull.Value ? (int)row["CreatedByUserID"] : default, row["CreatedDate"] != DBNull.Value ? (DateTime)row["CreatedDate"] : default);
       }

       private bool _AddNew()
       {
           this.DriverID = clsDriversDAL.Insert(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
           return (this.DriverID != -1);
       }

       private bool _Update()
       {
           return clsDriversDAL.Update(this.DriverID, this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
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
           return clsDriversDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsDriversDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsDriversDAL.GetByID(id) != null;
       }
   }
}
