using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsLicenseClasses
   {
       public int LicenseClassID { get; set; }
       public string ClassName { get; set; }
       public string ClassDescription { get; set; }
       public int MinimumAllowedAge { get; set; }
       public int DefaultValidityLength { get; set; }
       public decimal ClassFees { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsLicenseClasses() { Mode = enMode.AddNew; }

       private clsLicenseClasses(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
       {
           this.LicenseClassID = LicenseClassID;
           this.ClassName = ClassName;
           this.ClassDescription = ClassDescription;
           this.MinimumAllowedAge = MinimumAllowedAge;
           this.DefaultValidityLength = DefaultValidityLength;
           this.ClassFees = ClassFees;
           Mode = enMode.Update;
       }

       public static clsLicenseClasses Find(int id)
       {
           DataRow row = clsLicenseClassesDAL.GetByID(id);
           if (row == null) return null;

           return new clsLicenseClasses(row["LicenseClassID"] != DBNull.Value ? (int)row["LicenseClassID"] : default, row["ClassName"] != DBNull.Value ? (string)row["ClassName"] : default, row["ClassDescription"] != DBNull.Value ? (string)row["ClassDescription"] : default, row["MinimumAllowedAge"] != DBNull.Value ? (int)row["MinimumAllowedAge"] : default, row["DefaultValidityLength"] != DBNull.Value ? (int)row["DefaultValidityLength"] : default, row["ClassFees"] != DBNull.Value ? (decimal)row["ClassFees"] : default);
       }

       private bool _AddNew()
       {
           this.LicenseClassID = clsLicenseClassesDAL.Insert(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
           return (this.LicenseClassID != -1);
       }

       private bool _Update()
       {
           return clsLicenseClassesDAL.Update(this.LicenseClassID, this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
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
           return clsLicenseClassesDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsLicenseClassesDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsLicenseClassesDAL.GetByID(id) != null;
       }
   }
}
