using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
