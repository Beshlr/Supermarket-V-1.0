using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsSalesBillData 
  {

     static clsSalesBillData(){} 




 public static bool GetSalesBillBySalesBillID(int SalesBillID, 
  ref int? CustomerID, ref decimal BillPrice, ref int PaymentMethod, 
  ref int? CreatedByUserID, ref DateTime CreationDate, ref bool ReturnAvailabilty, 
  ref int? TodayRevenueBillID) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SalesBill_GetSalesBillBySalesBillID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@SalesBillID",SalesBillID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["CustomerID"] ==  DBNull.Value) 
          {

            CustomerID = null; 
          }
        else 
          CustomerID = Convert.ToInt32(reader["CustomerID"]);  

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

        if(reader["TodayRevenueBillID"] ==  DBNull.Value) 
          {

            TodayRevenueBillID = null; 
          }
        else 
          TodayRevenueBillID = Convert.ToInt32(reader["TodayRevenueBillID"]);  

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


    public static async Task<int> AddNewSalesBillAsync(int SalesBillID, 
  int? CustomerID, decimal BillPrice, int PaymentMethod, 
  int? CreatedByUserID, DateTime CreationDate, bool ReturnAvailabilty, 
  int? TodayRevenueBillID)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesBill_InsertNewSalesBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SalesBillID",SalesBillID);

     cmd.Parameters.AddWithValue("@CustomerID",(object)CustomerID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@PaymentMethod",PaymentMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

         cmd.Parameters.AddWithValue("@ReturnAvailabilty",ReturnAvailabilty);

     cmd.Parameters.AddWithValue("@TodayRevenueBillID",(object)TodayRevenueBillID ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewSalesBillID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newSalesBillID = (int)cmd.Parameters["@NewSalesBillID"].Value;
        return newSalesBillID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateSalesBillAsync(int SalesBillID, 
  int? CustomerID, decimal BillPrice, int PaymentMethod, 
  int? CreatedByUserID, DateTime CreationDate, bool ReturnAvailabilty, 
  int? TodayRevenueBillID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesBill_UpdateSalesBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SalesBillID",SalesBillID);

     cmd.Parameters.AddWithValue("@CustomerID",(object)CustomerID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BillPrice",BillPrice);

         cmd.Parameters.AddWithValue("@PaymentMethod",PaymentMethod);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

         cmd.Parameters.AddWithValue("@ReturnAvailabilty",ReturnAvailabilty);

     cmd.Parameters.AddWithValue("@TodayRevenueBillID",(object)TodayRevenueBillID ?? DBNull.Value);
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


    public static async Task<bool> DeleteSalesBillAsync(int SalesBillID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesBill_DeleteSalesBill",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SalesBillID",SalesBillID);
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

 public static async Task<bool> IsSalesBillExistsAsync(int SalesBillID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SalesBill_CheckSalesBillExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SalesBillID",SalesBillID);

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

 public static async Task<DataTable> GetAllSalesBillAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesBill_GetAllSalesBill",connection)) 

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