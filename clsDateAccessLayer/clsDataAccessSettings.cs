using System;
using System.Configuration;
namespace SuberMarketDB_DataAccess   
{   



 public class clsDataAccessSettings 


   { 
        public static readonly string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];


   }


 }

// Copy this and paste it in a file named App.config and change the conntection string value 

/*<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>

	<appSettings>
		<add key="ConnectionString" value="Server=.;Database=SuberMarketDB;User Id=sa;Password=sa123456;"/>
	</appSettings>
</configuration>*/