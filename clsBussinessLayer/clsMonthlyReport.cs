using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsMonthlyReport 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int ReportID {get;set;}

     public decimal? ElectricityBillAmount {get;set;}

     public decimal? WaterBillAmount {get;set;}

     public decimal? InternetBillAmount {get;set;}

     public decimal? RentBillAmount {get;set;}

     public decimal? PaidSalaries {get;set;}

     public decimal? MonthEarnings {get;set;}

     public decimal? TotalEarning {get;set;}



 public clsMonthlyReport() 
 {
     this.ReportID = default;

     this.ElectricityBillAmount = default;

     this.WaterBillAmount = default;

     this.InternetBillAmount = default;

     this.RentBillAmount = default;

     this.PaidSalaries = default;

     this.MonthEarnings = default;

     this.TotalEarning = default;

      Mode = enMode.AddNew;
 }

 private clsMonthlyReport(int ReportID, decimal? ElectricityBillAmount, 
	decimal? WaterBillAmount, decimal? InternetBillAmount, decimal? RentBillAmount, 
	decimal? PaidSalaries, decimal? MonthEarnings, decimal? TotalEarning)
 {
      this.ReportID = ReportID;

      this.ElectricityBillAmount = ElectricityBillAmount;

      this.WaterBillAmount = WaterBillAmount;

      this.InternetBillAmount = InternetBillAmount;

      this.RentBillAmount = RentBillAmount;

      this.PaidSalaries = PaidSalaries;

      this.MonthEarnings = MonthEarnings;

      this.TotalEarning = TotalEarning;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewMonthlyReportAsync()
 {
  this.ReportID = await (clsMonthlyReportData.AddNewMonthlyReportAsync
   (this.ReportID, this.ElectricityBillAmount, 
	this.WaterBillAmount, this.InternetBillAmount, this.RentBillAmount, 
	this.PaidSalaries, this.MonthEarnings, this.TotalEarning));
   return ReportID != 0;
 }

 private async Task<bool> _UpdateMonthlyReportAsync()
 {
   return await clsMonthlyReportData.UpdateMonthlyReportAsync (ReportID, ElectricityBillAmount, 
	WaterBillAmount, InternetBillAmount, RentBillAmount, 
	PaidSalaries, MonthEarnings, TotalEarning); 
 }

 public static async Task<bool> DeleteMonthlyReportAsync(int ReportID)
 {
   return await clsMonthlyReportData.DeleteMonthlyReportAsync (ReportID); 
 }

 public static clsMonthlyReport Find(int ReportID) 
 {

     decimal? ElectricityBillAmount = default;
	decimal? WaterBillAmount = default;decimal? InternetBillAmount = default;decimal? RentBillAmount = default;
	decimal? PaidSalaries = default;decimal? MonthEarnings = default;decimal? TotalEarning = default;

 

 bool IsFound =  clsMonthlyReportData.GetMonthlyReportByReportID
               (ReportID, ref ElectricityBillAmount, 
	ref WaterBillAmount, ref InternetBillAmount, ref RentBillAmount, 
	ref PaidSalaries, ref MonthEarnings, ref TotalEarning);

 if(IsFound) { 
   return new clsMonthlyReport
                   (ReportID, ElectricityBillAmount, 
	WaterBillAmount, InternetBillAmount, RentBillAmount, 
	PaidSalaries, MonthEarnings, TotalEarning);

}

 return  null;

} 



 public static async Task<bool> IsMonthlyReportExistsAsync(int ReportID) 
 {

  return  await clsMonthlyReportData.IsMonthlyReportExistsAsync(ReportID); 

 }

 public static async Task<DataTable> GetAllMonthlyReportsAsync() 
 {

  return  await clsMonthlyReportData.GetAllMonthlyReportsAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewMonthlyReportAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateMonthlyReportAsync(); 

  }

    return false; 
 }



    }
  }