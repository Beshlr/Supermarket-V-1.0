using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsSuppliersReturnBillData 
  {

     static clsSuppliersReturnBillData(){} 




 public static bool GetSuppliersReturnBillByReturnBillID(int ReturnBillID, 
  ref int? PurchaseBillID, ref decimal BillPrice, ref int ReturnMethod, 
  ref int? CreatedByUserID, ref DateTime ReturnDate) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnBill_GetSuppliersReturnBillByReturnBillID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["PurchaseBillID"] ==  DBNull.Value) 
          {

            PurchaseBillID = null; 
          }
        else 
          PurchaseBillID = Convert.ToInt32(reader["PurchaseBillID"]);  

        BillPrice = Convert.ToDecimal(reader["BillPrice"]); 

        ReturnMethod = Convert.ToInt32(reader["ReturnMethod"]); 

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

        ReturnDate = Convert.ToDateTime(reader["ReturnDate"]); 

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


    public static async Task<int> AddNewSuppliersReturnBillAsync(int ReturnBillID, 
  int? PurchaseBillID, decimal BillPrice, int ReturnMethod, 
  int? CreatedByUserID, DateTime ReturnDate)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnBill_InsertNewSuppliersReturnBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);

     cmd.Parameters.AddWithValue("@PurchaseBillID",(object)PurchaseBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@ReturnMethod",ReturnMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ReturnDate",ReturnDate);

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


    public static async Task<bool> UpdateSuppliersReturnBillAsync(int ReturnBillID, 
  int? PurchaseBillID, decimal BillPrice, int ReturnMethod, 
  int? CreatedByUserID, DateTime ReturnDate)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnBill_UpdateSuppliersReturnBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ReturnBillID",ReturnBillID);

     cmd.Parameters.AddWithValue("@PurchaseBillID",(object)PurchaseBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@ReturnMethod",ReturnMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ReturnDate",ReturnDate);
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


    public static async Task<bool> DeleteSuppliersReturnBillAsync(int ReturnBillID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnBill_DeleteSuppliersReturnBill",connection)) 

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

 public static async Task<bool> IsSuppliersReturnBillExistsAsync(int ReturnBillID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnBill_CheckSuppliersReturnBillExists",connection)) 

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

 public static async Task<DataTable> GetAllSuppliersReturnBillAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SuppliersReturnBill_GetAllSuppliersReturnBill",connection)) 

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