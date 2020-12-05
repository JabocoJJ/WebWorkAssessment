using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebWork.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }
        [Required]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Employee Last Name")]
        public string EmployeeLName { get; set; }

        
        [Display(Name = "Employee Station")] //course
        public List<string> EmployeeStation { get; set; }

        [Required]
        [Display(Name = "Employee Status")]
        public string EmployeeStatus { get; set; }

        
        [Display(Name = "Employee Training")]//year
        public List<bool> EmployeeTraining { get; set; }

        [Required]
        [Display(Name = "Employee Position")]//Level
        public string EmployeePosition { get; set; }

        
        public string Train { get; set; }//year

        
        public string Station { get; set; }//course

    }
}
