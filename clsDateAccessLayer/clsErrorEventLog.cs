using System;
using System.Diagnostics;
namespace SuberMarketDB_DataAccess   

{ 



 public class clsErrorEventLog 


  {
         private static readonly string SourceName = "SuberMarketDB";
      static clsErrorEventLog()
    {

       if (!EventLog.Exists(SourceName))  
       {
              EventLog.CreateEventSource(SourceName, "Application"); 
       } 
    }

     public static void LogError(string ErrorMessage, EventLogEntryType entryType = EventLogEntryType.Error) 
    {
            EventLog.WriteEntry(SourceName, ErrorMessage, entryType); 
    }

   }

}