using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWork.Pages
{
    public class DBConnection
    {
        public string DbString()
        {
            string dbString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return dbString;        
        }
        // Here we added the Database connection string from our local machines so when working on this project we had to change this everytime we got an upadated version of the Assignment from one another
        // This file connects the webpage to the database allowing us to add, remove and update fields in the tables we have in our database.
    }
}
