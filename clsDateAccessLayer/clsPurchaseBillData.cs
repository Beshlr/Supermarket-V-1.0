using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsPurchaseBillData 
  {

     static clsPurchaseBillData(){} 




 public static bool GetPurchaseBillByPurchBillID(int PurchBillID, 
  ref int? SupplierID, ref decimal BillPrice, ref int PaymentMethod, 
  ref int? CreatedByUserID, ref DateTime CreationDate, ref bool ReturnAvailabilty, 
  ref bool IsEMP, ref decimal ActualEarnings, ref int? TodayRevenueID) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("PurchaseBill_GetPurchaseBillByPurchBillID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@PurchBillID",PurchBillID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["SupplierID"] ==  DBNull.Value) 
          {

            SupplierID = null; 
          }
        else 
          SupplierID = Convert.ToInt32(reader["SupplierID"]);  

        BillPrice = Convert.ToDecimal(reader["BillPrice"]); 

        PaymentMethod = Convert.ToInt32(reader["PaymentMethod"]); 

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

        CreationDate = Convert.ToDateTime(reader["CreationDate"]); 

        ReturnAvailabilty = Convert.ToBoolean(reader["ReturnAvailabilty"]); 

        IsEMP = Convert.ToBoolean(reader["IsEMP"]); 

        ActualEarnings = Convert.ToDecimal(reader["ActualEarnings"]); 

        if(reader["TodayRevenueID"] ==  DBNull.Value) 
          {

            TodayRevenueID = null; 
          }
        else 
          TodayRevenueID = Convert.ToInt32(reader["TodayRevenueID"]);  

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


    public static async Task<int> AddNewPurchaseBillAsync(int PurchBillID, 
  int? SupplierID, decimal BillPrice, int PaymentMethod, 
  int? CreatedByUserID, DateTime CreationDate, bool ReturnAvailabilty, 
  bool IsEMP, decimal ActualEarnings, int? TodayRevenueID)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchaseBill_InsertNewPurchaseBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PurchBillID",PurchBillID);

     cmd.Parameters.AddWithValue("@SupplierID",(object)SupplierID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@PaymentMethod",PaymentMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

         cmd.Parameters.AddWithValue("@ReturnAvailabilty",ReturnAvailabilty);

         cmd.Parameters.AddWithValue("@IsEMP",IsEMP);

         cmd.Parameters.AddWithValue("@ActualEarnings",ActualEarnings);

     cmd.Parameters.AddWithValue("@TodayRevenueID",(object)TodayRevenueID ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewPurchBillID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newPurchBillID = (int)cmd.Parameters["@NewPurchBillID"].Value;
        return newPurchBillID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdatePurchaseBillAsync(int PurchBillID, 
  int? SupplierID, decimal BillPrice, int PaymentMethod, 
  int? CreatedByUserID, DateTime CreationDate, bool ReturnAvailabilty, 
  bool IsEMP, decimal ActualEarnings, int? TodayRevenueID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchaseBill_UpdatePurchaseBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PurchBillID",PurchBillID);

     cmd.Parameters.AddWithValue("@SupplierID",(object)SupplierID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@PaymentMethod",PaymentMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

         cmd.Parameters.AddWithValue("@ReturnAvailabilty",ReturnAvailabilty);

         cmd.Parameters.AddWithValue("@IsEMP",IsEMP);

         cmd.Parameters.AddWithValue("@ActualEarnings",ActualEarnings);

     cmd.Parameters.AddWithValue("@TodayRevenueID",(object)TodayRevenueID ?? DBNull.Value);
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


    public static async Task<bool> DeletePurchaseBillAsync(int PurchBillID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchaseBill_DeletePurchaseBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PurchBillID",PurchBillID);
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

 public static async Task<bool> IsPurchaseBillExistsAsync(int PurchBillID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("PurchaseBill_CheckPurchaseBillExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PurchBillID",PurchBillID);

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

 public static async Task<DataTable> GetAllPurchaseBillAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchaseBill_GetAllPurchaseBill",connection)) 

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