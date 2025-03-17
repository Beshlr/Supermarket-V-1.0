using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsSupplier 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int SupplierID {get;set;}

     public int? PersonID {get;set;}

     public string NationalNo {get;set;}

     public bool IsActive {get;set;}

     public DateTime CreationDate {get;set;}

     public int? CreatedByUserID {get;set;}

     public decimal? Depts {get;set;}



 public clsSupplier() 
 {
     this.SupplierID = default;

     this.PersonID = default;

     this.NationalNo = default;

     this.IsActive = default;

     this.CreationDate = default;

     this.CreatedByUserID = default;

     this.Depts = default;

      Mode = enMode.AddNew;
 }

 private clsSupplier(int SupplierID, int? PersonID, 
	string NationalNo, bool IsActive, DateTime CreationDate, 
	int? CreatedByUserID, decimal? Depts)
 {
      this.SupplierID = SupplierID;

      this.PersonID = PersonID;

      this.NationalNo = NationalNo;

      this.IsActive = IsActive;

      this.CreationDate = CreationDate;

      this.CreatedByUserID = CreatedByUserID;

      this.Depts = Depts;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewSupplierAsync()
 {
  this.SupplierID = await (clsSupplierData.AddNewSupplierAsync
   (this.SupplierID, this.PersonID, 
	this.NationalNo, this.IsActive, this.CreationDate, 
	this.CreatedByUserID, this.Depts));
   return SupplierID != 0;
 }

 private async Task<bool> _UpdateSupplierAsync()
 {
   return await clsSupplierData.UpdateSupplierAsync (SupplierID, PersonID, 
	NationalNo, IsActive, CreationDate, 
	CreatedByUserID, Depts); 
 }

 public static async Task<bool> DeleteSupplierAsync(int SupplierID)
 {
   return await clsSupplierData.DeleteSupplierAsync (SupplierID); 
 }

 public static clsSupplier Find(int SupplierID) 
 {

     int? PersonID = default;
	string NationalNo = default;bool IsActive = default;DateTime CreationDate = default;
	int? CreatedByUserID = default;decimal? Depts = default;

 

 bool IsFound =  clsSupplierData.GetSupplierBySupplierID
               (SupplierID, ref PersonID, 
	ref NationalNo, ref IsActive, ref CreationDate, 
	ref CreatedByUserID, ref Depts);

 if(IsFound) { 
   return new clsSupplier
                   (SupplierID, PersonID, 
	NationalNo, IsActive, CreationDate, 
	CreatedByUserID, Depts);

}

 return  null;

} 



 public static async Task<bool> IsSupplierExistsAsync(int SupplierID) 
 {

  return  await clsSupplierData.IsSupplierExistsAsync(SupplierID); 

 }

 public static async Task<DataTable> GetAllSuppliersAsync() 
 {

  return  await clsSupplierData.GetAllSuppliersAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewSupplierAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateSupplierAsync(); 

  }

    return false; 
 }



    }
  }