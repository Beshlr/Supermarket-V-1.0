using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsPayroll 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int EmployeeID {get;set;}

     public int EmployeeID {get;set;}

     public decimal BasicSalary {get;set;}

     public decimal? Bonus {get;set;}

     public decimal? Deductions {get;set;}

     public decimal NetSalary {get;set;}

     public DateTime PayRollDate {get;set;}

     public bool IsActive {get;set;}



 public clsPayroll() 
 {
     this.EmployeeID = default;

     this.EmployeeID = default;

     this.BasicSalary = default;

     this.Bonus = default;

     this.Deductions = default;

     this.NetSalary = default;

     this.PayRollDate = default;

     this.IsActive = default;

      Mode = enMode.AddNew;
 }

 private clsPayroll(int EmployeeID, int EmployeeID, 
	decimal BasicSalary, decimal? Bonus, decimal? Deductions, 
	decimal NetSalary, DateTime PayRollDate, bool IsActive)
 {
      this.EmployeeID = EmployeeID;

      this.EmployeeID = EmployeeID;

      this.BasicSalary = BasicSalary;

      this.Bonus = Bonus;

      this.Deductions = Deductions;

      this.NetSalary = NetSalary;

      this.PayRollDate = PayRollDate;

      this.IsActive = IsActive;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewPayrollAsync()
 {
  this.EmployeeID = await (clsPayrollData.AddNewPayrollAsync
   (this.EmployeeID, this.EmployeeID, 
	this.BasicSalary, this.Bonus, this.Deductions, 
	this.NetSalary, this.PayRollDate, this.IsActive));
   return EmployeeID != 0;
 }

 private async Task<bool> _UpdatePayrollAsync()
 {
   return await clsPayrollData.UpdatePayrollAsync (EmployeeID, EmployeeID, 
	BasicSalary, Bonus, Deductions, 
	NetSalary, PayRollDate, IsActive); 
 }

 public static async Task<bool> DeletePayrollAsync(int EmployeeID)
 {
   return await clsPayrollData.DeletePayrollAsync (EmployeeID); 
 }

 public static clsPayroll Find(int EmployeeID) 
 {

     
	decimal BasicSalary = default;decimal? Bonus = default;decimal? Deductions = default;
	decimal NetSalary = default;DateTime PayRollDate = default;bool IsActive = default;

 

 bool IsFound =  clsPayrollData.GetPayrollByEmployeeID
               (EmployeeID, EmployeeID, 
	ref BasicSalary, ref Bonus, ref Deductions, 
	ref NetSalary, ref PayRollDate, ref IsActive);

 if(IsFound) { 
   return new clsPayroll
                   (EmployeeID, EmployeeID, 
	BasicSalary, Bonus, Deductions, 
	NetSalary, PayRollDate, IsActive);

}

 return  null;

} 



 public static async Task<bool> IsPayrollExistsAsync(int EmployeeID) 
 {

  return  await clsPayrollData.IsPayrollExistsAsync(EmployeeID); 

 }

 public static async Task<DataTable> GetAllPayrollAsync() 
 {

  return  await clsPayrollData.GetAllPayrollAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewPayrollAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdatePayrollAsync(); 

  }

    return false; 
 }



    }
  }