using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsProductsInStockData 
  {

     static clsProductsInStockData(){} 




 public static bool GetProductsInStockByP_Stock_ID(int P_Stock_ID, 
  ref string ItemTitle, ref string Description, ref string Barcode, 
  ref int? CategoryID, ref decimal? SupplierPrice, ref decimal? SalePrice, 
  ref int Quantity, ref string Producer, ref DateTime? ProductionDate, 
  ref DateTime? ExpirationDate, ref int? LeastQuantity, ref int? SupplierID, 
  ref DateTime? MaxReturnDate) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("ProductsInStock_GetProductsInStockByP_Stock_ID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@P_Stock_ID",P_Stock_ID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        ItemTitle = Convert.ToString(reader["ItemTitle"]); 

        if(reader["Description"] ==  DBNull.Value) 
          {

            Description = null; 
          }
        else 
          Description = Convert.ToString(reader["Description"]);  

        if(reader["Barcode"] ==  DBNull.Value) 
          {

            Barcode = null; 
          }
        else 
          Barcode = Convert.ToString(reader["Barcode"]);  

        if(reader["CategoryID"] ==  DBNull.Value) 
          {

            CategoryID = null; 
          }
        else 
          CategoryID = Convert.ToInt32(reader["CategoryID"]);  

        if(reader["SupplierPrice"] ==  DBNull.Value) 
          {

            SupplierPrice = null; 
          }
        else 
          SupplierPrice = Convert.ToDecimal(reader["SupplierPrice"]);  

        if(reader["SalePrice"] ==  DBNull.Value) 
          {

            SalePrice = null; 
          }
        else 
          SalePrice = Convert.ToDecimal(reader["SalePrice"]);  

        Quantity = Convert.ToInt32(reader["Quantity"]); 

        if(reader["Producer"] ==  DBNull.Value) 
          {

            Producer = null; 
          }
        else 
          Producer = Convert.ToString(reader["Producer"]);  

        if(reader["ProductionDate"] ==  DBNull.Value) 
          {

            ProductionDate = null; 
          }
        else 
          ProductionDate = Convert.ToDateTime(reader["ProductionDate"]);  

        if(reader["ExpirationDate"] ==  DBNull.Value) 
          {

            ExpirationDate = null; 
          }
        else 
          ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);  

        if(reader["LeastQuantity"] ==  DBNull.Value) 
          {

            LeastQuantity = null; 
          }
        else 
          LeastQuantity = Convert.ToInt32(reader["LeastQuantity"]);  

        if(reader["SupplierID"] ==  DBNull.Value) 
          {

            SupplierID = null; 
          }
        else 
          SupplierID = Convert.ToInt32(reader["SupplierID"]);  

        if(reader["MaxReturnDate"] ==  DBNull.Value) 
          {

            MaxReturnDate = null; 
          }
        else 
          MaxReturnDate = Convert.ToDateTime(reader["MaxReturnDate"]);  

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


    public static async Task<int> AddNewProductsInStockAsync(int P_Stock_ID, 
  string ItemTitle, string Description, string Barcode, 
  int? CategoryID, decimal? SupplierPrice, decimal? SalePrice, 
  int Quantity, string Producer, DateTime? ProductionDate, 
  DateTime? ExpirationDate, int? LeastQuantity, int? SupplierID, 
  DateTime? MaxReturnDate)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("ProductsInStock_InsertNewProductsInStock",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@P_Stock_ID",P_Stock_ID);

         cmd.Parameters.AddWithValue("@ItemTitle",ItemTitle);

     cmd.Parameters.AddWithValue("@Description",(object)Description ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Barcode",(object)Barcode ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@CategoryID",(object)CategoryID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@SupplierPrice",(object)SupplierPrice ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@SalePrice",(object)SalePrice ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@Quantity",Quantity);

     cmd.Parameters.AddWithValue("@Producer",(object)Producer ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@ProductionDate",(object)ProductionDate ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@ExpirationDate",(object)ExpirationDate ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@LeastQuantity",(object)LeastQuantity ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@SupplierID",(object)SupplierID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@MaxReturnDate",(object)MaxReturnDate ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewP_Stock_ID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newP_Stock_ID = (int)cmd.Parameters["@NewP_Stock_ID"].Value;
        return newP_Stock_ID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateProductsInStockAsync(int P_Stock_ID, 
  string ItemTitle, string Description, string Barcode, 
  int? CategoryID, decimal? SupplierPrice, decimal? SalePrice, 
  int Quantity, string Producer, DateTime? ProductionDate, 
  DateTime? ExpirationDate, int? LeastQuantity, int? SupplierID, 
  DateTime? MaxReturnDate)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("ProductsInStock_UpdateProductsInStock",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@P_Stock_ID",P_Stock_ID);

         cmd.Parameters.AddWithValue("@ItemTitle",ItemTitle);

     cmd.Parameters.AddWithValue("@Description",(object)Description ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Barcode",(object)Barcode ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@CategoryID",(object)CategoryID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@SupplierPrice",(object)SupplierPrice ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@SalePrice",(object)SalePrice ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@Quantity",Quantity);

     cmd.Parameters.AddWithValue("@Producer",(object)Producer ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@ProductionDate",(object)ProductionDate ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@ExpirationDate",(object)ExpirationDate ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@LeastQuantity",(object)LeastQuantity ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@SupplierID",(object)SupplierID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@MaxReturnDate",(object)MaxReturnDate ?? DBNull.Value);
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


    public static async Task<bool> DeleteProductsInStockAsync(int P_Stock_ID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("ProductsInStock_DeleteProductsInStock",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@P_Stock_ID",P_Stock_ID);
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

 public static async Task<bool> IsProductsInStockExistsAsync(int P_Stock_ID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("ProductsInStock_CheckProductsInStockExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@P_Stock_ID",P_Stock_ID);

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

 public static async Task<DataTable> GetAllProductsInStockAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("ProductsInStock_GetAllProductsInStock",connection)) 

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