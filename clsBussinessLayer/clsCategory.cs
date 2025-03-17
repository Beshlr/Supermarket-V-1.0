using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsCategory 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int CategoryID {get;set;}

     public string CategoryTitle {get;set;}

     public string Description {get;set;}



 public clsCategory() 
 {
     this.CategoryID = default;

     this.CategoryTitle = default;

     this.Description = default;

      Mode = enMode.AddNew;
 }

 private clsCategory(int CategoryID, string CategoryTitle, 
	string Description)
 {
      this.CategoryID = CategoryID;

      this.CategoryTitle = CategoryTitle;

      this.Description = Description;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewCategoryAsync()
 {
  this.CategoryID = await (clsCategoryData.AddNewCategoryAsync
   (this.CategoryID, this.CategoryTitle, 
	this.Description));
   return CategoryID != 0;
 }

 private async Task<bool> _UpdateCategoryAsync()
 {
   return await clsCategoryData.UpdateCategoryAsync (CategoryID, CategoryTitle, 
	Description); 
 }

 public static async Task<bool> DeleteCategoryAsync(int CategoryID)
 {
   return await clsCategoryData.DeleteCategoryAsync (CategoryID); 
 }

 public static clsCategory Find(int CategoryID) 
 {

     string CategoryTitle = default;
	string Description = default;

 

 bool IsFound =  clsCategoryData.GetCategoryByCategoryID
               (CategoryID, ref CategoryTitle, 
	ref Description);

 if(IsFound) { 
   return new clsCategory
                   (CategoryID, CategoryTitle, 
	Description);

}

 return  null;

} 



 public static async Task<bool> IsCategoryExistsAsync(int CategoryID) 
 {

  return  await clsCategoryData.IsCategoryExistsAsync(CategoryID); 

 }

 public static async Task<DataTable> GetAllCategoriesAsync() 
 {

  return  await clsCategoryData.GetAllCategoriesAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewCategoryAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateCategoryAsync(); 

  }

    return false; 
 }



    }
  }