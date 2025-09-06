using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clsPeople
   {
       public int PersonID { get; set; }
       public string NationalNo { get; set; }
       public string FirstName { get; set; }
       public string SecondName { get; set; }
       public string ThirdName { get; set; }
       public string LastName { get; set; }
       public DateTime DateOfBirth { get; set; }
       public int Gender { get; set; }
       public string Address { get; set; }
       public string Phone { get; set; }
       public string Email { get; set; }
       public int NationalityCountryID { get; set; }
       public string ImagePath { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clsPeople() { Mode = enMode.AddNew; }

       private clsPeople(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
       {
           this.PersonID = PersonID;
           this.NationalNo = NationalNo;
           this.FirstName = FirstName;
           this.SecondName = SecondName;
           this.ThirdName = ThirdName;
           this.LastName = LastName;
           this.DateOfBirth = DateOfBirth;
           this.Gender = Gender;
           this.Address = Address;
           this.Phone = Phone;
           this.Email = Email;
           this.NationalityCountryID = NationalityCountryID;
           this.ImagePath = ImagePath;
           Mode = enMode.Update;
       }

       public static clsPeople Find(int id)
       {
           DataRow row = clsPeopleDAL.GetByID(id);
           if (row == null) return null;

           return new clsPeople(row["PersonID"] != DBNull.Value ? (int)row["PersonID"] : default, row["NationalNo"] != DBNull.Value ? (string)row["NationalNo"] : default, row["FirstName"] != DBNull.Value ? (string)row["FirstName"] : default, row["SecondName"] != DBNull.Value ? (string)row["SecondName"] : default, row["ThirdName"] != DBNull.Value ? (string)row["ThirdName"] : default, row["LastName"] != DBNull.Value ? (string)row["LastName"] : default, row["DateOfBirth"] != DBNull.Value ? (DateTime)row["DateOfBirth"] : default, row["Gender"] != DBNull.Value ? (int)row["Gender"] : default, row["Address"] != DBNull.Value ? (string)row["Address"] : default, row["Phone"] != DBNull.Value ? (string)row["Phone"] : default, row["Email"] != DBNull.Value ? (string)row["Email"] : default, row["NationalityCountryID"] != DBNull.Value ? (int)row["NationalityCountryID"] : default, row["ImagePath"] != DBNull.Value ? (string)row["ImagePath"] : default);
       }

       private bool _AddNew()
       {
           this.PersonID = clsPeopleDAL.Insert(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
           return (this.PersonID != -1);
       }

       private bool _Update()
       {
           return clsPeopleDAL.Update(this.PersonID, this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
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
           return clsPeopleDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clsPeopleDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clsPeopleDAL.GetByID(id) != null;
       }
   }
}
