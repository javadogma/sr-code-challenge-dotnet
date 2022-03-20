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

        [HttpPost("{id}", Name = "setCompensation")]
        public IActionResult SetCompensation([FromBody] CompensationInput compensationInput)
        {
            _logger.LogDebug($"Received compensation set request for '{compensationInput.Id}'");

            // TODO: validate the incoming variables. Return an error if there is a problem.

            var employee = _employeeService.GetById(compensationInput.Id);
            var updatedEmployee = _employeeService.GetById(compensationInput.Id);

            if (employee != null && updatedEmployee != null)
            {
                updatedEmployee.Salary = compensationInput.Salary;
                updatedEmployee.EffectiveDate = compensationInput.EffectiveDate;

                _employeeService.Replace(employee, updatedEmployee);
            }

            Compensation compensation = new Compensation(employee);

            // Compensation will proably never be null here but doesn't hurt.
            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
