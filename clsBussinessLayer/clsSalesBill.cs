using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsSalesBill 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int SalesBillID {get;set;}

     public int? CustomerID {get;set;}

     public decimal BillPrice {get;set;}

     public int PaymentMethod {get;set;}

     public int? CreatedByUserID {get;set;}

     public DateTime CreationDate {get;set;}

     public bool ReturnAvailabilty {get;set;}

     public int? TodayRevenueBillID {get;set;}



 public clsSalesBill() 
 {
     this.SalesBillID = default;

     this.CustomerID = default;

     this.BillPrice = default;

     this.PaymentMethod = default;

     this.CreatedByUserID = default;

     this.CreationDate = default;

     this.ReturnAvailabilty = default;

     this.TodayRevenueBillID = default;

      Mode = enMode.AddNew;
 }

 private clsSalesBill(int SalesBillID, int? CustomerID, 
	decimal BillPrice, int PaymentMethod, int? CreatedByUserID, 
	DateTime CreationDate, bool ReturnAvailabilty, int? TodayRevenueBillID)
 {
      this.SalesBillID = SalesBillID;

      this.CustomerID = CustomerID;

      this.BillPrice = BillPrice;

      this.PaymentMethod = PaymentMethod;

      this.CreatedByUserID = CreatedByUserID;

      this.CreationDate = CreationDate;

      this.ReturnAvailabilty = ReturnAvailabilty;

      this.TodayRevenueBillID = TodayRevenueBillID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewSalesBillAsync()
 {
  this.SalesBillID = await (clsSalesBillData.AddNewSalesBillAsync
   (this.SalesBillID, this.CustomerID, 
	this.BillPrice, this.PaymentMethod, this.CreatedByUserID, 
	this.CreationDate, this.ReturnAvailabilty, this.TodayRevenueBillID));
   return SalesBillID != 0;
 }

 private async Task<bool> _UpdateSalesBillAsync()
 {
   return await clsSalesBillData.UpdateSalesBillAsync (SalesBillID, CustomerID, 
	BillPrice, PaymentMethod, CreatedByUserID, 
	CreationDate, ReturnAvailabilty, TodayRevenueBillID); 
 }

 public static async Task<bool> DeleteSalesBillAsync(int SalesBillID)
 {
   return await clsSalesBillData.DeleteSalesBillAsync (SalesBillID); 
 }

 public static clsSalesBill Find(int SalesBillID) 
 {

     int? CustomerID = default;
	decimal BillPrice = default;int PaymentMethod = default;int? CreatedByUserID = default;
	DateTime CreationDate = default;bool ReturnAvailabilty = default;int? TodayRevenueBillID = default;

 

 bool IsFound =  clsSalesBillData.GetSalesBillBySalesBillID
               (SalesBillID, ref CustomerID, 
	ref BillPrice, ref PaymentMethod, ref CreatedByUserID, 
	ref CreationDate, ref ReturnAvailabilty, ref TodayRevenueBillID);

 if(IsFound) { 
   return new clsSalesBill
                   (SalesBillID, CustomerID, 
	BillPrice, PaymentMethod, CreatedByUserID, 
	CreationDate, ReturnAvailabilty, TodayRevenueBillID);

}

 return  null;

} 



 public static async Task<bool> IsSalesBillExistsAsync(int SalesBillID) 
 {

  return  await clsSalesBillData.IsSalesBillExistsAsync(SalesBillID); 

 }

 public static async Task<DataTable> GetAllSalesBillAsync() 
 {

  return  await clsSalesBillData.GetAllSalesBillAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewSalesBillAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateSalesBillAsync(); 

  }

    return false; 
 }



    }
  }