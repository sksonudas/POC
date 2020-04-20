using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDetailService.Models;
using ProjectDetailService.Services;

namespace ProjectDetailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDetailController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IEmpProjectService _empProjectService;
        public ProjectDetailController(IIdentityService identityService, IEmpProjectService empProjectService)
        {
            _identityService = identityService;
            _empProjectService = empProjectService;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var empid = _identityService.GetIdentity().EmpId;
            var name = _identityService.GetIdentity().FullName;
            EmployeeProjectModel result = _empProjectService.GetEmpProjectDetail(empid);
            result.EmployeeName = name;
            var model = await Task.Run(() => result);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);

        }


        // GET: api/Detail/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Detail
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Detail/5
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
