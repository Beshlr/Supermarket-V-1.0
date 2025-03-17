using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsEmployee 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int EmployeeID {get;set;}

     public int PersonID {get;set;}

     public string NationalNo {get;set;}

     public bool IsActive {get;set;}

     public DateTime CreationDate {get;set;}

     public string JobTitle {get;set;}

     public int CreatedByUserID {get;set;}

     public decimal Salary {get;set;}

     public decimal? Fines {get;set;}

     public decimal NetSalary {get;set;}

     public DateTime HiringDate {get;set;}

     public int? ShiftID {get;set;}



 public clsEmployee() 
 {
     this.EmployeeID = default;

     this.PersonID = default;

     this.NationalNo = default;

     this.IsActive = default;

     this.CreationDate = default;

     this.JobTitle = default;

     this.CreatedByUserID = default;

     this.Salary = default;

     this.Fines = default;

     this.NetSalary = default;

     this.HiringDate = default;

     this.ShiftID = default;

      Mode = enMode.AddNew;
 }

 private clsEmployee(int EmployeeID, int PersonID, 
	string NationalNo, bool IsActive, DateTime CreationDate, 
	string JobTitle, int CreatedByUserID, decimal Salary, 
	decimal? Fines, decimal NetSalary, DateTime HiringDate, 
	int? ShiftID)
 {
      this.EmployeeID = EmployeeID;

      this.PersonID = PersonID;

      this.NationalNo = NationalNo;

      this.IsActive = IsActive;

      this.CreationDate = CreationDate;

      this.JobTitle = JobTitle;

      this.CreatedByUserID = CreatedByUserID;

      this.Salary = Salary;

      this.Fines = Fines;

      this.NetSalary = NetSalary;

      this.HiringDate = HiringDate;

      this.ShiftID = ShiftID;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewEmployeeAsync()
 {
  this.EmployeeID = await (clsEmployeeData.AddNewEmployeeAsync
   (this.EmployeeID, this.PersonID, 
	this.NationalNo, this.IsActive, this.CreationDate, 
	this.JobTitle, this.CreatedByUserID, this.Salary, 
	this.Fines, this.NetSalary, this.HiringDate, 
	this.ShiftID));
   return EmployeeID != 0;
 }

 private async Task<bool> _UpdateEmployeeAsync()
 {
   return await clsEmployeeData.UpdateEmployeeAsync (EmployeeID, PersonID, 
	NationalNo, IsActive, CreationDate, 
	JobTitle, CreatedByUserID, Salary, 
	Fines, NetSalary, HiringDate, 
	ShiftID); 
 }

 public static async Task<bool> DeleteEmployeeAsync(int EmployeeID)
 {
   return await clsEmployeeData.DeleteEmployeeAsync (EmployeeID); 
 }

 public static clsEmployee Find(int EmployeeID) 
 {

     int PersonID = default;
	string NationalNo = default;bool IsActive = default;DateTime CreationDate = default;
	string JobTitle = default;int CreatedByUserID = default;decimal Salary = default;
	decimal? Fines = default;decimal NetSalary = default;DateTime HiringDate = default;
	int? ShiftID = default;

 

 bool IsFound =  clsEmployeeData.GetEmployeeByEmployeeID
               (EmployeeID, ref PersonID, 
	ref NationalNo, ref IsActive, ref CreationDate, 
	ref JobTitle, ref CreatedByUserID, ref Salary, 
	ref Fines, ref NetSalary, ref HiringDate, 
	ref ShiftID);

 if(IsFound) { 
   return new clsEmployee
                   (EmployeeID, PersonID, 
	NationalNo, IsActive, CreationDate, 
	JobTitle, CreatedByUserID, Salary, 
	Fines, NetSalary, HiringDate, 
	ShiftID);

}

 return  null;

} 



 public static async Task<bool> IsEmployeeExistsAsync(int EmployeeID) 
 {

  return  await clsEmployeeData.IsEmployeeExistsAsync(EmployeeID); 

 }

 public static async Task<DataTable> GetAllEmployeesAsync() 
 {

  return  await clsEmployeeData.GetAllEmployeesAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewEmployeeAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateEmployeeAsync(); 

  }

    return false; 
 }



    }
  }