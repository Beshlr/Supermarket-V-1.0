using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsCustomersReturnBillData 
  {

     static clsCustomersReturnBillData(){} 




 public static bool GetCustomersReturnBillByReturnBillID(int ReturnBillID, 
  ref int? SaleBillID, ref decimal BillPrice, ref int ReturnMethod, 
  ref int? CreatedByUserID, ref DateTime ReturnDate, ref decimal TotalRefundPrice, 
  ref string Description) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("CustomersReturnBill_GetCustomersReturnBillByReturnBillID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["SaleBillID"] ==  DBNull.Value) 
          {

            SaleBillID = null; 
          }
        else 
          SaleBillID = Convert.ToInt32(reader["SaleBillID"]);  

        BillPrice = Convert.ToDecimal(reader["BillPrice"]); 

        ReturnMethod = Convert.ToInt32(reader["ReturnMethod"]); 

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

        ReturnDate = Convert.ToDateTime(reader["ReturnDate"]); 

        TotalRefundPrice = Convert.ToDecimal(reader["TotalRefundPrice"]); 

        if(reader["Description"] ==  DBNull.Value) 
          {

            Description = null; 
          }
        else 
          Description = Convert.ToString(reader["Description"]);  

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


    public static async Task<int> AddNewCustomersReturnBillAsync(int ReturnBillID, 
  int? SaleBillID, decimal BillPrice, int ReturnMethod, 
  int? CreatedByUserID, DateTime ReturnDate, decimal TotalRefundPrice, 
  string Description)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnBill_InsertNewCustomersReturnBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);

     cmd.Parameters.AddWithValue("@SaleBillID",(object)SaleBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@ReturnMethod",ReturnMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ReturnDate",ReturnDate);

         cmd.Parameters.AddWithValue("@TotalRefundPrice",TotalRefundPrice);

     cmd.Parameters.AddWithValue("@Description",(object)Description ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewReturnBillID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newReturnBillID = (int)cmd.Parameters["@NewReturnBillID"].Value;
        return newReturnBillID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateCustomersReturnBillAsync(int ReturnBillID, 
  int? SaleBillID, decimal BillPrice, int ReturnMethod, 
  int? CreatedByUserID, DateTime ReturnDate, decimal TotalRefundPrice, 
  string Description)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnBill_UpdateCustomersReturnBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);

     cmd.Parameters.AddWithValue("@SaleBillID",(object)SaleBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@ReturnMethod",ReturnMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ReturnDate",ReturnDate);

         cmd.Parameters.AddWithValue("@TotalRefundPrice",TotalRefundPrice);

     cmd.Parameters.AddWithValue("@Description",(object)Description ?? DBNull.Value);
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


    public static async Task<bool> DeleteCustomersReturnBillAsync(int ReturnBillID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnBill_DeleteCustomersReturnBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);
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

 public static async Task<bool> IsCustomersReturnBillExistsAsync(int ReturnBillID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("CustomersReturnBill_CheckCustomersReturnBillExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);

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

 public static async Task<DataTable> GetAllCustomersReturnBillAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnBill_GetAllCustomersReturnBill",connection)) 

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