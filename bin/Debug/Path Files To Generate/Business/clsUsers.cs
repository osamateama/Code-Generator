using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsUsers
   {
       public int UserID { get; set; }
       public int PersonID { get; set; }
       public string UserName { get; set; }
       public string Password { get; set; }
       public bool IsActive { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsUsers() { Mode = enMode.AddNew; }

       private clsUsers(int UserID, int PersonID, string UserName, string Password, bool IsActive)
       {
           this.UserID = UserID;
           this.PersonID = PersonID;
           this.UserName = UserName;
           this.Password = Password;
           this.IsActive = IsActive;
           Mode = enMode.Update;
       }

       public static clsUsers Find(int id)
       {
           DataRow row = clsUsersDAL.GetByID(id);
           if (row == null) return null;

           return new clsUsers(row["UserID"] != DBNull.Value ? (int)row["UserID"] : default, row["PersonID"] != DBNull.Value ? (int)row["PersonID"] : default, row["UserName"] != DBNull.Value ? (string)row["UserName"] : default, row["Password"] != DBNull.Value ? (string)row["Password"] : default, row["IsActive"] != DBNull.Value ? (bool)row["IsActive"] : default);
       }

       private bool _AddNew()
       {
           this.UserID = clsUsersDAL.Insert(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
           return (this.UserID != -1);
       }

       private bool _Update()
       {
           return clsUsersDAL.Update(this.UserID, this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
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
           return clsUsersDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsUsersDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsUsersDAL.GetByID(id) != null;
       }
   }
}
