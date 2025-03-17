using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsCustomersReturnDetail 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int ReturnDetailsID {get;set;}

     public int? ReturnBillID {get;set;}

     public decimal TotalRefund {get;set;}

     public decimal UnitPrice {get;set;}

     public int? StockID {get;set;}

     public int Quantity {get;set;}



 public clsCustomersReturnDetail() 
 {
     this.ReturnDetailsID = default;

     this.ReturnBillID = default;

     this.TotalRefund = default;

     this.UnitPrice = default;

     this.StockID = default;

     this.Quantity = default;

      Mode = enMode.AddNew;
 }

 private clsCustomersReturnDetail(int ReturnDetailsID, int? ReturnBillID, 
	decimal TotalRefund, decimal UnitPrice, int? StockID, 
	int Quantity)
 {
      this.ReturnDetailsID = ReturnDetailsID;

      this.ReturnBillID = ReturnBillID;

      this.TotalRefund = TotalRefund;

      this.UnitPrice = UnitPrice;

      this.StockID = StockID;

      this.Quantity = Quantity;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewCustomersReturnDetailAsync()
 {
  this.ReturnDetailsID = await (clsCustomersReturnDetailData.AddNewCustomersReturnDetailAsync
   (this.ReturnDetailsID, this.ReturnBillID, 
	this.TotalRefund, this.UnitPrice, this.StockID, 
	this.Quantity));
   return ReturnDetailsID != 0;
 }

 private async Task<bool> _UpdateCustomersReturnDetailAsync()
 {
   return await clsCustomersReturnDetailData.UpdateCustomersReturnDetailAsync (ReturnDetailsID, ReturnBillID, 
	TotalRefund, UnitPrice, StockID, 
	Quantity); 
 }

 public static async Task<bool> DeleteCustomersReturnDetailAsync(int ReturnDetailsID)
 {
   return await clsCustomersReturnDetailData.DeleteCustomersReturnDetailAsync (ReturnDetailsID); 
 }

 public static clsCustomersReturnDetail Find(int ReturnDetailsID) 
 {

     int? ReturnBillID = default;
	decimal TotalRefund = default;decimal UnitPrice = default;int? StockID = default;
	int Quantity = default;

 

 bool IsFound =  clsCustomersReturnDetailData.GetCustomersReturnDetailByReturnDetailsID
               (ReturnDetailsID, ref ReturnBillID, 
	ref TotalRefund, ref UnitPrice, ref StockID, 
	ref Quantity);

 if(IsFound) { 
   return new clsCustomersReturnDetail
                   (ReturnDetailsID, ReturnBillID, 
	TotalRefund, UnitPrice, StockID, 
	Quantity);

}

 return  null;

} 



 public static async Task<bool> IsCustomersReturnDetailExistsAsync(int ReturnDetailsID) 
 {

  return  await clsCustomersReturnDetailData.IsCustomersReturnDetailExistsAsync(ReturnDetailsID); 

 }

 public static async Task<DataTable> GetAllCustomersReturnDetailsAsync() 
 {

  return  await clsCustomersReturnDetailData.GetAllCustomersReturnDetailsAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewCustomersReturnDetailAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateCustomersReturnDetailAsync(); 

  }

    return false; 
 }



    }
  }