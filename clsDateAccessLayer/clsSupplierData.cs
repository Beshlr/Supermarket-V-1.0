using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsSupplierData 
  {

     static clsSupplierData(){} 




 public static bool GetSupplierBySupplierID(int SupplierID, 
  ref int? PersonID, ref string NationalNo, ref bool IsActive, 
  ref DateTime CreationDate, ref int? CreatedByUserID, ref decimal? Depts) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Suppliers_GetSupplierBySupplierID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@SupplierID",SupplierID);

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

        NationalNo = Convert.ToString(reader["NationalNo"]); 

        IsActive = Convert.ToBoolean(reader["IsActive"]); 

        CreationDate = Convert.ToDateTime(reader["CreationDate"]); 

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

        if(reader["Depts"] ==  DBNull.Value) 
          {

            Depts = null; 
          }
        else 
          Depts = Convert.ToDecimal(reader["Depts"]);  

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


    public static async Task<int> AddNewSupplierAsync(int SupplierID, 
  int? PersonID, string NationalNo, bool IsActive, 
  DateTime CreationDate, int? CreatedByUserID, decimal? Depts)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Suppliers_InsertNewSupplier",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SupplierID",SupplierID);

     cmd.Parameters.AddWithValue("@PersonID",(object)PersonID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@NationalNo",NationalNo);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Depts",(object)Depts ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewSupplierID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newSupplierID = (int)cmd.Parameters["@NewSupplierID"].Value;
        return newSupplierID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateSupplierAsync(int SupplierID, 
  int? PersonID, string NationalNo, bool IsActive, 
  DateTime CreationDate, int? CreatedByUserID, decimal? Depts)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Suppliers_UpdateSupplier",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SupplierID",SupplierID);

     cmd.Parameters.AddWithValue("@PersonID",(object)PersonID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@NationalNo",NationalNo);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Depts",(object)Depts ?? DBNull.Value);
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


    public static async Task<bool> DeleteSupplierAsync(int SupplierID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Suppliers_DeleteSupplier",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SupplierID",SupplierID);
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

 public static async Task<bool> IsSupplierExistsAsync(int SupplierID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Suppliers_CheckSupplierExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@SupplierID",SupplierID);

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

 public static async Task<DataTable> GetAllSuppliersAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Suppliers_GetAllSuppliers",connection)) 

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