using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsUser 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int UserID {get;set;}

     public int EmployeeID {get;set;}

     public string UserName {get;set;}

     public string Password {get;set;}

     public bool IsActive {get;set;}

     public int? PermissionsID {get;set;}

     public DateTime CreationDate {get;set;}

     public int? CreatedByUserID {get;set;}



 public clsUser() 
 {
     this.UserID = default;

     this.EmployeeID = default;

     this.UserName = default;

     this.Password = default;

     this.IsActive = default;

     this.PermissionsID = default;

     this.CreationDate = default;

     this.CreatedByUserID = default;

      Mode = enMode.AddNew;
 }

 private clsUser(int UserID, int EmployeeID, 
	string UserName, string Password, bool IsActive, 
	int? PermissionsID, DateTime CreationDate, int? CreatedByUserID)
 {
      this.UserID = UserID;

      this.EmployeeID = EmployeeID;

      this.UserName = UserName;

      this.Password = Password;

      this.IsActive = IsActive;

      this.PermissionsID = PermissionsID;

      this.CreationDate = CreationDate;

      this.CreatedByUserID = CreatedByUserID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewUserAsync()
 {
  this.UserID = await (clsUserData.AddNewUserAsync
   (this.UserID, this.EmployeeID, 
	this.UserName, this.Password, this.IsActive, 
	this.PermissionsID, this.CreationDate, this.CreatedByUserID));
   return UserID != 0;
 }

 private async Task<bool> _UpdateUserAsync()
 {
   return await clsUserData.UpdateUserAsync (UserID, EmployeeID, 
	UserName, Password, IsActive, 
	PermissionsID, CreationDate, CreatedByUserID); 
 }

 public static async Task<bool> DeleteUserAsync(int UserID)
 {
   return await clsUserData.DeleteUserAsync (UserID); 
 }

 public static clsUser Find(int UserID) 
 {

     int EmployeeID = default;
	string UserName = default;string Password = default;bool IsActive = default;
	int? PermissionsID = default;DateTime CreationDate = default;int? CreatedByUserID = default;

 

 bool IsFound =  clsUserData.GetUserByUserID
               (UserID, ref EmployeeID, 
	ref UserName, ref Password, ref IsActive, 
	ref PermissionsID, ref CreationDate, ref CreatedByUserID);

 if(IsFound) { 
   return new clsUser
                   (UserID, EmployeeID, 
	UserName, Password, IsActive, 
	PermissionsID, CreationDate, CreatedByUserID);

}

 return  null;

} 



 public static async Task<bool> IsUserExistsAsync(int UserID) 
 {

  return  await clsUserData.IsUserExistsAsync(UserID); 

 }

 public static async Task<DataTable> GetAllUsersAsync() 
 {

  return  await clsUserData.GetAllUsersAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewUserAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateUserAsync(); 

  }

    return false; 
 }



    }
  }