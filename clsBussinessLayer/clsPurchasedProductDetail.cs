using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsPurchasedProductDetail 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int ProductID {get;set;}

     public int? BillID {get;set;}

     public string Description {get;set;}

     public string Producer {get;set;}

     public int BoxQuantity {get;set;}

     public int ItemsQuantityInBox {get;set;}

     public int BoxPrice {get;set;}

     public int? StockID {get;set;}



 public clsPurchasedProductDetail() 
 {
     this.ProductID = default;

     this.BillID = default;

     this.Description = default;

     this.Producer = default;

     this.BoxQuantity = default;

     this.ItemsQuantityInBox = default;

     this.BoxPrice = default;

     this.StockID = default;

      Mode = enMode.AddNew;
 }

 private clsPurchasedProductDetail(int ProductID, int? BillID, 
	string Description, string Producer, int BoxQuantity, 
	int ItemsQuantityInBox, int BoxPrice, int? StockID)
 {
      this.ProductID = ProductID;

      this.BillID = BillID;

      this.Description = Description;

      this.Producer = Producer;

      this.BoxQuantity = BoxQuantity;

      this.ItemsQuantityInBox = ItemsQuantityInBox;

      this.BoxPrice = BoxPrice;

      this.StockID = StockID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewPurchasedProductDetailAsync()
 {
  this.ProductID = await (clsPurchasedProductDetailData.AddNewPurchasedProductDetailAsync
   (this.ProductID, this.BillID, 
	this.Description, this.Producer, this.BoxQuantity, 
	this.ItemsQuantityInBox, this.BoxPrice, this.StockID));
   return ProductID != 0;
 }

 private async Task<bool> _UpdatePurchasedProductDetailAsync()
 {
   return await clsPurchasedProductDetailData.UpdatePurchasedProductDetailAsync (ProductID, BillID, 
	Description, Producer, BoxQuantity, 
	ItemsQuantityInBox, BoxPrice, StockID); 
 }

 public static async Task<bool> DeletePurchasedProductDetailAsync(int ProductID)
 {
   return await clsPurchasedProductDetailData.DeletePurchasedProductDetailAsync (ProductID); 
 }

 public static clsPurchasedProductDetail Find(int ProductID) 
 {

     int? BillID = default;
	string Description = default;string Producer = default;int BoxQuantity = default;
	int ItemsQuantityInBox = default;int BoxPrice = default;int? StockID = default;

 

 bool IsFound =  clsPurchasedProductDetailData.GetPurchasedProductDetailByProductID
               (ProductID, ref BillID, 
	ref Description, ref Producer, ref BoxQuantity, 
	ref ItemsQuantityInBox, ref BoxPrice, ref StockID);

 if(IsFound) { 
   return new clsPurchasedProductDetail
                   (ProductID, BillID, 
	Description, Producer, BoxQuantity, 
	ItemsQuantityInBox, BoxPrice, StockID);

}

 return  null;

} 



 public static async Task<bool> IsPurchasedProductDetailExistsAsync(int ProductID) 
 {

  return  await clsPurchasedProductDetailData.IsPurchasedProductDetailExistsAsync(ProductID); 

 }

 public static async Task<DataTable> GetAllPurchasedProductDetailsAsync() 
 {

  return  await clsPurchasedProductDetailData.GetAllPurchasedProductDetailsAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewPurchasedProductDetailAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdatePurchasedProductDetailAsync(); 

  }

    return false; 
 }



    }
  }