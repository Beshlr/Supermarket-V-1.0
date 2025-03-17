using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsLogData 
  {

     static clsLogData(){} 




 public static bool GetLogByLogID(int LogID, 
  ref int? EmployeeID, ref DateTime ActionDate, ref int? CreatedByUserID, 
  ref string ActionTitle) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Logs_GetLogByLogID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@LogID",LogID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        if(reader["EmployeeID"] ==  DBNull.Value) 
          {

            EmployeeID = null; 
          }
        else 
          EmployeeID = Convert.ToInt32(reader["EmployeeID"]);  

        ActionDate = Convert.ToDateTime(reader["ActionDate"]); 

        if(reader["CreatedByUserID"] ==  DBNull.Value) 
          {

            CreatedByUserID = null; 
          }
        else 
          CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);  

        ActionTitle = Convert.ToString(reader["ActionTitle"]); 

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


    public static async Task<int> AddNewLogAsync(int LogID, 
  int? EmployeeID, DateTime ActionDate, int? CreatedByUserID, 
  string ActionTitle)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Logs_InsertNewLog",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@LogID",LogID);

     cmd.Parameters.AddWithValue("@EmployeeID",(object)EmployeeID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ActionDate",ActionDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ActionTitle",ActionTitle);

         SqlParameter outputIdParam = new SqlParameter("@NewLogID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newLogID = (int)cmd.Parameters["@NewLogID"].Value;
        return newLogID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateLogAsync(int LogID, 
  int? EmployeeID, DateTime ActionDate, int? CreatedByUserID, 
  string ActionTitle)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Logs_UpdateLog",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@LogID",LogID);

     cmd.Parameters.AddWithValue("@EmployeeID",(object)EmployeeID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ActionDate",ActionDate);

     cmd.Parameters.AddWithValue("@CreatedByUserID",(object)CreatedByUserID ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@ActionTitle",ActionTitle);
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


    public static async Task<bool> DeleteLogAsync(int LogID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Logs_DeleteLog",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@LogID",LogID);
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

 public static async Task<bool> IsLogExistsAsync(int LogID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Logs_CheckLogExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@LogID",LogID);

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

 public static async Task<DataTable> GetAllLogsAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Logs_GetAllLogs",connection)) 

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