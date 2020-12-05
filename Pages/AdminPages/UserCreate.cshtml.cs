using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWork.Models;

namespace WebWork.Pages.Users
{
    public class UserCreateModel : PageModel
    {
        [BindProperty]
        public User UserMod { get; set; }

        public List<string> URole { get; set; } = new List<string> { "User", "Admin" };
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DBConnection db = new DBConnection();
            string DbConnection = db.DbString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(UserMod.FirstName);
            Console.WriteLine(UserMod.UserName);
            Console.WriteLine(UserMod.Password);
            Console.WriteLine(UserMod.Role);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO UserTable (FirstName, UserName, Password, Role) VALUES (@FName, @UName, @Pwd, @Role)";

                command.Parameters.AddWithValue("@FName", UserMod.FirstName);
                command.Parameters.AddWithValue("@UName", UserMod.UserName);
                command.Parameters.AddWithValue("@Pwd", UserMod.Password);
                command.Parameters.AddWithValue("@Role", UserMod.Role);
                command.ExecuteNonQuery();
            }

            return RedirectToPage("/Index");
        }
    }
}
