using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebWork.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebWork.Pages.Employees
{
    public class UpdateModel : PageModel


    {
       [BindProperty]
        public Employee EmployeeRec { get; set; }
        public string[] StatusOfEmployee = new string[2] { "Active", "Not-Active" };
        public List<string> Position = new List<string> { "Team Member", "Shift Runner", "ARGM", "RGM", "Area Coach" };
        public List<string> Train = new List<string> { "NONE", "FOH", "MOH", "BOH", "SIDES", "LH", "SR", "ARGM" };
        public List<SelectListItem> Station = new List<SelectListItem>
        {
            new SelectListItem { Text = "Front of House", Value = "Front of House"},
            new SelectListItem { Text = "Middle of House", Value = "Middle of House"},
            new SelectListItem { Text = "Back of House", Value = "Back of House"},
            new SelectListItem { Text = "Sides", Value = "Sides", Selected = true },
            new SelectListItem { Text = "Lobby Host", Value = "Lobby Host"}
        };
        string DbConnection = DataConnect();

        public static string DataConnect()
        {
            DBConnection db = new DBConnection();
            string DbConnection = db.DbString();
            return DbConnection;
        }
        public IActionResult OnGet(int? id)
        {
            EmployeeRec = new Employee();
            EmployeeRec.EmployeeTraining = new List<bool> { true, false, false, false, false, false, false, false };
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();




            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Employee WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            foreach (var station in EmployeeRec.EmployeeStation)
            {
                Console.WriteLine(station);
                EmployeeRec.Station += station + ",";
            }
            for (int i = 0; i < Train.Count(); i++)
            {
                Console.WriteLine(EmployeeRec.EmployeeTraining[i]);
                if (EmployeeRec.EmployeeTraining[i] == true)
                {
                    EmployeeRec.Train += Train[i] + ",";
                }
            }
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(EmployeeRec.EmployeeID);
            Console.WriteLine(EmployeeRec.EmployeeName);
            Console.WriteLine(EmployeeRec.EmployeeLName);
            Console.WriteLine(EmployeeRec.Station);
            Console.WriteLine(EmployeeRec.EmployeeStatus);
            Console.WriteLine(EmployeeRec.Train);
            Console.WriteLine(EmployeeRec.EmployeePosition);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Employee SET EmployeeID =@EID , EmployeeName = @EName, EmployeeLName = @ELName, EmployeeStation = @EStation, EmployeeStatus = @EStatus, EmployeeTraining = @ETraining, EmployeePosition = @EPosition WHERE Id =@ID ";

                command.Parameters.AddWithValue("@ID", EmployeeRec.Id);
                command.Parameters.AddWithValue("@EID", EmployeeRec.EmployeeID);
                command.Parameters.AddWithValue("@EName", EmployeeRec.EmployeeName);
                command.Parameters.AddWithValue("@ELName", EmployeeRec.EmployeeLName);
                command.Parameters.AddWithValue("@EStation", EmployeeRec.Station);
                command.Parameters.AddWithValue("@EStatus", EmployeeRec.EmployeeStatus);
                command.Parameters.AddWithValue("@ETraining", EmployeeRec.Train);
                command.Parameters.AddWithValue("@EPosition", EmployeeRec.EmployeePosition);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("/Index");
        }


    }
}
