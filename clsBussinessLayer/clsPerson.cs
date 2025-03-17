using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsPerson 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int PersonID {get;set;}

     public string Name {get;set;}

     public string Email {get;set;}

     public string Address {get;set;}

     public string Phone {get;set;}

     public bool? Gendor {get;set;}

     public int? CreatedByUserID {get;set;}

     public DateTime CreationDate {get;set;}



 public clsPerson() 
 {
     this.PersonID = default;

     this.Name = default;

     this.Email = default;

     this.Address = default;

     this.Phone = default;

     this.Gendor = default;

     this.CreatedByUserID = default;

     this.CreationDate = default;

      Mode = enMode.AddNew;
 }

 private clsPerson(int PersonID, string Name, 
	string Email, string Address, string Phone, 
	bool? Gendor, int? CreatedByUserID, DateTime CreationDate)
 {
      this.PersonID = PersonID;

      this.Name = Name;

      this.Email = Email;

      this.Address = Address;

      this.Phone = Phone;

      this.Gendor = Gendor;

      this.CreatedByUserID = CreatedByUserID;

      this.CreationDate = CreationDate;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewPersonAsync()
 {
  this.PersonID = await (clsPersonData.AddNewPersonAsync
   (this.PersonID, this.Name, 
	this.Email, this.Address, this.Phone, 
	this.Gendor, this.CreatedByUserID, this.CreationDate));
   return PersonID != 0;
 }

 private async Task<bool> _UpdatePersonAsync()
 {
   return await clsPersonData.UpdatePersonAsync (PersonID, Name, 
	Email, Address, Phone, 
	Gendor, CreatedByUserID, CreationDate); 
 }

 public static async Task<bool> DeletePersonAsync(int PersonID)
 {
   return await clsPersonData.DeletePersonAsync (PersonID); 
 }

 public static clsPerson Find(int PersonID) 
 {

     string Name = default;
	string Email = default;string Address = default;string Phone = default;
	bool? Gendor = default;int? CreatedByUserID = default;DateTime CreationDate = default;

 

 bool IsFound =  clsPersonData.GetPersonByPersonID
               (PersonID, ref Name, 
	ref Email, ref Address, ref Phone, 
	ref Gendor, ref CreatedByUserID, ref CreationDate);

 if(IsFound) { 
   return new clsPerson
                   (PersonID, Name, 
	Email, Address, Phone, 
	Gendor, CreatedByUserID, CreationDate);

}

 return  null;

} 



 public static async Task<bool> IsPersonExistsAsync(int PersonID) 
 {

  return  await clsPersonData.IsPersonExistsAsync(PersonID); 

 }

 public static async Task<DataTable> GetAllPeopleAsync() 
 {

  return  await clsPersonData.GetAllPeopleAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewPersonAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdatePersonAsync(); 

  }

    return false; 
 }



    }
  }