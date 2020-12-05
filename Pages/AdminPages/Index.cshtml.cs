using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWork.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebWork.Pages.Employees
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Employee EmployeeRecord { get; set; }
        public List<string> Position = new List<string> { "Team Member", "Shift Runner", "ARGM", "RGM","Area Coach" };
        public List<SelectListItem> Station = new List<SelectListItem>
        
        {
            new SelectListItem { Text = "Front of House", Value = "Front of House"},
            new SelectListItem { Text = "Middle of House", Value = "Middle of House"},
            new SelectListItem { Text = "Back of House", Value = "Back of House"},
            new SelectListItem { Text = "Sides", Value = "Sides", Selected = true },
            new SelectListItem { Text = "Lobby Host", Value = "Lobby Host"}
        };
        public string[] StatusOfEmployee = new string[2] { "Active", "Not-Active" };
        public List<string> Train = new List<string> { "NONE","FOH", "MOH","BOH", "SIDES","LH","SR","ARGM" };

        public void OnGet()
        {
            EmployeeRecord = new Employee();
            EmployeeRecord.EmployeeTraining = new List<bool> { true, false, false, false, false, false, false,false };
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            foreach (var station in EmployeeRecord.EmployeeStation)
            {
                Console.WriteLine(station);
                EmployeeRecord.Station += station + ",";
            }


            for (int i = 0; i < Train.Count(); i++)
            {
                Console.WriteLine(EmployeeRecord.EmployeeTraining[i]);
                if (EmployeeRecord.EmployeeTraining[i] == true)
                {
                    EmployeeRecord.Train += Train[i] + ",";
                }
            }

            DBConnection db = new DBConnection();
            string DbConnection = db.DbString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO Employee (EmployeeID, EmployeeName, EmployeeLName, EmployeeStation, EmployeeStatus, EmployeeTraining, EmployeePosition) VALUES (@EID, @EName, @ELName, @EStation, @EStatus, @ETraining, @EPosition)";

                command.Parameters.AddWithValue("@EID", EmployeeRecord.EmployeeID);
                command.Parameters.AddWithValue("@EName", EmployeeRecord.EmployeeName);
                command.Parameters.AddWithValue("@ELName", EmployeeRecord.EmployeeLName);
                command.Parameters.AddWithValue("@EStation", EmployeeRecord.Station);
                command.Parameters.AddWithValue("@EStatus", EmployeeRecord.EmployeeStatus);
                command.Parameters.AddWithValue("@ETraining", EmployeeRecord.Train);
                command.Parameters.AddWithValue("@EPosition", EmployeeRecord.EmployeePosition);

                Console.WriteLine(EmployeeRecord.EmployeeID);
                Console.WriteLine(EmployeeRecord.EmployeeName);
                Console.WriteLine(EmployeeRecord.EmployeeLName);
                Console.WriteLine(EmployeeRecord.Station);
                Console.WriteLine(EmployeeRecord.EmployeeStatus);
                Console.WriteLine(EmployeeRecord.Train);
                Console.WriteLine(EmployeeRecord.EmployeePosition);


                command.ExecuteNonQuery();
            }


            return RedirectToPage("/Index");
        }


        
    }

}
