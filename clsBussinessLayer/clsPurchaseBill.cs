using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsPurchaseBill 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int PurchBillID {get;set;}

     public int? SupplierID {get;set;}

     public decimal BillPrice {get;set;}

     public int PaymentMethod {get;set;}

     public int? CreatedByUserID {get;set;}

     public DateTime CreationDate {get;set;}

     public bool ReturnAvailabilty {get;set;}

     public bool IsEMP {get;set;}

     public decimal ActualEarnings {get;set;}

     public int? TodayRevenueID {get;set;}



 public clsPurchaseBill() 
 {
     this.PurchBillID = default;

     this.SupplierID = default;

     this.BillPrice = default;

     this.PaymentMethod = default;

     this.CreatedByUserID = default;

     this.CreationDate = default;

     this.ReturnAvailabilty = default;

     this.IsEMP = default;

     this.ActualEarnings = default;

     this.TodayRevenueID = default;

      Mode = enMode.AddNew;
 }

 private clsPurchaseBill(int PurchBillID, int? SupplierID, 
	decimal BillPrice, int PaymentMethod, int? CreatedByUserID, 
	DateTime CreationDate, bool ReturnAvailabilty, bool IsEMP, 
	decimal ActualEarnings, int? TodayRevenueID)
 {
      this.PurchBillID = PurchBillID;

      this.SupplierID = SupplierID;

      this.BillPrice = BillPrice;

      this.PaymentMethod = PaymentMethod;

      this.CreatedByUserID = CreatedByUserID;

      this.CreationDate = CreationDate;

      this.ReturnAvailabilty = ReturnAvailabilty;

      this.IsEMP = IsEMP;

      this.ActualEarnings = ActualEarnings;

      this.TodayRevenueID = TodayRevenueID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewPurchaseBillAsync()
 {
  this.PurchBillID = await (clsPurchaseBillData.AddNewPurchaseBillAsync
   (this.PurchBillID, this.SupplierID, 
	this.BillPrice, this.PaymentMethod, this.CreatedByUserID, 
	this.CreationDate, this.ReturnAvailabilty, this.IsEMP, 
	this.ActualEarnings, this.TodayRevenueID));
   return PurchBillID != 0;
 }

 private async Task<bool> _UpdatePurchaseBillAsync()
 {
   return await clsPurchaseBillData.UpdatePurchaseBillAsync (PurchBillID, SupplierID, 
	BillPrice, PaymentMethod, CreatedByUserID, 
	CreationDate, ReturnAvailabilty, IsEMP, 
	ActualEarnings, TodayRevenueID); 
 }

 public static async Task<bool> DeletePurchaseBillAsync(int PurchBillID)
 {
   return await clsPurchaseBillData.DeletePurchaseBillAsync (PurchBillID); 
 }

 public static clsPurchaseBill Find(int PurchBillID) 
 {

     int? SupplierID = default;
	decimal BillPrice = default;int PaymentMethod = default;int? CreatedByUserID = default;
	DateTime CreationDate = default;bool ReturnAvailabilty = default;bool IsEMP = default;
	decimal ActualEarnings = default;int? TodayRevenueID = default;

 

 bool IsFound =  clsPurchaseBillData.GetPurchaseBillByPurchBillID
               (PurchBillID, ref SupplierID, 
	ref BillPrice, ref PaymentMethod, ref CreatedByUserID, 
	ref CreationDate, ref ReturnAvailabilty, ref IsEMP, 
	ref ActualEarnings, ref TodayRevenueID);

 if(IsFound) { 
   return new clsPurchaseBill
                   (PurchBillID, SupplierID, 
	BillPrice, PaymentMethod, CreatedByUserID, 
	CreationDate, ReturnAvailabilty, IsEMP, 
	ActualEarnings, TodayRevenueID);

}

 return  null;

} 



 public static async Task<bool> IsPurchaseBillExistsAsync(int PurchBillID) 
 {

  return  await clsPurchaseBillData.IsPurchaseBillExistsAsync(PurchBillID); 

 }

 public static async Task<DataTable> GetAllPurchaseBillAsync() 
 {

  return  await clsPurchaseBillData.GetAllPurchaseBillAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewPurchaseBillAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdatePurchaseBillAsync(); 

  }

    return false; 
 }



    }
  }