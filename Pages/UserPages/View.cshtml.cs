using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWork.Models;
using System.Data.SqlClient;
namespace WebWork.Pages.Employees
{
    public class ViewModel : PageModel
    {
        public List<Employee> EmployeeRecord { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Position { get; set; }

        public List<string> PositionItems { get; set; } = new List<string> { "Team Member", "Shift Runner", "ARGM", "RGM", "Area Coach" };

        public void OnGet()
        {
            DBConnection db = new DBConnection();
            string DbConnection = db.DbString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Employee";
                if (!string.IsNullOrEmpty(Position))
                {
                    command.CommandText += " WHERE EmployeePosition = @EPosition";
                    command.Parameters.AddWithValue("@EPosition", (Position));
                }


                SqlDataReader reader = command.ExecuteReader(); 

                EmployeeRecord = new List<Employee>(); 
                while (reader.Read())
                {
                    Employee record = new Employee(); 
                    record.Id = reader.GetInt32(0);
                    record.EmployeeID = reader.GetString(1);
                    record.EmployeeName = reader.GetString(2);
                    record.EmployeeLName = reader.GetString(3);
                    record.Station = reader.GetString(4);
                    record.EmployeeStatus = reader.GetString(5);
                    record.Train = reader.GetString(6);
                    record.EmployeePosition = reader.GetString(7);

                    EmployeeRecord.Add(record); 
                }

                reader.Close();


            }
        }
    }
}
