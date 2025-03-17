using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using SuberMarketDB_DataAccess;


namespace SuberMarketDB_Business 
 {
 public class clsDailyRevenue 
  {
     public enum enMode {AddNew,Update}
     public enMode Mode {get;set;} = enMode.AddNew;

     public int BillRevenueID {get;set;}

     public decimal TotalBillsEarning {get;set;}

     public decimal TotalBillsDeductions {get;set;}

     public int? MonthlyReportID {get;set;}

     public decimal ActualEarning {get;set;}



 public clsDailyRevenue() 
 {
     this.BillRevenueID = default;

     this.TotalBillsEarning = default;

     this.TotalBillsDeductions = default;

     this.MonthlyReportID = default;

     this.ActualEarning = default;

      Mode = enMode.AddNew;
 }

 private clsDailyRevenue(int BillRevenueID, decimal TotalBillsEarning, 
	decimal TotalBillsDeductions, int? MonthlyReportID, decimal ActualEarning)
 {
      this.BillRevenueID = BillRevenueID;

      this.TotalBillsEarning = TotalBillsEarning;

      this.TotalBillsDeductions = TotalBillsDeductions;

      this.MonthlyReportID = MonthlyReportID;

      this.ActualEarning = ActualEarning;

      Mode = enMode.Update;
  }

 private async Task<bool> _AddNewDailyRevenueAsync()
 {
  this.BillRevenueID = await (clsDailyRevenueData.AddNewDailyRevenueAsync
   (this.BillRevenueID, this.TotalBillsEarning, 
	this.TotalBillsDeductions, this.MonthlyReportID, this.ActualEarning));
   return BillRevenueID != 0;
 }

 private async Task<bool> _UpdateDailyRevenueAsync()
 {
   return await clsDailyRevenueData.UpdateDailyRevenueAsync (BillRevenueID, TotalBillsEarning, 
	TotalBillsDeductions, MonthlyReportID, ActualEarning); 
 }

 public static async Task<bool> DeleteDailyRevenueAsync(int BillRevenueID)
 {
   return await clsDailyRevenueData.DeleteDailyRevenueAsync (BillRevenueID); 
 }

 public static clsDailyRevenue Find(int BillRevenueID) 
 {

     decimal TotalBillsEarning = default;
	decimal TotalBillsDeductions = default;int? MonthlyReportID = default;decimal ActualEarning = default;

 

 bool IsFound =  clsDailyRevenueData.GetDailyRevenueByBillRevenueID
               (BillRevenueID, ref TotalBillsEarning, 
	ref TotalBillsDeductions, ref MonthlyReportID, ref ActualEarning);

 if(IsFound) { 
   return new clsDailyRevenue
                   (BillRevenueID, TotalBillsEarning, 
	TotalBillsDeductions, MonthlyReportID, ActualEarning);

}

 return  null;

} 



 public static async Task<bool> IsDailyRevenueExistsAsync(int BillRevenueID) 
 {

  return  await clsDailyRevenueData.IsDailyRevenueExistsAsync(BillRevenueID); 

 }

 public static async Task<DataTable> GetAllDailyRevenueAsync() 
 {

  return  await clsDailyRevenueData.GetAllDailyRevenueAsync(); 

 }

  public async Task<bool> Save() 
 {

   switch(Mode) 
   {

    case enMode.AddNew:
    if( await _AddNewDailyRevenueAsync())
    {
       Mode = enMode.Update; 
       return true;
    }
    else  
      return false;

       case enMode.Update: 
        return await _UpdateDailyRevenueAsync(); 

  }

    return false; 
 }



    }
  }