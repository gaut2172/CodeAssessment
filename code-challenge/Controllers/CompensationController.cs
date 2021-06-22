using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received compensation GET request for '{id}'");

            Compensation compensation = _compensationService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation CREATE request for employeeId " +
                $"'{compensation.Employee}', " +
                $"salary: '{compensation.Salary}', " +
                $"effective date: '{compensation.EffectiveDate}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.CompensationId }, compensation);
        }

    }
}
