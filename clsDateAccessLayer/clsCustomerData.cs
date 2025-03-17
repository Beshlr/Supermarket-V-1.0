using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsCustomerData 
  {

     static clsCustomerData(){} 




 public static bool GetCustomerByCustomarID(int CustomarID, 
  ref int? PersonID, ref DateTime CreationDate, ref int? CreatedByUserID, 
  ref decimal? Balance, ref double? DiscountPoints) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Customers_GetCustomerByCustomarID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@CustomarID",CustomarID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["PersonID"] ==  DBNull.Value) 
          {

            PersonID = null; 
          }
        else 
          PersonID = Convert.ToInt32(reader["PersonID"]);  

        CreationDate = Convert.ToDateTime(reader["CreationDate"]); 

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

        if(reader["Balance"] ==  DBNull.Value) 
          {

            Balance = null; 
          }
        else 
          Balance = Convert.ToDecimal(reader["Balance"]);  

        if(reader["DiscountPoints"] ==  DBNull.Value) 
          {

            DiscountPoints = null; 
          }
        else 
          DiscountPoints = Convert.ToDouble(reader["DiscountPoints"]);  

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


    public static async Task<int> AddNewCustomerAsync(int CustomarID, 
  int? PersonID, DateTime CreationDate, int? CreatedByUserID, 
  decimal? Balance, double? DiscountPoints)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Customers_InsertNewCustomer",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@CustomarID",CustomarID);

     cmd.Parameters.AddWithValue("@PersonID",(object)PersonID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Balance",(object)Balance ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@DiscountPoints",(object)DiscountPoints ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewCustomarID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newCustomarID = (int)cmd.Parameters["@NewCustomarID"].Value;
        return newCustomarID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateCustomerAsync(int CustomarID, 
  int? PersonID, DateTime CreationDate, int? CreatedByUserID, 
  decimal? Balance, double? DiscountPoints)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Customers_UpdateCustomer",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@CustomarID",CustomarID);

     cmd.Parameters.AddWithValue("@PersonID",(object)PersonID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Balance",(object)Balance ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@DiscountPoints",(object)DiscountPoints ?? DBNull.Value);
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


    public static async Task<bool> DeleteCustomerAsync(int CustomarID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Customers_DeleteCustomer",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@CustomarID",CustomarID);
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

 public static async Task<bool> IsCustomerExistsAsync(int CustomarID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Customers_CheckCustomerExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@CustomarID",CustomarID);

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

 public static async Task<DataTable> GetAllCustomersAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Customers_GetAllCustomers",connection)) 

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