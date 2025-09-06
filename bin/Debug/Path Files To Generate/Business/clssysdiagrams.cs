using DataAccessLayer;
using System;
using System.Data;

namespace BusinessLayer
{
   public class clssysdiagrams
   {
       public string name { get; set; }
       public int principal_id { get; set; }
       public int diagram_id { get; set; }
       public int? version { get; set; }
       public byte[] definition { get; set; }

       public enum enMode { AddNew = 0, Update = 1 }
       public enMode Mode { get; set; } = enMode.AddNew;

       public clssysdiagrams() { Mode = enMode.AddNew; }

       private clssysdiagrams(string name, int principal_id, int diagram_id, int? version, byte[] definition)
       {
           this.name = name;
           this.principal_id = principal_id;
           this.diagram_id = diagram_id;
           this.version = version;
           this.definition = definition;
           Mode = enMode.Update;
       }

       public static clssysdiagrams Find(int id)
       {
           DataRow row = clssysdiagramsDAL.GetByID(id);
           if (row == null) return null;

           return new clssysdiagrams(row["name"] != DBNull.Value ? (string)row["name"] : default, row["principal_id"] != DBNull.Value ? (int)row["principal_id"] : default, row["diagram_id"] != DBNull.Value ? (int)row["diagram_id"] : default, row["version"] != DBNull.Value ? (int?)row["version"] : default, row["definition"] != DBNull.Value ? (byte[])row["definition"] : default);
       }

       private bool _AddNew()
       {
           this.name = clssysdiagramsDAL.Insert(this.name, this.principal_id, this.diagram_id, this.version, this.definition);
           return (this.name != -1);
       }

       private bool _Update()
       {
           return clssysdiagramsDAL.Update(this.name, this.name, this.principal_id, this.diagram_id, this.version, this.definition);
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
           return clssysdiagramsDAL.Delete(id);
       }

       public static DataTable GetAll()
       {
           return clssysdiagramsDAL.GetAll();
       }

       public static bool IsExist(int id)
       {
           return clssysdiagramsDAL.GetByID(id) != null;
       }
   }
}
