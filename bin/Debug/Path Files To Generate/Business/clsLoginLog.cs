using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsLoginLog
   {
       public int LogID { get; set; }
       public string UserName { get; set; }
       public string Password { get; set; }
       public DateTime? LoginDate { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsLoginLog() { Mode = enMode.AddNew; }

       private clsLoginLog(int LogID, string UserName, string Password, DateTime? LoginDate)
       {
           this.LogID = LogID;
           this.UserName = UserName;
           this.Password = Password;
           this.LoginDate = LoginDate;
           Mode = enMode.Update;
       }

       public static clsLoginLog Find(int id)
       {
           DataRow row = clsLoginLogDAL.GetByID(id);
           if (row == null) return null;

           return new clsLoginLog(row["LogID"] != DBNull.Value ? (int)row["LogID"] : default, row["UserName"] != DBNull.Value ? (string)row["UserName"] : default, row["Password"] != DBNull.Value ? (string)row["Password"] : default, row["LoginDate"] != DBNull.Value ? (DateTime?)row["LoginDate"] : default);
       }

       private bool _AddNew()
       {
           this.LogID = clsLoginLogDAL.Insert(this.LogID, this.UserName, this.Password, this.LoginDate);
           return (this.LogID != -1);
       }

       private bool _Update()
       {
           return clsLoginLogDAL.Update(this.LogID, this.LogID, this.UserName, this.Password, this.LoginDate);
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
           return clsLoginLogDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsLoginLogDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsLoginLogDAL.GetByID(id) != null;
       }
   }
}
