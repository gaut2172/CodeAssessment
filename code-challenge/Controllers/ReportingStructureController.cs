using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/reporting")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{id}", Name = "getReportingById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received reporting structure GET request for '{id}'");

            // Use recursive method to find num of people that report to the employee
            int numReporters = GetReportersCount(id);

            // Catch 404 not found
            if (numReporters == -1)
            {
                return NotFound();
            }

            return Ok(numReporters);
        }

        public int GetReportersCount(string employeeId)
        {
            int numReporters = 0;

            // Query in-memory EF Core database for the given employee
            Employee employee = _employeeService.GetById(employeeId);

            // If an employee was found
            if (employee != null)
            {
                // For every employeeId that reports to the found employee...
                foreach (string reporter in employee.DirectReports)
                {
                    numReporters++;
                    // Run this function again recursively, adding up all of the employeeIds that indirectly report to the first employee
                    numReporters += GetReportersCount(reporter);
                }
            }
            else
            {
                // No employee has the given employeeId
                numReporters = -1;
            }

            return numReporters;
        }
    }
}
