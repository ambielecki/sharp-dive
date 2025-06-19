using DiveCalculator.Services.DiveCalculator;
using Microsoft.AspNetCore.Mvc;

namespace DiveCalculator.Controllers;

[ApiController]
[Route("[controller]")]
public class DiveCalculatorController(ILogger<DiveCalculatorController> logger, IDiveCalculator diveCalculator) : ControllerBase
{
    private readonly ILogger<DiveCalculatorController> _logger = logger;

    [HttpGet("pressure-group", Name = "GetPressureGroup")]
    public IActionResult GetPressureGroup(int? depth, int? minutes) {
        if (depth == null || minutes == null) {
            return BadRequest();
        }
        
        return Ok(diveCalculator.GetPressureGroup(depth.Value, minutes.Value));
    }

    [HttpGet("max-bottom-time", Name = "GetMaxBottomTime")]
    public IActionResult GetMaxBottomTime(int? depth) {
        if (depth == null) {
            return BadRequest();
        }

        return Ok(diveCalculator.GetMaxBottomTime(depth.Value));
    }
}