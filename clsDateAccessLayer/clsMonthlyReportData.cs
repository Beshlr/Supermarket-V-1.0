using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsMonthlyReportData 
  {

     static clsMonthlyReportData(){} 




 public static bool GetMonthlyReportByReportID(int ReportID, 
  ref decimal? ElectricityBillAmount, ref decimal? WaterBillAmount, ref decimal? InternetBillAmount, 
  ref decimal? RentBillAmount, ref decimal? PaidSalaries, ref decimal? MonthEarnings, 
  ref decimal? TotalEarning) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("MonthlyReports_GetMonthlyReportByReportID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@ReportID",ReportID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["ElectricityBillAmount"] ==  DBNull.Value) 
          {

            ElectricityBillAmount = null; 
          }
        else 
          ElectricityBillAmount = Convert.ToDecimal(reader["ElectricityBillAmount"]);  

        if(reader["WaterBillAmount"] ==  DBNull.Value) 
          {

            WaterBillAmount = null; 
          }
        else 
          WaterBillAmount = Convert.ToDecimal(reader["WaterBillAmount"]);  

        if(reader["InternetBillAmount"] ==  DBNull.Value) 
          {

            InternetBillAmount = null; 
          }
        else 
          InternetBillAmount = Convert.ToDecimal(reader["InternetBillAmount"]);  

        if(reader["RentBillAmount"] ==  DBNull.Value) 
          {

            RentBillAmount = null; 
          }
        else 
          RentBillAmount = Convert.ToDecimal(reader["RentBillAmount"]);  

        if(reader["PaidSalaries"] ==  DBNull.Value) 
          {

            PaidSalaries = null; 
          }
        else 
          PaidSalaries = Convert.ToDecimal(reader["PaidSalaries"]);  

        if(reader["MonthEarnings"] ==  DBNull.Value) 
          {

            MonthEarnings = null; 
          }
        else 
          MonthEarnings = Convert.ToDecimal(reader["MonthEarnings"]);  

        if(reader["TotalEarning"] ==  DBNull.Value) 
          {

            TotalEarning = null; 
          }
        else 
          TotalEarning = Convert.ToDecimal(reader["TotalEarning"]);  

       IsFound = true;
    }
     }
     }

   } 

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    IsFound = false;
   }

     return IsFound;

   }


    public static async Task<int> AddNewMonthlyReportAsync(int ReportID, 
  decimal? ElectricityBillAmount, decimal? WaterBillAmount, decimal? InternetBillAmount, 
  decimal? RentBillAmount, decimal? PaidSalaries, decimal? MonthEarnings, 
  decimal? TotalEarning)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("MonthlyReports_InsertNewMonthlyReport",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReportID",ReportID);

     cmd.Parameters.AddWithValue("@ElectricityBillAmount",(object)ElectricityBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@WaterBillAmount",(object)WaterBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@InternetBillAmount",(object)InternetBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@RentBillAmount",(object)RentBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@PaidSalaries",(object)PaidSalaries ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@MonthEarnings",(object)MonthEarnings ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@TotalEarning",(object)TotalEarning ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewReportID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newReportID = (int)cmd.Parameters["@NewReportID"].Value;
        return newReportID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateMonthlyReportAsync(int ReportID, 
  decimal? ElectricityBillAmount, decimal? WaterBillAmount, decimal? InternetBillAmount, 
  decimal? RentBillAmount, decimal? PaidSalaries, decimal? MonthEarnings, 
  decimal? TotalEarning)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("MonthlyReports_UpdateMonthlyReport",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReportID",ReportID);

     cmd.Parameters.AddWithValue("@ElectricityBillAmount",(object)ElectricityBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@WaterBillAmount",(object)WaterBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@InternetBillAmount",(object)InternetBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@RentBillAmount",(object)RentBillAmount ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@PaidSalaries",(object)PaidSalaries ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@MonthEarnings",(object)MonthEarnings ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@TotalEarning",(object)TotalEarning ?? DBNull.Value);
         IsRowsAffected =  (await cmd.ExecuteNonQueryAsync() > 0);
       
         

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    IsRowsAffected = false;
   }
        return IsRowsAffected;
   }


    public static async Task<bool> DeleteMonthlyReportAsync(int ReportID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("MonthlyReports_DeleteMonthlyReport",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReportID",ReportID);
         IsRowsAffected =  (await cmd.ExecuteNonQueryAsync() > 0);
         
         

     }

    }

    }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    IsRowsAffected = false;
    }
        return IsRowsAffected;
     }

 public static async Task<bool> IsMonthlyReportExistsAsync(int ReportID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("MonthlyReports_CheckMonthlyReportExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReportID",ReportID);

         SqlParameter returnParameter = new SqlParameter("@ReturnVal",SqlDbType.Int)
         {
                      Direction = ParameterDirection.ReturnValue
         };

         cmd.Parameters.Add(returnParameter);
         await cmd.ExecuteNonQueryAsync();

     IsFound = (int)returnParameter.Value == 1; 



     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    IsFound = false;
   }
        return IsFound;
   }

 public static async Task<DataTable> GetAllMonthlyReportsAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("MonthlyReports_GetAllMonthlyReports",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

        using (SqlDataReader reader =  await cmd.ExecuteReaderAsync()) 

      {

   if(reader.HasRows)
    dt.Load(reader);

   else
    dt = null;

        }

     }

    }

    }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    dt = null;
    }
        return dt;
      }
     }
   }