using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsPersonData 
  {

     static clsPersonData(){} 




 public static bool GetPersonByPersonID(int PersonID, 
  ref string Name, ref string Email, ref string Address, 
  ref string Phone, ref bool? Gendor, ref int? CreatedByUserID, 
  ref DateTime CreationDate) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("People_GetPersonByPersonID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@PersonID",PersonID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        Name = Convert.ToString(reader["Name"]); 

        if(reader["Email"] ==  DBNull.Value) 
          {

            Email = null; 
          }
        else 
          Email = Convert.ToString(reader["Email"]);  

        if(reader["Address"] ==  DBNull.Value) 
          {

            Address = null; 
          }
        else 
          Address = Convert.ToString(reader["Address"]);  

        if(reader["Phone"] ==  DBNull.Value) 
          {

            Phone = null; 
          }
        else 
          Phone = Convert.ToString(reader["Phone"]);  

        if(reader["Gendor"] ==  DBNull.Value) 
          {

            Gendor = null; 
          }
        else 
          Gendor = Convert.ToBoolean(reader["Gendor"]);  

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

        CreationDate = Convert.ToDateTime(reader["CreationDate"]); 

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


    public static async Task<int> AddNewPersonAsync(int PersonID, 
  string Name, string Email, string Address, 
  string Phone, bool? Gendor, int? CreatedByUserID, 
  DateTime CreationDate)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("People_InsertNewPerson",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PersonID",PersonID);

         cmd.Parameters.AddWithValue("@Name",Name);

     cmd.Parameters.AddWithValue("@Email",(object)Email ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Address",(object)Address ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Phone",(object)Phone ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Gendor",(object)Gendor ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

         SqlParameter outputIdParam = new SqlParameter("@NewPersonID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newPersonID = (int)cmd.Parameters["@NewPersonID"].Value;
        return newPersonID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdatePersonAsync(int PersonID, 
  string Name, string Email, string Address, 
  string Phone, bool? Gendor, int? CreatedByUserID, 
  DateTime CreationDate)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("People_UpdatePerson",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PersonID",PersonID);

         cmd.Parameters.AddWithValue("@Name",Name);

     cmd.Parameters.AddWithValue("@Email",(object)Email ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Address",(object)Address ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Phone",(object)Phone ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Gendor",(object)Gendor ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);
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


    public static async Task<bool> DeletePersonAsync(int PersonID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("People_DeletePerson",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PersonID",PersonID);
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

 public static async Task<bool> IsPersonExistsAsync(int PersonID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("People_CheckPersonExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@PersonID",PersonID);

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

 public static async Task<DataTable> GetAllPeopleAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("People_GetAllPeople",connection)) 

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