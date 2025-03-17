using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsCustomersReturnBill 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int ReturnBillID {get;set;}

     public int? SaleBillID {get;set;}

     public decimal BillPrice {get;set;}

     public int ReturnMethod {get;set;}

     public int? CreatedByUserID {get;set;}

     public DateTime ReturnDate {get;set;}

     public decimal TotalRefundPrice {get;set;}

     public string Description {get;set;}



 public clsCustomersReturnBill() 
 {
     this.ReturnBillID = default;

     this.SaleBillID = default;

     this.BillPrice = default;

     this.ReturnMethod = default;

     this.CreatedByUserID = default;

     this.ReturnDate = default;

     this.TotalRefundPrice = default;

     this.Description = default;

      Mode = enMode.AddNew;
 }

 private clsCustomersReturnBill(int ReturnBillID, int? SaleBillID, 
	decimal BillPrice, int ReturnMethod, int? CreatedByUserID, 
	DateTime ReturnDate, decimal TotalRefundPrice, string Description)
 {
      this.ReturnBillID = ReturnBillID;

      this.SaleBillID = SaleBillID;

      this.BillPrice = BillPrice;

      this.ReturnMethod = ReturnMethod;

      this.CreatedByUserID = CreatedByUserID;

      this.ReturnDate = ReturnDate;

      this.TotalRefundPrice = TotalRefundPrice;

      this.Description = Description;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewCustomersReturnBillAsync()
 {
  this.ReturnBillID = await (clsCustomersReturnBillData.AddNewCustomersReturnBillAsync
   (this.ReturnBillID, this.SaleBillID, 
	this.BillPrice, this.ReturnMethod, this.CreatedByUserID, 
	this.ReturnDate, this.TotalRefundPrice, this.Description));
   return ReturnBillID != 0;
 }

 private async Task<bool> _UpdateCustomersReturnBillAsync()
 {
   return await clsCustomersReturnBillData.UpdateCustomersReturnBillAsync (ReturnBillID, SaleBillID, 
	BillPrice, ReturnMethod, CreatedByUserID, 
	ReturnDate, TotalRefundPrice, Description); 
 }

 public static async Task<bool> DeleteCustomersReturnBillAsync(int ReturnBillID)
 {
   return await clsCustomersReturnBillData.DeleteCustomersReturnBillAsync (ReturnBillID); 
 }

 public static clsCustomersReturnBill Find(int ReturnBillID) 
 {

     int? SaleBillID = default;
	decimal BillPrice = default;int ReturnMethod = default;int? CreatedByUserID = default;
	DateTime ReturnDate = default;decimal TotalRefundPrice = default;string Description = default;

 

 bool IsFound =  clsCustomersReturnBillData.GetCustomersReturnBillByReturnBillID
               (ReturnBillID, ref SaleBillID, 
	ref BillPrice, ref ReturnMethod, ref CreatedByUserID, 
	ref ReturnDate, ref TotalRefundPrice, ref Description);

 if(IsFound) { 
   return new clsCustomersReturnBill
                   (ReturnBillID, SaleBillID, 
	BillPrice, ReturnMethod, CreatedByUserID, 
	ReturnDate, TotalRefundPrice, Description);

}

 return  null;

} 



 public static async Task<bool> IsCustomersReturnBillExistsAsync(int ReturnBillID) 
 {

  return  await clsCustomersReturnBillData.IsCustomersReturnBillExistsAsync(ReturnBillID); 

 }

 public static async Task<DataTable> GetAllCustomersReturnBillAsync() 
 {

  return  await clsCustomersReturnBillData.GetAllCustomersReturnBillAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewCustomersReturnBillAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateCustomersReturnBillAsync(); 

  }

    return false; 
 }



    }
  }