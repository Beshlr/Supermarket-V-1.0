using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SuberMarketDB_DataAccess   
{ 



 public class clsPayrollData 
  {

     static clsPayrollData(){} 




 public static bool GetPayrollByEmployeeID(int EmployeeID, 
  int EmployeeID, ref decimal BasicSalary, ref decimal? Bonus, 
  ref decimal? Deductions, ref decimal NetSalary, ref DateTime PayRollDate, 
  ref bool IsActive) 

  {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Payroll_GetPayrollByEmployeeID",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         connection.Open();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

        using (SqlDataReader reader =  cmd.ExecuteReader()) 

      {

        if(reader.Read())   
        {

        EmployeeID = Convert.ToInt32(reader["EmployeeID"]); 

        BasicSalary = Convert.ToDecimal(reader["BasicSalary"]); 

        if(reader["Bonus"] ==  DBNull.Value) 
          {

            Bonus = null; 
          }
        else 
          Bonus = Convert.ToDecimal(reader["Bonus"]);  

        if(reader["Deductions"] ==  DBNull.Value) 
          {

            Deductions = null; 
          }
        else 
          Deductions = Convert.ToDecimal(reader["Deductions"]);  

        NetSalary = Convert.ToDecimal(reader["NetSalary"]); 

        PayRollDate = Convert.ToDateTime(reader["PayRollDate"]); 

        IsActive = Convert.ToBoolean(reader["IsActive"]); 

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


    public static async Task<int> AddNewPayrollAsync(int EmployeeID, 
  int EmployeeID, decimal BasicSalary, decimal? Bonus, 
  decimal? Deductions, decimal NetSalary, DateTime PayRollDate, 
  bool IsActive)
   {
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Payroll_InsertNewPayroll",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@BasicSalary",BasicSalary);

     cmd.Parameters.AddWithValue("@Bonus",(object)Bonus ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Deductions",(object)Deductions ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@NetSalary",NetSalary);

         cmd.Parameters.AddWithValue("@PayRollDate",PayRollDate);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);

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


    public static async Task<bool> UpdatePayrollAsync(int EmployeeID, 
  int EmployeeID, decimal BasicSalary, decimal? Bonus, 
  decimal? Deductions, decimal NetSalary, DateTime PayRollDate, 
  bool IsActive)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Payroll_UpdatePayroll",connection)) 

     {

        cmd.CommandType = CommandType.StoredProcedure; 
            

         await connection.OpenAsync();

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@EmployeeID",EmployeeID);

         cmd.Parameters.AddWithValue("@BasicSalary",BasicSalary);

     cmd.Parameters.AddWithValue("@Bonus",(object)Bonus ?? DBNull.Value);

     cmd.Parameters.AddWithValue("@Deductions",(object)Deductions ?? DBNull.Value);

         cmd.Parameters.AddWithValue("@NetSalary",NetSalary);

         cmd.Parameters.AddWithValue("@PayRollDate",PayRollDate);

         cmd.Parameters.AddWithValue("@IsActive",IsActive);
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


    public static async Task<bool> DeletePayrollAsync(int EmployeeID)
   {
        bool IsRowsAffected = false;
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Payroll_DeletePayroll",connection)) 

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

 public static async Task<bool> IsPayrollExistsAsync(int EmployeeID) 
 {

    bool IsFound = false; 



   try 
   {


     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
   

      using (SqlCommand cmd = new SqlCommand("Payroll_CheckPayrollExists",connection)) 

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

 public static async Task<DataTable> GetAllPayrollAsync() 
    {
    DataTable dt = new DataTable();
 
   

   try 
   {


    

     using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 

    {
 

      

      using (SqlCommand cmd = new SqlCommand("Payroll_GetAllPayroll",connection)) 

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