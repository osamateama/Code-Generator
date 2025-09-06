using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsCountries
   {
       public int CountryID { get; set; }
       public string CountryName { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsCountries() { Mode = enMode.AddNew; }

       private clsCountries(int CountryID, string CountryName)
       {
           this.CountryID = CountryID;
           this.CountryName = CountryName;
           Mode = enMode.Update;
       }

       public static clsCountries Find(int id)
       {
           DataRow row = clsCountriesDAL.GetByID(id);
           if (row == null) return null;

           return new clsCountries(row["CountryID"] != DBNull.Value ? (int)row["CountryID"] : default, row["CountryName"] != DBNull.Value ? (string)row["CountryName"] : default);
       }

       private bool _AddNew()
       {
           this.CountryID = clsCountriesDAL.Insert(this.CountryID, this.CountryName);
           return (this.CountryID != -1);
       }

       private bool _Update()
       {
           return clsCountriesDAL.Update(this.CountryID, this.CountryID, this.CountryName);
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
           return clsCountriesDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsCountriesDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsCountriesDAL.GetByID(id) != null;
       }
   }
}
