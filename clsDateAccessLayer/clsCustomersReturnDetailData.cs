using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsCustomersReturnDetailData 
  {

     static clsCustomersReturnDetailData(){} 




 public static bool GetCustomersReturnDetailByReturnDetailsID(int ReturnDetailsID, 
  ref int? ReturnBillID, ref decimal TotalRefund, ref decimal UnitPrice, 
  ref int? StockID, ref int Quantity) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("CustomersReturnDetails_GetCustomersReturnDetailByReturnDetailsID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@ReturnDetailsID",ReturnDetailsID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["ReturnBillID"] ==  DBNull.Value) 
          {

            ReturnBillID = null; 
          }
        else 
          ReturnBillID = Convert.ToInt32(reader["ReturnBillID"]);  

        TotalRefund = Convert.ToDecimal(reader["TotalRefund"]); 

        UnitPrice = Convert.ToDecimal(reader["UnitPrice"]); 

        if(reader["StockID"] ==  DBNull.Value) 
          {

            StockID = null; 
          }
        else 
          StockID = Convert.ToInt32(reader["StockID"]);  

        Quantity = Convert.ToInt32(reader["Quantity"]); 

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


    public static async Task<int> AddNewCustomersReturnDetailAsync(int ReturnDetailsID, 
  int? ReturnBillID, decimal TotalRefund, decimal UnitPrice, 
  int? StockID, int Quantity)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnDetails_InsertNewCustomersReturnDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnDetailsID",ReturnDetailsID);

     cmd.Parameters.AddWithValue("@ReturnBillID",(object)ReturnBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@TotalRefund",TotalRefund);

         cmd.Parameters.AddWithValue("@UnitPrice",UnitPrice);

     cmd.Parameters.AddWithValue("@StockID",(object)StockID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@Quantity",Quantity);

         SqlParameter outputIdParam = new SqlParameter("@NewReturnDetailsID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newReturnDetailsID = (int)cmd.Parameters["@NewReturnDetailsID"].Value;
        return newReturnDetailsID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateCustomersReturnDetailAsync(int ReturnDetailsID, 
  int? ReturnBillID, decimal TotalRefund, decimal UnitPrice, 
  int? StockID, int Quantity)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnDetails_UpdateCustomersReturnDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnDetailsID",ReturnDetailsID);

     cmd.Parameters.AddWithValue("@ReturnBillID",(object)ReturnBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@TotalRefund",TotalRefund);

         cmd.Parameters.AddWithValue("@UnitPrice",UnitPrice);

     cmd.Parameters.AddWithValue("@StockID",(object)StockID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@Quantity",Quantity);
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


    public static async Task<bool> DeleteCustomersReturnDetailAsync(int ReturnDetailsID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnDetails_DeleteCustomersReturnDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnDetailsID",ReturnDetailsID);
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

 public static async Task<bool> IsCustomersReturnDetailExistsAsync(int ReturnDetailsID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("CustomersReturnDetails_CheckCustomersReturnDetailExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnDetailsID",ReturnDetailsID);

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

 public static async Task<DataTable> GetAllCustomersReturnDetailsAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("CustomersReturnDetails_GetAllCustomersReturnDetails",connection)) 

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