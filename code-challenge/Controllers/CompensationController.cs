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

        // TODO: Review how the query parameters are retreived from the request.
        // Decoration needed?
        [HttpPost("{id}", Name = "setCompensation")]
        public IActionResult SetCompensation(
            String id,
            DateTime effectiveDate,
            decimal salary)
        {
            _logger.LogDebug($"Received compensation set request for '{id}'");

            // TODO: validate the incoming variables. Return an error if there is a problem.

            var employee = _employeeService.GetById(id);
            if(employee != null)
            {
                employee.Salary = salary;
                employee.EffectiveDate = effectiveDate;

                // TODO: have to duplicate the employee into a new object here.
                // This lets it compile for now.
                _employeeService.Replace(employee, employee);
            }

            Compensation compensation = new Compensation(employee);

            // Compensation will proably never be null here but doesn't hurt.
            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
