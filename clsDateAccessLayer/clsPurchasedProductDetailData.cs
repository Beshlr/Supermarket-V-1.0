using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsPurchasedProductDetailData 
  {

     static clsPurchasedProductDetailData(){} 




 public static bool GetPurchasedProductDetailByProductID(int ProductID, 
  ref int? BillID, ref string Description, ref string Producer, 
  ref int BoxQuantity, ref int ItemsQuantityInBox, ref int BoxPrice, 
  ref int? StockID) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("PurchasedProductDetails_GetPurchasedProductDetailByProductID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@ProductID",ProductID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["BillID"] ==  DBNull.Value) 
          {

            BillID = null; 
          }
        else 
          BillID = Convert.ToInt32(reader["BillID"]);  

        if(reader["Description"] ==  DBNull.Value) 
          {

            Description = null; 
          }
        else 
          Description = Convert.ToString(reader["Description"]);  

        if(reader["Producer"] ==  DBNull.Value) 
          {

            Producer = null; 
          }
        else 
          Producer = Convert.ToString(reader["Producer"]);  

        BoxQuantity = Convert.ToInt32(reader["BoxQuantity"]); 

        ItemsQuantityInBox = Convert.ToInt32(reader["ItemsQuantityInBox"]); 

        BoxPrice = Convert.ToInt32(reader["BoxPrice"]); 

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


    public static async Task<int> AddNewPurchasedProductDetailAsync(int ProductID, 
  int? BillID, string Description, string Producer, 
  int BoxQuantity, int ItemsQuantityInBox, int BoxPrice, 
  int? StockID)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchasedProductDetails_InsertNewPurchasedProductDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ProductID",ProductID);

     cmd.Parameters.AddWithValue("@BillID",(object)BillID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Description",(object)Description ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Producer",(object)Producer ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BoxQuantity",BoxQuantity);

         cmd.Parameters.AddWithValue("@ItemsQuantityInBox",ItemsQuantityInBox);

         cmd.Parameters.AddWithValue("@BoxPrice",BoxPrice);

     cmd.Parameters.AddWithValue("@StockID",(object)StockID ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewProductID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newProductID = (int)cmd.Parameters["@NewProductID"].Value;
        return newProductID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdatePurchasedProductDetailAsync(int ProductID, 
  int? BillID, string Description, string Producer, 
  int BoxQuantity, int ItemsQuantityInBox, int BoxPrice, 
  int? StockID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchasedProductDetails_UpdatePurchasedProductDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ProductID",ProductID);

     cmd.Parameters.AddWithValue("@BillID",(object)BillID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Description",(object)Description ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Producer",(object)Producer ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@BoxQuantity",BoxQuantity);

         cmd.Parameters.AddWithValue("@ItemsQuantityInBox",ItemsQuantityInBox);

         cmd.Parameters.AddWithValue("@BoxPrice",BoxPrice);

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


    public static async Task<bool> DeletePurchasedProductDetailAsync(int ProductID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchasedProductDetails_DeletePurchasedProductDetail",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ProductID",ProductID);
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

 public static async Task<bool> IsPurchasedProductDetailExistsAsync(int ProductID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("PurchasedProductDetails_CheckPurchasedProductDetailExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@ProductID",ProductID);

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

 public static async Task<DataTable> GetAllPurchasedProductDetailsAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("PurchasedProductDetails_GetAllPurchasedProductDetails",connection)) 

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