using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsSalesProductDetail 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int SaleProductID {get;set;}

     public int? SalesBillID {get;set;}

     public decimal UnitPrice {get;set;}

     public int Quantity {get;set;}

     public decimal TotalPrice {get;set;}

     public int? StockID {get;set;}



 public clsSalesProductDetail() 
 {
     this.SaleProductID = default;

     this.SalesBillID = default;

     this.UnitPrice = default;

     this.Quantity = default;

     this.TotalPrice = default;

     this.StockID = default;

      Mode = enMode.AddNew;
 }

 private clsSalesProductDetail(int SaleProductID, int? SalesBillID, 
	decimal UnitPrice, int Quantity, decimal TotalPrice, 
	int? StockID)
 {
      this.SaleProductID = SaleProductID;

      this.SalesBillID = SalesBillID;

      this.UnitPrice = UnitPrice;

      this.Quantity = Quantity;

      this.TotalPrice = TotalPrice;

      this.StockID = StockID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewSalesProductDetailAsync()
 {
  this.SaleProductID = await (clsSalesProductDetailData.AddNewSalesProductDetailAsync
   (this.SaleProductID, this.SalesBillID, 
	this.UnitPrice, this.Quantity, this.TotalPrice, 
	this.StockID));
   return SaleProductID != 0;
 }

 private async Task<bool> _UpdateSalesProductDetailAsync()
 {
   return await clsSalesProductDetailData.UpdateSalesProductDetailAsync (SaleProductID, SalesBillID, 
	UnitPrice, Quantity, TotalPrice, 
	StockID); 
 }

 public static async Task<bool> DeleteSalesProductDetailAsync(int SaleProductID)
 {
   return await clsSalesProductDetailData.DeleteSalesProductDetailAsync (SaleProductID); 
 }

 public static clsSalesProductDetail Find(int SaleProductID) 
 {

     int? SalesBillID = default;
	decimal UnitPrice = default;int Quantity = default;decimal TotalPrice = default;
	int? StockID = default;

 

 bool IsFound =  clsSalesProductDetailData.GetSalesProductDetailBySaleProductID
               (SaleProductID, ref SalesBillID, 
	ref UnitPrice, ref Quantity, ref TotalPrice, 
	ref StockID);

 if(IsFound) { 
   return new clsSalesProductDetail
                   (SaleProductID, SalesBillID, 
	UnitPrice, Quantity, TotalPrice, 
	StockID);

}

 return  null;

} 



 public static async Task<bool> IsSalesProductDetailExistsAsync(int SaleProductID) 
 {

  return  await clsSalesProductDetailData.IsSalesProductDetailExistsAsync(SaleProductID); 

 }

 public static async Task<DataTable> GetAllSalesProductDetailsAsync() 
 {

  return  await clsSalesProductDetailData.GetAllSalesProductDetailsAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewSalesProductDetailAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateSalesProductDetailAsync(); 

  }

    return false; 
 }



    }
  }