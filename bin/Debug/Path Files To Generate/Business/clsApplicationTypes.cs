using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsApplicationTypes
   {
       public int ApplicationTypeID { get; set; }
       public string ApplicationTypeTitle { get; set; }
       public decimal ApplicationFees { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsApplicationTypes() { Mode = enMode.AddNew; }

       private clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
       {
           this.ApplicationTypeID = ApplicationTypeID;
           this.ApplicationTypeTitle = ApplicationTypeTitle;
           this.ApplicationFees = ApplicationFees;
           Mode = enMode.Update;
       }

       public static clsApplicationTypes Find(int id)
       {
           DataRow row = clsApplicationTypesDAL.GetByID(id);
           if (row == null) return null;

           return new clsApplicationTypes(row["ApplicationTypeID"] != DBNull.Value ? (int)row["ApplicationTypeID"] : default, row["ApplicationTypeTitle"] != DBNull.Value ? (string)row["ApplicationTypeTitle"] : default, row["ApplicationFees"] != DBNull.Value ? (decimal)row["ApplicationFees"] : default);
       }

       private bool _AddNew()
       {
           this.ApplicationTypeID = clsApplicationTypesDAL.Insert(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
           return (this.ApplicationTypeID != -1);
       }

       private bool _Update()
       {
           return clsApplicationTypesDAL.Update(this.ApplicationTypeID, this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
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
           return clsApplicationTypesDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsApplicationTypesDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsApplicationTypesDAL.GetByID(id) != null;
       }
   }
}
