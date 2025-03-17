using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsSuppliersReturnBill 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int ReturnBillID {get;set;}

     public int? PurchaseBillID {get;set;}

     public decimal BillPrice {get;set;}

     public int ReturnMethod {get;set;}

     public int? CreatedByUserID {get;set;}

     public DateTime ReturnDate {get;set;}



 public clsSuppliersReturnBill() 
 {
     this.ReturnBillID = default;

     this.PurchaseBillID = default;

     this.BillPrice = default;

     this.ReturnMethod = default;

     this.CreatedByUserID = default;

     this.ReturnDate = default;

      Mode = enMode.AddNew;
 }

 private clsSuppliersReturnBill(int ReturnBillID, int? PurchaseBillID, 
	decimal BillPrice, int ReturnMethod, int? CreatedByUserID, 
	DateTime ReturnDate)
 {
      this.ReturnBillID = ReturnBillID;

      this.PurchaseBillID = PurchaseBillID;

      this.BillPrice = BillPrice;

      this.ReturnMethod = ReturnMethod;

      this.CreatedByUserID = CreatedByUserID;

      this.ReturnDate = ReturnDate;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewSuppliersReturnBillAsync()
 {
  this.ReturnBillID = await (clsSuppliersReturnBillData.AddNewSuppliersReturnBillAsync
   (this.ReturnBillID, this.PurchaseBillID, 
	this.BillPrice, this.ReturnMethod, this.CreatedByUserID, 
	this.ReturnDate));
   return ReturnBillID != 0;
 }

 private async Task<bool> _UpdateSuppliersReturnBillAsync()
 {
   return await clsSuppliersReturnBillData.UpdateSuppliersReturnBillAsync (ReturnBillID, PurchaseBillID, 
	BillPrice, ReturnMethod, CreatedByUserID, 
	ReturnDate); 
 }

 public static async Task<bool> DeleteSuppliersReturnBillAsync(int ReturnBillID)
 {
   return await clsSuppliersReturnBillData.DeleteSuppliersReturnBillAsync (ReturnBillID); 
 }

 public static clsSuppliersReturnBill Find(int ReturnBillID) 
 {

     int? PurchaseBillID = default;
	decimal BillPrice = default;int ReturnMethod = default;int? CreatedByUserID = default;
	DateTime ReturnDate = default;

 

 bool IsFound =  clsSuppliersReturnBillData.GetSuppliersReturnBillByReturnBillID
               (ReturnBillID, ref PurchaseBillID, 
	ref BillPrice, ref ReturnMethod, ref CreatedByUserID, 
	ref ReturnDate);

 if(IsFound) { 
   return new clsSuppliersReturnBill
                   (ReturnBillID, PurchaseBillID, 
	BillPrice, ReturnMethod, CreatedByUserID, 
	ReturnDate);

}

 return  null;

} 



 public static async Task<bool> IsSuppliersReturnBillExistsAsync(int ReturnBillID) 
 {

  return  await clsSuppliersReturnBillData.IsSuppliersReturnBillExistsAsync(ReturnBillID); 

 }

 public static async Task<DataTable> GetAllSuppliersReturnBillAsync() 
 {

  return  await clsSuppliersReturnBillData.GetAllSuppliersReturnBillAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewSuppliersReturnBillAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateSuppliersReturnBillAsync(); 

  }

    return false; 
 }



    }
  }