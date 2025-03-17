using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsProductsInStock 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int P_Stock_ID {get;set;}

     public string ItemTitle {get;set;}

     public string Description {get;set;}

     public string Barcode {get;set;}

     public int? CategoryID {get;set;}

     public decimal? SupplierPrice {get;set;}

     public decimal? SalePrice {get;set;}

     public int Quantity {get;set;}

     public string Producer {get;set;}

     public DateTime? ProductionDate {get;set;}

     public DateTime? ExpirationDate {get;set;}

     public int? LeastQuantity {get;set;}

     public int? SupplierID {get;set;}

     public DateTime? MaxReturnDate {get;set;}



 public clsProductsInStock() 
 {
     this.P_Stock_ID = default;

     this.ItemTitle = default;

     this.Description = default;

     this.Barcode = default;

     this.CategoryID = default;

     this.SupplierPrice = default;

     this.SalePrice = default;

     this.Quantity = default;

     this.Producer = default;

     this.ProductionDate = default;

     this.ExpirationDate = default;

     this.LeastQuantity = default;

     this.SupplierID = default;

     this.MaxReturnDate = default;

      Mode = enMode.AddNew;
 }

 private clsProductsInStock(int P_Stock_ID, string ItemTitle, 
	string Description, string Barcode, int? CategoryID, 
	decimal? SupplierPrice, decimal? SalePrice, int Quantity, 
	string Producer, DateTime? ProductionDate, DateTime? ExpirationDate, 
	int? LeastQuantity, int? SupplierID, DateTime? MaxReturnDate)
 {
      this.P_Stock_ID = P_Stock_ID;

      this.ItemTitle = ItemTitle;

      this.Description = Description;

      this.Barcode = Barcode;

      this.CategoryID = CategoryID;

      this.SupplierPrice = SupplierPrice;

      this.SalePrice = SalePrice;

      this.Quantity = Quantity;

      this.Producer = Producer;

      this.ProductionDate = ProductionDate;

      this.ExpirationDate = ExpirationDate;

      this.LeastQuantity = LeastQuantity;

      this.SupplierID = SupplierID;

      this.MaxReturnDate = MaxReturnDate;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewProductsInStockAsync()
 {
  this.P_Stock_ID = await (clsProductsInStockData.AddNewProductsInStockAsync
   (this.P_Stock_ID, this.ItemTitle, 
	this.Description, this.Barcode, this.CategoryID, 
	this.SupplierPrice, this.SalePrice, this.Quantity, 
	this.Producer, this.ProductionDate, this.ExpirationDate, 
	this.LeastQuantity, this.SupplierID, this.MaxReturnDate));
   return P_Stock_ID != 0;
 }

 private async Task<bool> _UpdateProductsInStockAsync()
 {
   return await clsProductsInStockData.UpdateProductsInStockAsync (P_Stock_ID, ItemTitle, 
	Description, Barcode, CategoryID, 
	SupplierPrice, SalePrice, Quantity, 
	Producer, ProductionDate, ExpirationDate, 
	LeastQuantity, SupplierID, MaxReturnDate); 
 }

 public static async Task<bool> DeleteProductsInStockAsync(int P_Stock_ID)
 {
   return await clsProductsInStockData.DeleteProductsInStockAsync (P_Stock_ID); 
 }

 public static clsProductsInStock Find(int P_Stock_ID) 
 {

     string ItemTitle = default;
	string Description = default;string Barcode = default;int? CategoryID = default;
	decimal? SupplierPrice = default;decimal? SalePrice = default;int Quantity = default;
	string Producer = default;DateTime? ProductionDate = default;DateTime? ExpirationDate = default;
	int? LeastQuantity = default;int? SupplierID = default;DateTime? MaxReturnDate = default;

 

 bool IsFound =  clsProductsInStockData.GetProductsInStockByP_Stock_ID
               (P_Stock_ID, ref ItemTitle, 
	ref Description, ref Barcode, ref CategoryID, 
	ref SupplierPrice, ref SalePrice, ref Quantity, 
	ref Producer, ref ProductionDate, ref ExpirationDate, 
	ref LeastQuantity, ref SupplierID, ref MaxReturnDate);

 if(IsFound) { 
   return new clsProductsInStock
                   (P_Stock_ID, ItemTitle, 
	Description, Barcode, CategoryID, 
	SupplierPrice, SalePrice, Quantity, 
	Producer, ProductionDate, ExpirationDate, 
	LeastQuantity, SupplierID, MaxReturnDate);

}

 return  null;

} 



 public static async Task<bool> IsProductsInStockExistsAsync(int P_Stock_ID) 
 {

  return  await clsProductsInStockData.IsProductsInStockExistsAsync(P_Stock_ID); 

 }

 public static async Task<DataTable> GetAllProductsInStockAsync() 
 {

  return  await clsProductsInStockData.GetAllProductsInStockAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewProductsInStockAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateProductsInStockAsync(); 

  }

    return false; 
 }



    }
  }