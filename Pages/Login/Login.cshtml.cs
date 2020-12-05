using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWork.Models;

namespace WebWork.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User UserMod { get; set; }

        public string Message { get; set; }

        public string SessionID;

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            DBConnection db = new DBConnection();
            string DbConnection = db.DbString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(UserMod.UserName);
            Console.WriteLine(UserMod.Password);








            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT FirstName, UserName, Role FROM UserTable WHERE UserName = @UName AND Password = @Pwd";

                command.Parameters.AddWithValue("@UName", UserMod.UserName);
                command.Parameters.AddWithValue("@Pwd", UserMod.Password);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserMod.FirstName = reader.GetString(0);
                    UserMod.UserName = reader.GetString(1);
                    UserMod.Role = reader.GetString(2);
                }
            }
            if (!string.IsNullOrEmpty(UserMod.FirstName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("username", UserMod.UserName);
                HttpContext.Session.SetString("fname", UserMod.FirstName);

                if (UserMod.Role == "User")
                {
                    return RedirectToPage("/UserPages/UserIndex");
                }
                else
                {
                    return RedirectToPage("/AdminPages/AdminIndex");
                }


            }
            else
            {
                Message = "Invalid Username and Password!";
                return Page();
            }


        }

    }
}
