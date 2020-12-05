using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWork.Models;
namespace WebWork.Pages.Employees
{
    public class MultiDeleteModel : PageModel
    {
 

        [BindProperty]
        public List<Employee> Employ { get; set; } 


        [BindProperty]
        public List<bool> IsSelect { get; set; }


        public List<Employee> EmployeeToDelete { get; set; }
        string DbConnection = dataConnect();
        public static string dataConnect()
        {

            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DbString();
            return DbConnection;
        }
        public IActionResult OnGet()
        {

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Employee";

                SqlDataReader reader = command.ExecuteReader();

                Employ = new List<Employee>(); 
                IsSelect = new List<bool>();
                while (reader.Read())
                {
                    Employee EmployeeRec = new Employee();
                    EmployeeRec.Id = reader.GetInt32(0); 
                    EmployeeRec.EmployeeID = reader.GetString(1);
                    EmployeeRec.EmployeeName = reader.GetString(2);
                    EmployeeRec.EmployeeLName = reader.GetString(3);
                    EmployeeRec.Station = reader.GetString(4);
                    EmployeeRec.EmployeeStatus = reader.GetString(5);
                    EmployeeRec.Train = reader.GetString(6);
                    EmployeeRec.EmployeePosition = reader.GetString(7);
                    Employ.Add(EmployeeRec);
                    IsSelect.Add(false);

                }
            }


            return Page();

        }

        public IActionResult OnPost()
        {
            EmployeeToDelete = new List<Employee>();
            for (int i = 0; i < Employ.Count; i++) 
            {
                if (IsSelect[i] == true) 
                {
                    EmployeeToDelete.Add(Employ[i]); 
                }
            }

            Console.WriteLine("Employee to be deleted : ");

            for (int i = 0; i < EmployeeToDelete.Count(); i++)
            {
                Console.WriteLine(EmployeeToDelete[i].Id); 
                Console.WriteLine(EmployeeToDelete[i].EmployeeID); 
                Console.WriteLine(EmployeeToDelete[i].EmployeeName);
                Console.WriteLine(EmployeeToDelete[i].EmployeeLName);
                Console.WriteLine(EmployeeToDelete[i].Station);
                Console.WriteLine(EmployeeToDelete[i].EmployeeStatus);
                Console.WriteLine(EmployeeToDelete[i].Train);
                Console.WriteLine(EmployeeToDelete[i].EmployeePosition);

            }

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            for (int i = 0; i < EmployeeToDelete.Count(); i++)
            {

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"DELETE FROM Employee WHERE Id = @EID";
                    command.Parameters.AddWithValue("@EID", EmployeeToDelete[i].Id);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/Index");


        }


    }
}

