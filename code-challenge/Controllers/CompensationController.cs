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
            if(employee == null)
                return NotFound();

            Compensation compensation = new Compensation(employee);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        /// <summary>
        /// Handle the compensation update
        /// </summary>
        /// 
        /// id is pulled from the query parameters, compensation input is pulled
        /// from the request body. [FromBody].
        /// 
        /// <param name="id"></param>
        /// <param name="compensationInput"></param>
        /// <returns></returns>
        [HttpPost("{id}", Name = "setCompensation")]
        public IActionResult SetCompensation(string id, [FromBody] CompensationInput compensationInput)
        {
            _logger.LogDebug($"Received compensation set request for '{id}'");

            // if the employee can't be found, bail out.
            var employee = _employeeService.GetById(id);
            if(employee == null)
                return NotFound();

            // Copy the employee so the id can be preserved.
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

            // update the compensation values
            updatedEmployee.Salary = compensationInput.Salary;
            updatedEmployee.EffectiveDate = compensationInput.EffectiveDate;

            // update the employee compensation information
            employee = _employeeService.Replace(employee, updatedEmployee);

            // create a compensation object
            Compensation compensation = new Compensation(employee);

            // Compensation will proably never be null here but doesn't hurt.
            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
