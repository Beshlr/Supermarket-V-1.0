using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsPermission 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int PersmisionID {get;set;}



 public clsPermission() 
 {
     this.PersmisionID = default;

      Mode = enMode.AddNew;
 }

 private clsPermission(int PersmisionID)
 {
      this.PersmisionID = PersmisionID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewPermissionAsync()
 {
  this.PersmisionID = await (clsPermissionData.AddNewPermissionAsync
   (this.PersmisionID));
   return PersmisionID != 0;
 }

 private async Task<bool> _UpdatePermissionAsync()
 {
   return await clsPermissionData.UpdatePermissionAsync (PersmisionID); 
 }

 public static async Task<bool> DeletePermissionAsync(int PersmisionID)
 {
   return await clsPermissionData.DeletePermissionAsync (PersmisionID); 
 }

 public static clsPermission Find(int PersmisionID) 
 {

     int PersmisionID = default;

 

 bool IsFound =  clsPermissionData.GetPermissionByPersmisionID
               (ref PersmisionID);

 if(IsFound) { 
   return new clsPermission
                   (PersmisionID);

}

 return  null;

} 



 public static async Task<bool> IsPermissionExistsAsync(int PersmisionID) 
 {

  return  await clsPermissionData.IsPermissionExistsAsync(PersmisionID); 

 }

 public static async Task<DataTable> GetAllPermissionsAsync() 
 {

  return  await clsPermissionData.GetAllPermissionsAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewPermissionAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdatePermissionAsync(); 

  }

    return false; 
 }



    }
  }