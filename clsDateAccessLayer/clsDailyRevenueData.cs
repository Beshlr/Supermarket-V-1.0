using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsDailyRevenueData 
  {

     static clsDailyRevenueData(){} 




 public static bool GetDailyRevenueByBillRevenueID(int BillRevenueID, 
  ref decimal TotalBillsEarning, ref decimal TotalBillsDeductions, ref int? MonthlyReportID, 
  ref decimal ActualEarning) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("DailyRevenue_GetDailyRevenueByBillRevenueID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@BillRevenueID",BillRevenueID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        TotalBillsEarning = Convert.ToDecimal(reader["TotalBillsEarning"]); 

        TotalBillsDeductions = Convert.ToDecimal(reader["TotalBillsDeductions"]); 

        if(reader["MonthlyReportID"] ==  DBNull.Value) 
          {

            MonthlyReportID = null; 
          }
        else 
          MonthlyReportID = Convert.ToInt32(reader["MonthlyReportID"]);  

        ActualEarning = Convert.ToDecimal(reader["ActualEarning"]); 

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


    public static async Task<int> AddNewDailyRevenueAsync(int BillRevenueID, 
  decimal TotalBillsEarning, decimal TotalBillsDeductions, int? MonthlyReportID, 
  decimal ActualEarning)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("DailyRevenue_InsertNewDailyRevenue",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@BillRevenueID",BillRevenueID);

         cmd.Parameters.AddWithValue("@TotalBillsEarning",TotalBillsEarning);

         cmd.Parameters.AddWithValue("@TotalBillsDeductions",TotalBillsDeductions);

     cmd.Parameters.AddWithValue("@MonthlyReportID",(object)MonthlyReportID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ActualEarning",ActualEarning);

         SqlParameter outputIdParam = new SqlParameter("@NewBillRevenueID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newBillRevenueID = (int)cmd.Parameters["@NewBillRevenueID"].Value;
        return newBillRevenueID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateDailyRevenueAsync(int BillRevenueID, 
  decimal TotalBillsEarning, decimal TotalBillsDeductions, int? MonthlyReportID, 
  decimal ActualEarning)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("DailyRevenue_UpdateDailyRevenue",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@BillRevenueID",BillRevenueID);

         cmd.Parameters.AddWithValue("@TotalBillsEarning",TotalBillsEarning);

         cmd.Parameters.AddWithValue("@TotalBillsDeductions",TotalBillsDeductions);

     cmd.Parameters.AddWithValue("@MonthlyReportID",(object)MonthlyReportID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ActualEarning",ActualEarning);
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


    public static async Task<bool> DeleteDailyRevenueAsync(int BillRevenueID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("DailyRevenue_DeleteDailyRevenue",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@BillRevenueID",BillRevenueID);
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

 public static async Task<bool> IsDailyRevenueExistsAsync(int BillRevenueID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("DailyRevenue_CheckDailyRevenueExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@BillRevenueID",BillRevenueID);

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

 public static async Task<DataTable> GetAllDailyRevenueAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("DailyRevenue_GetAllDailyRevenue",connection)) 

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