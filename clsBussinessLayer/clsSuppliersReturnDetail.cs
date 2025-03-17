using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsSuppliersReturnDetail 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int ReturnDetailsID {get;set;}

     public int? ReturnBillID {get;set;}

     public decimal TotalRefund {get;set;}

     public decimal UnitPrice {get;set;}

     public int? StockID {get;set;}



 public clsSuppliersReturnDetail() 
 {
     this.ReturnDetailsID = default;

     this.ReturnBillID = default;

     this.TotalRefund = default;

     this.UnitPrice = default;

     this.StockID = default;

      Mode = enMode.AddNew;
 }

 private clsSuppliersReturnDetail(int ReturnDetailsID, int? ReturnBillID, 
	decimal TotalRefund, decimal UnitPrice, int? StockID)
 {
      this.ReturnDetailsID = ReturnDetailsID;

      this.ReturnBillID = ReturnBillID;

      this.TotalRefund = TotalRefund;

      this.UnitPrice = UnitPrice;

      this.StockID = StockID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewSuppliersReturnDetailAsync()
 {
  this.ReturnDetailsID = await (clsSuppliersReturnDetailData.AddNewSuppliersReturnDetailAsync
   (this.ReturnDetailsID, this.ReturnBillID, 
	this.TotalRefund, this.UnitPrice, this.StockID));
   return ReturnDetailsID != 0;
 }

 private async Task<bool> _UpdateSuppliersReturnDetailAsync()
 {
   return await clsSuppliersReturnDetailData.UpdateSuppliersReturnDetailAsync (ReturnDetailsID, ReturnBillID, 
	TotalRefund, UnitPrice, StockID); 
 }

 public static async Task<bool> DeleteSuppliersReturnDetailAsync(int ReturnDetailsID)
 {
   return await clsSuppliersReturnDetailData.DeleteSuppliersReturnDetailAsync (ReturnDetailsID); 
 }

 public static clsSuppliersReturnDetail Find(int ReturnDetailsID) 
 {

     int? ReturnBillID = default;
	decimal TotalRefund = default;decimal UnitPrice = default;int? StockID = default;

 

 bool IsFound =  clsSuppliersReturnDetailData.GetSuppliersReturnDetailByReturnDetailsID
               (ReturnDetailsID, ref ReturnBillID, 
	ref TotalRefund, ref UnitPrice, ref StockID);

 if(IsFound) { 
   return new clsSuppliersReturnDetail
                   (ReturnDetailsID, ReturnBillID, 
	TotalRefund, UnitPrice, StockID);

}

 return  null;

} 



 public static async Task<bool> IsSuppliersReturnDetailExistsAsync(int ReturnDetailsID) 
 {

  return  await clsSuppliersReturnDetailData.IsSuppliersReturnDetailExistsAsync(ReturnDetailsID); 

 }

 public static async Task<DataTable> GetAllSuppliersReturnDetailsAsync() 
 {

  return  await clsSuppliersReturnDetailData.GetAllSuppliersReturnDetailsAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewSuppliersReturnDetailAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateSuppliersReturnDetailAsync(); 

  }

    return false; 
 }



    }
  }