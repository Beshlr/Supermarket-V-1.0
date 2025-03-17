using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsCustomer 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int CustomarID {get;set;}

     public int? PersonID {get;set;}

     public DateTime CreationDate {get;set;}

     public int? CreatedByUserID {get;set;}

     public decimal? Balance {get;set;}

     public double? DiscountPoints {get;set;}



 public clsCustomer() 
 {
     this.CustomarID = default;

     this.PersonID = default;

     this.CreationDate = default;

     this.CreatedByUserID = default;

     this.Balance = default;

     this.DiscountPoints = default;

      Mode = enMode.AddNew;
 }

 private clsCustomer(int CustomarID, int? PersonID, 
	DateTime CreationDate, int? CreatedByUserID, decimal? Balance, 
	double? DiscountPoints)
 {
      this.CustomarID = CustomarID;

      this.PersonID = PersonID;

      this.CreationDate = CreationDate;

      this.CreatedByUserID = CreatedByUserID;

      this.Balance = Balance;

      this.DiscountPoints = DiscountPoints;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewCustomerAsync()
 {
  this.CustomarID = await (clsCustomerData.AddNewCustomerAsync
   (this.CustomarID, this.PersonID, 
	this.CreationDate, this.CreatedByUserID, this.Balance, 
	this.DiscountPoints));
   return CustomarID != 0;
 }

 private async Task<bool> _UpdateCustomerAsync()
 {
   return await clsCustomerData.UpdateCustomerAsync (CustomarID, PersonID, 
	CreationDate, CreatedByUserID, Balance, 
	DiscountPoints); 
 }

 public static async Task<bool> DeleteCustomerAsync(int CustomarID)
 {
   return await clsCustomerData.DeleteCustomerAsync (CustomarID); 
 }

 public static clsCustomer Find(int CustomarID) 
 {

     int? PersonID = default;
	DateTime CreationDate = default;int? CreatedByUserID = default;decimal? Balance = default;
	double? DiscountPoints = default;

 

 bool IsFound =  clsCustomerData.GetCustomerByCustomarID
               (CustomarID, ref PersonID, 
	ref CreationDate, ref CreatedByUserID, ref Balance, 
	ref DiscountPoints);

 if(IsFound) { 
   return new clsCustomer
                   (CustomarID, PersonID, 
	CreationDate, CreatedByUserID, Balance, 
	DiscountPoints);

}

 return  null;

} 



 public static async Task<bool> IsCustomerExistsAsync(int CustomarID) 
 {

  return  await clsCustomerData.IsCustomerExistsAsync(CustomarID); 

 }

 public static async Task<DataTable> GetAllCustomersAsync() 
 {

  return  await clsCustomerData.GetAllCustomersAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewCustomerAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateCustomerAsync(); 

  }

    return false; 
 }



    }
  }