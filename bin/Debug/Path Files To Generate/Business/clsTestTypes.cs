using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsTestTypes
   {
       public int TestTypeID { get; set; }
       public string TestTypeTitle { get; set; }
       public string TestTypeDescription { get; set; }
       public decimal TestTypeFees { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsTestTypes() { Mode = enMode.AddNew; }

       private clsTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
       {
           this.TestTypeID = TestTypeID;
           this.TestTypeTitle = TestTypeTitle;
           this.TestTypeDescription = TestTypeDescription;
           this.TestTypeFees = TestTypeFees;
           Mode = enMode.Update;
       }

       public static clsTestTypes Find(int id)
       {
           DataRow row = clsTestTypesDAL.GetByID(id);
           if (row == null) return null;

           return new clsTestTypes(row["TestTypeID"] != DBNull.Value ? (int)row["TestTypeID"] : default, row["TestTypeTitle"] != DBNull.Value ? (string)row["TestTypeTitle"] : default, row["TestTypeDescription"] != DBNull.Value ? (string)row["TestTypeDescription"] : default, row["TestTypeFees"] != DBNull.Value ? (decimal)row["TestTypeFees"] : default);
       }

       private bool _AddNew()
       {
           this.TestTypeID = clsTestTypesDAL.Insert(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
           return (this.TestTypeID != -1);
       }

       private bool _Update()
       {
           return clsTestTypesDAL.Update(this.TestTypeID, this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
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
           return clsTestTypesDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsTestTypesDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsTestTypesDAL.GetByID(id) != null;
       }
   }
}
