using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsShift 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int ShiftID {get;set;}

     public int EmpNum {get;set;}

     public DateTime StartShift {get;set;}

     public DateTime EndShift {get;set;}



 public clsShift() 
 {
     this.ShiftID = default;

     this.EmpNum = default;

     this.StartShift = default;

     this.EndShift = default;

      Mode = enMode.AddNew;
 }

 private clsShift(int ShiftID, int EmpNum, 
	DateTime StartShift, DateTime EndShift)
 {
      this.ShiftID = ShiftID;

      this.EmpNum = EmpNum;

      this.StartShift = StartShift;

      this.EndShift = EndShift;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewShiftAsync()
 {
  this.ShiftID = await (clsShiftData.AddNewShiftAsync
   (this.ShiftID, this.EmpNum, 
	this.StartShift, this.EndShift));
   return ShiftID != 0;
 }

 private async Task<bool> _UpdateShiftAsync()
 {
   return await clsShiftData.UpdateShiftAsync (ShiftID, EmpNum, 
	StartShift, EndShift); 
 }

 public static async Task<bool> DeleteShiftAsync(int ShiftID)
 {
   return await clsShiftData.DeleteShiftAsync (ShiftID); 
 }

 public static clsShift Find(int ShiftID) 
 {

     int EmpNum = default;
	DateTime StartShift = default;DateTime EndShift = default;

 

 bool IsFound =  clsShiftData.GetShiftByShiftID
               (ShiftID, ref EmpNum, 
	ref StartShift, ref EndShift);

 if(IsFound) { 
   return new clsShift
                   (ShiftID, EmpNum, 
	StartShift, EndShift);

}

 return  null;

} 



 public static async Task<bool> IsShiftExistsAsync(int ShiftID) 
 {

  return  await clsShiftData.IsShiftExistsAsync(ShiftID); 

 }

 public static async Task<DataTable> GetAllShiftAsync() 
 {

  return  await clsShiftData.GetAllShiftAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewShiftAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateShiftAsync(); 

  }

    return false; 
 }



    }
  }