using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController  : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{id}", Name = "getCompensation")]
        public IActionResult GetCompensation(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var employee = _employeeService.GetById(id);

            Compensation compensation = new Compensation(employee);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        /// <summary>
        /// Handle the compensation update
        /// </summary>
        /// 
        /// I was never thrilled with the black magic of convention programming.
        /// It's not clear to the reviewer, especially a noob, that this method
        /// pulls the string from the query parameters and the CompensationInput from
        /// the request body. The difference being primative versus complex types.
        /// 
        /// <param name="id"></param>
        /// <param name="compensationInput"></param>
        /// <returns></returns>
        [HttpPost("{id}", Name = "setCompensation")]
        public IActionResult SetCompensation(string id, CompensationInput compensationInput)
        {
            _logger.LogDebug($"Received compensation set request for '{id}'");

            // TODO: validate the incoming variables. Return an error if there is a problem.

            var employee = _employeeService.GetById(id);

            // Becuase this isn't a complicated class we'll just copy it instead
            // of trying to deep copy it. As I understand it, since many of these
            // are not primitive types they'll be copied by reference and we don't
            // want that.
            var updatedEmployee = new Employee()
            {
                Department = employee.Department,
                DirectReports = employee.DirectReports,
                EffectiveDate = employee.EffectiveDate,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                Salary = employee.Salary
            };

            if (employee != null && updatedEmployee != null)
            {
                updatedEmployee.Salary = compensationInput.Salary;
                updatedEmployee.EffectiveDate = compensationInput.EffectiveDate;
                updatedEmployee.Salary = new Decimal(155.12);
                updatedEmployee.EffectiveDate = DateTime.Now;

                employee = _employeeService.Replace(employee, updatedEmployee);
            }

            Compensation compensation = new Compensation(employee);

            // Compensation will proably never be null here but doesn't hurt.
            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
