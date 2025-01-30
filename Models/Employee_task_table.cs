using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace emp_task.Models
{
    public class Employee_task_table
    {
        public int Id { get; set; }
        [Key] 
        public int Profile_id { get; set; }
        [ForeignKey(nameof(Profile_id))]
        public string Task_Name { get; set; }
        public string Task_Descr { get; set; }
        public DateTimeOffset Start_time { get; set; }
        public int Status { get; set; }
         



    }
}