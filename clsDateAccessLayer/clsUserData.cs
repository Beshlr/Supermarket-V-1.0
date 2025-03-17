using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsUserData 
  {

     static clsUserData(){} 




 public static bool GetUserByUserID(int UserID, 
  ref int EmployeeID, ref string UserName, ref string Password, 
  ref bool IsActive, ref int? PermissionsID, ref DateTime CreationDate, 
  ref int? CreatedByUserID) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Users_GetUserByUserID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@UserID",UserID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        EmployeeID = Convert.ToInt32(reader["EmployeeID"]); 

        UserName = Convert.ToString(reader["UserName"]); 

        Password = Convert.ToString(reader["Password"]); 

        IsActive = Convert.ToBoolean(reader["IsActive"]); 

        if(reader["PermissionsID"] ==  DBNull.Value) 
          {

            PermissionsID = null; 
          }
        else 
          PermissionsID = Convert.ToInt32(reader["PermissionsID"]);  

        CreationDate = Convert.ToDateTime(reader["CreationDate"]); 

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

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


    public static async Task<int> AddNewUserAsync(int UserID, 
  int EmployeeID, string UserName, string Password, 
  bool IsActive, int? PermissionsID, DateTime CreationDate, 
  int? CreatedByUserID)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Users_InsertNewUser",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@UserID",UserID);

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@UserName",UserName);

         cmd.Parameters.AddWithValue("@Password",Password);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);

     cmd.Parameters.AddWithValue("@PermissionsID",(object)PermissionsID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewUserID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newUserID = (int)cmd.Parameters["@NewUserID"].Value;
        return newUserID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateUserAsync(int UserID, 
  int EmployeeID, string UserName, string Password, 
  bool IsActive, int? PermissionsID, DateTime CreationDate, 
  int? CreatedByUserID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Users_UpdateUser",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@UserID",UserID);

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@UserName",UserName);

         cmd.Parameters.AddWithValue("@Password",Password);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);

     cmd.Parameters.AddWithValue("@PermissionsID",(object)PermissionsID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);
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


    public static async Task<bool> DeleteUserAsync(int UserID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Users_DeleteUser",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@UserID",UserID);
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

 public static async Task<bool> IsUserExistsAsync(int UserID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Users_CheckUserExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@UserID",UserID);

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

 public static async Task<DataTable> GetAllUsersAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Users_GetAllUsers",connection)) 

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