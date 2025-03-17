using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsSuppliersReturnDetailData 
  {

     static clsSuppliersReturnDetailData(){} 




 public static bool GetSuppliersReturnDetailByReturnDetailsID(int ReturnDetailsID, 
  ref int? ReturnBillID, ref decimal TotalRefund, ref decimal UnitPrice, 
  ref int? StockID) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnDetails_GetSuppliersReturnDetailByReturnDetailsID",connection)) 

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


    public static async Task<int> AddNewSuppliersReturnDetailAsync(int ReturnDetailsID, 
  int? ReturnBillID, decimal TotalRefund, decimal UnitPrice, 
  int? StockID)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnDetails_InsertNewSuppliersReturnDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnDetailsID",ReturnDetailsID);

     cmd.Parameters.AddWithValue("@ReturnBillID",(object)ReturnBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@TotalRefund",TotalRefund);

         cmd.Parameters.AddWithValue("@UnitPrice",UnitPrice);

     cmd.Parameters.AddWithValue("@StockID",(object)StockID ?? DBNull.Value);

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


    public static async Task<bool> UpdateSuppliersReturnDetailAsync(int ReturnDetailsID, 
  int? ReturnBillID, decimal TotalRefund, decimal UnitPrice, 
  int? StockID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnDetails_UpdateSuppliersReturnDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnDetailsID",ReturnDetailsID);

     cmd.Parameters.AddWithValue("@ReturnBillID",(object)ReturnBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@TotalRefund",TotalRefund);

         cmd.Parameters.AddWithValue("@UnitPrice",UnitPrice);

     cmd.Parameters.AddWithValue("@StockID",(object)StockID ?? DBNull.Value);
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


    public static async Task<bool> DeleteSuppliersReturnDetailAsync(int ReturnDetailsID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnDetails_DeleteSuppliersReturnDetail",connection)) 

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

 public static async Task<bool> IsSuppliersReturnDetailExistsAsync(int ReturnDetailsID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnDetails_CheckSuppliersReturnDetailExists",connection)) 

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

 public static async Task<DataTable> GetAllSuppliersReturnDetailsAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnDetails_GetAllSuppliersReturnDetails",connection)) 

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