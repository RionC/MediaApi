using MediaApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaApi.Controllers
{
    public class StatusController : ControllerBase
    {
        ISystemTime Clock;

        public StatusController(ISystemTime clock)
        {
            Clock = clock;
        }
        
        // Get /status
        [HttpGet("status")]
        public ActionResult<StatusResponse> GetStatus()
        {
            var response = new StatusResponse
            {
                Message = "Everything is groovy!",
                CreatedAt = Clock.GetCurrent()
            };
            return Ok(response); // this will return a 200
        }

        // GET /sayhi/printName
        [HttpGet("sayhi/{name:minlength(3)}")]
        public IActionResult SayHi(string name)
        {
            return Ok($"Hello, {name}!");
        }

        // GET /employees?department=DEV
        [HttpGet("employees")]
        public IActionResult Employee(string department = "All")
        {
            return Ok($"Getting you the employees in department {department}");
        }

        [HttpPost("employees")]
        public IActionResult HireEmployee([FromBody] HiringRequest employeeToHire)
        {
            return Ok($"hiring {employeeToHire.FirstName} as a {employeeToHire.Department}");
        }
    }

    public class StatusResponse
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class HiringRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal StartingSalary { get; set; }
        public string Department { get; set; }
    }
}
