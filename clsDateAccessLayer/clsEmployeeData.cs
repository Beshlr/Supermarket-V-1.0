using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsEmployeeData 
  {

     static clsEmployeeData(){} 




 public static bool GetEmployeeByEmployeeID(int EmployeeID, 
  ref int PersonID, ref string NationalNo, ref bool IsActive, 
  ref DateTime CreationDate, ref string JobTitle, ref int CreatedByUserID, 
  ref decimal Salary, ref decimal? Fines, ref decimal NetSalary, 
  ref DateTime HiringDate, ref int? ShiftID) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Employees_GetEmployeeByEmployeeID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        PersonID = Convert.ToInt32(reader["PersonID"]); 

        NationalNo = Convert.ToString(reader["NationalNo"]); 

        IsActive = Convert.ToBoolean(reader["IsActive"]); 

        CreationDate = Convert.ToDateTime(reader["CreationDate"]); 

        JobTitle = Convert.ToString(reader["JobTitle"]); 

        CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]); 

        Salary = Convert.ToDecimal(reader["Salary"]); 

        if(reader["Fines"] ==  DBNull.Value) 
          {

            Fines = null; 
          }
        else 
          Fines = Convert.ToDecimal(reader["Fines"]);  

        NetSalary = Convert.ToDecimal(reader["NetSalary"]); 

        HiringDate = Convert.ToDateTime(reader["HiringDate"]); 

        if(reader["ShiftID"] ==  DBNull.Value) 
          {

            ShiftID = null; 
          }
        else 
          ShiftID = Convert.ToInt32(reader["ShiftID"]);  

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


    public static async Task<int> AddNewEmployeeAsync(int EmployeeID, 
  int PersonID, string NationalNo, bool IsActive, 
  DateTime CreationDate, string JobTitle, int CreatedByUserID, 
  decimal Salary, decimal? Fines, decimal NetSalary, 
  DateTime HiringDate, int? ShiftID)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Employees_InsertNewEmployee",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@PersonID",PersonID);

         cmd.Parameters.AddWithValue("@NationalNo",NationalNo);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

         cmd.Parameters.AddWithValue("@JobTitle",JobTitle);

         cmd.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

         cmd.Parameters.AddWithValue("@Salary",Salary);

     cmd.Parameters.AddWithValue("@Fines",(object)Fines ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@NetSalary",NetSalary);

         cmd.Parameters.AddWithValue("@HiringDate",HiringDate);

     cmd.Parameters.AddWithValue("@ShiftID",(object)ShiftID ?? DBNull.Value);

         SqlParameter outputIdParam = new SqlParameter("@NewEmployeeID",SqlDbType.Int)
         {
                      Direction = ParameterDirection.Output
         };

         cmd.Parameters.Add(outputIdParam);
         await cmd.ExecuteNonQueryAsync();
        int newEmployeeID = (int)cmd.Parameters["@NewEmployeeID"].Value;
        return newEmployeeID;

     }

    }

   }

   catch(SqlException ex) 
   {

    clsErrorEventLog.LogError(ex.Message);
    return 0;
   }
   }


    public static async Task<bool> UpdateEmployeeAsync(int EmployeeID, 
  int PersonID, string NationalNo, bool IsActive, 
  DateTime CreationDate, string JobTitle, int CreatedByUserID, 
  decimal Salary, decimal? Fines, decimal NetSalary, 
  DateTime HiringDate, int? ShiftID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Employees_UpdateEmployee",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@PersonID",PersonID);

         cmd.Parameters.AddWithValue("@NationalNo",NationalNo);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);

         cmd.Parameters.AddWithValue("@CreationDate",CreationDate);

         cmd.Parameters.AddWithValue("@JobTitle",JobTitle);

         cmd.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

         cmd.Parameters.AddWithValue("@Salary",Salary);

     cmd.Parameters.AddWithValue("@Fines",(object)Fines ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@NetSalary",NetSalary);

         cmd.Parameters.AddWithValue("@HiringDate",HiringDate);

     cmd.Parameters.AddWithValue("@ShiftID",(object)ShiftID ?? DBNull.Value);
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


    public static async Task<bool> DeleteEmployeeAsync(int EmployeeID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Employees_DeleteEmployee",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);
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

 public static async Task<bool> IsEmployeeExistsAsync(int EmployeeID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Employees_CheckEmployeeExists",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

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

 public static async Task<DataTable> GetAllEmployeesAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Employees_GetAllEmployees",connection)) 

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