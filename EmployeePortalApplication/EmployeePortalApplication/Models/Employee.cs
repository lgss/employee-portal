using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSS.Mentoring.EmployeePortal.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        [Display(Name = "EmployeeID")]
        public string EmployeeNumber { get; set; }
        public string Photo { get; set; }
    }
}