using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsLog 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int LogID {get;set;}

     public int? EmployeeID {get;set;}

     public DateTime ActionDate {get;set;}

     public int? CreatedByUserID {get;set;}

     public string ActionTitle {get;set;}



 public clsLog() 
 {
     this.LogID = default;

     this.EmployeeID = default;

     this.ActionDate = default;

     this.CreatedByUserID = default;

     this.ActionTitle = default;

      Mode = enMode.AddNew;
 }

 private clsLog(int LogID, int? EmployeeID, 
	DateTime ActionDate, int? CreatedByUserID, string ActionTitle)
 {
      this.LogID = LogID;

      this.EmployeeID = EmployeeID;

      this.ActionDate = ActionDate;

      this.CreatedByUserID = CreatedByUserID;

      this.ActionTitle = ActionTitle;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewLogAsync()
 {
  this.LogID = await (clsLogData.AddNewLogAsync
   (this.LogID, this.EmployeeID, 
	this.ActionDate, this.CreatedByUserID, this.ActionTitle));
   return LogID != 0;
 }

 private async Task<bool> _UpdateLogAsync()
 {
   return await clsLogData.UpdateLogAsync (LogID, EmployeeID, 
	ActionDate, CreatedByUserID, ActionTitle); 
 }

 public static async Task<bool> DeleteLogAsync(int LogID)
 {
   return await clsLogData.DeleteLogAsync (LogID); 
 }

 public static clsLog Find(int LogID) 
 {

     int? EmployeeID = default;
	DateTime ActionDate = default;int? CreatedByUserID = default;string ActionTitle = default;

 

 bool IsFound =  clsLogData.GetLogByLogID
               (LogID, ref EmployeeID, 
	ref ActionDate, ref CreatedByUserID, ref ActionTitle);

 if(IsFound) { 
   return new clsLog
                   (LogID, EmployeeID, 
	ActionDate, CreatedByUserID, ActionTitle);

}

 return  null;

} 



 public static async Task<bool> IsLogExistsAsync(int LogID) 
 {

  return  await clsLogData.IsLogExistsAsync(LogID); 

 }

 public static async Task<DataTable> GetAllLogsAsync() 
 {

  return  await clsLogData.GetAllLogsAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewLogAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateLogAsync(); 

  }

    return false; 
 }



    }
  }