using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebWork.Models;

namespace WebWork.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Employee EmployeeRec { get; set; }
        string DbConnection = DataConnect();

        public static string DataConnect()
        {
            DBConnection db = new DBConnection();
            string DbConnection = db.DbString();
            return DbConnection;
        }
        public IActionResult OnGet(int? id)
        {



            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Employee WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();

                EmployeeRec = new Employee();
                while (reader.Read())
                {
                    EmployeeRec.Id = reader.GetInt32(0);
                    EmployeeRec.EmployeeID = reader.GetString(1);
                    EmployeeRec.EmployeeName = reader.GetString(2);
                    EmployeeRec.EmployeeLName = reader.GetString(3);
                    EmployeeRec.Train = reader.GetString(4);
                    EmployeeRec.EmployeeStatus = reader.GetString(5);
                    EmployeeRec.Station = reader.GetString(6);
                    EmployeeRec.EmployeePosition = reader.GetString(7);
                }

            }

            conn.Close();

            return Page();
        }
        public IActionResult OnPost()
        {

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "DELETE Employee WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", EmployeeRec.Id);
                command.ExecuteNonQuery();
            }

            conn.Close();
            return RedirectToPage("/Index");
        }


    }
}
