using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsSalesProductDetailData 
  {

     static clsSalesProductDetailData(){} 




 public static bool GetSalesProductDetailBySaleProductID(int SaleProductID, 
  ref int? SalesBillID, ref decimal UnitPrice, ref int Quantity, 
  ref decimal TotalPrice, ref int? StockID) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SalesProductDetails_GetSalesProductDetailBySaleProductID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@SaleProductID",SaleProductID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["SalesBillID"] ==  DBNull.Value) 
          {

            SalesBillID = null; 
          }
        else 
          SalesBillID = Convert.ToInt32(reader["SalesBillID"]);  

        UnitPrice = Convert.ToDecimal(reader["UnitPrice"]); 

        Quantity = Convert.ToInt32(reader["Quantity"]); 

        TotalPrice = Convert.ToDecimal(reader["TotalPrice"]); 

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


    public static async Task<int> AddNewSalesProductDetailAsync(int SaleProductID, 
  int? SalesBillID, decimal UnitPrice, int Quantity, 
  decimal TotalPrice, int? StockID)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesProductDetails_InsertNewSalesProductDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SaleProductID",SaleProductID);

     cmd.Parameters.AddWithValue("@SalesBillID",(object)SalesBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@UnitPrice",UnitPrice);

         cmd.Parameters.AddWithValue("@Quantity",Quantity);

         cmd.Parameters.AddWithValue("@TotalPrice",TotalPrice);

     cmd.Parameters.AddWithValue("@StockID",(object)StockID ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewSaleProductID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newSaleProductID = (int)cmd.Parameters["@NewSaleProductID"].Value;
        return newSaleProductID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateSalesProductDetailAsync(int SaleProductID, 
  int? SalesBillID, decimal UnitPrice, int Quantity, 
  decimal TotalPrice, int? StockID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesProductDetails_UpdateSalesProductDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SaleProductID",SaleProductID);

     cmd.Parameters.AddWithValue("@SalesBillID",(object)SalesBillID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@UnitPrice",UnitPrice);

         cmd.Parameters.AddWithValue("@Quantity",Quantity);

         cmd.Parameters.AddWithValue("@TotalPrice",TotalPrice);

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


    public static async Task<bool> DeleteSalesProductDetailAsync(int SaleProductID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesProductDetails_DeleteSalesProductDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SaleProductID",SaleProductID);
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

 public static async Task<bool> IsSalesProductDetailExistsAsync(int SaleProductID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("SalesProductDetails_CheckSalesProductDetailExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SaleProductID",SaleProductID);

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

 public static async Task<DataTable> GetAllSalesProductDetailsAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("SalesProductDetails_GetAllSalesProductDetails",connection)) 

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