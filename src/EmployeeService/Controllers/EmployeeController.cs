using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Services;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public EmployeeController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var empid = _identityService.GetIdentity().EmpId;
            var name = _identityService.GetIdentity().FullName;
            var designation= _identityService.GetIdentity().Designation;
            var result = new Employee { EmpId = empid, FullName = name, Designation=designation };
            var model = await Task.Run(() => result);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);

        }

        //[HttpGet]
        //public string Get()
        //{
        //    return "value";
        //}

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value" + id;
        }

        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
