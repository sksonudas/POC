using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApp
{
    public static class Model
    {
        public class Login
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class SecurityToken
        {
            public string auth_token { get; set; }
        }

        public class Employee
        {
            public int EmpId { get; set; }
            public string FullName { get; set; }
            public string Designation { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }


        public class Project
        {
            public int EmpId { get; set; }
            public string ProjectId { get; set; }
            public string ProjectName { get; set; }
            public string EmployeeName { get; set; }


        }
    }
}
