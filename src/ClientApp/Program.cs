using System;
using static ClientApp.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

namespace ClientApp
{
    class Program
    {
        static readonly string baseUrl = "http://localhost:8000";
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Console.WriteLine("********** Welcome To Our Application !!!********");
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var login = ReadLoginDetails();
                var accessToken = await Authenticate(login);
               
                if (accessToken.auth_token == null)
                {
                    Console.WriteLine("\n Incorrect UserName or Password.");
                    System.Environment.Exit(0);
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken.auth_token);
                Console.WriteLine("\n Login Successfull.");

                DisplayMenu();

                string key;
                while ((key = Console.ReadKey().KeyChar.ToString()) != "3")
                {
                    int.TryParse(key, out int keyValue);

                    switch (keyValue)
                    {
                        case 1:
                            await ShowEmployee();
                            break;
                        case 2:
                            await ShowProjectDetail();
                            break;
                        default:
                            break;
                    }

                    Console.Write("Enter the option (number): ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("App interrupted.");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("App closed.");
            }

        }

        static Login ReadLoginDetails()
        {
            Console.WriteLine();
            Console.Write("Enter the user name: ");
            var username = Console.ReadLine();
            Console.Write("Enter the password: ");
            var password = Console.ReadLine();
            return new Login() { UserName = username, Password = password };
        }

        static async Task<SecurityToken> Authenticate(Login login)
        {
            var response = await client.PostAsync("/user/authenticate", new StringContent(JsonConvert.SerializeObject(new { Username = login.UserName, Password = login.Password }),
                                Encoding.UTF8, "application/json"));
            var token = await response.Content.ReadAsStringAsync();
            if (token == null)
                return null;
            return JsonConvert.DeserializeObject<SecurityToken>(token);
        }


        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Get Employee Detail");
            Console.WriteLine("2. Get Project Detail");
            Console.WriteLine("3. Close app (X)");
            Console.WriteLine();
            Console.Write("Enter the option (number): ");
        }


        static async Task ShowEmployee()
        {


            var result = await ShowEmployeeAsync();

            Console.WriteLine();
            Console.WriteLine("Employee are...");
            Console.WriteLine();

            if (result != null)
            {
                Console.WriteLine($"Emp-ID No: {result.EmpId}");
                Console.WriteLine($"Desination: {result.Designation}");

            }
            else
            {
                Console.WriteLine($"Status: Transaction failed");
                // Console.WriteLine($"Message: {transactionResult.Message}");
            }

            Console.WriteLine();
        }

        static async Task ShowProjectDetail()
        {
            var result = await ShowProjectAsync();

            Console.WriteLine();
            Console.WriteLine("Project are...");
            Console.WriteLine();

            if (result != null)
            {
                Console.WriteLine($"Emp-ID: {result.EmpId}");
                Console.WriteLine($"EmpName: {result.EmployeeName}");
                Console.WriteLine($"ProjectId: {result.ProjectId}");
                Console.WriteLine($"ProjectName: {result.ProjectName}");

            }
            else
            {
                Console.WriteLine($"Status: Transaction failed");
                // Console.WriteLine($"Message: {transactionResult.Message}");
            }

            Console.WriteLine();
        }


        static async Task<Employee> ShowEmployeeAsync()
        {
            var response = await client.GetAsync($"/Employee/EmpDetail");
            var transactionResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Employee>(transactionResult);
        }

        static async Task<Project> ShowProjectAsync()
        {
            var response = await client.GetAsync($"/Project/ProjectDetail");
            var transactionResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Project>(transactionResult);
        }

    }
}
