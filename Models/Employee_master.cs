using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace emp_task.Models
{
    public class Employee_master
    {
        [Key]
        public int Profile_id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTimeOffset Date_of_birth { get; set; }
        public string Phone_number { get; set; }
        public string Email_id { get; set; }



    }
}