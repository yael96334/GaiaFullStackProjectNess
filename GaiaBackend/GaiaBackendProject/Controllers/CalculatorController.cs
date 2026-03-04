using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculationProject.Models;
using CalculationProject.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpGet("operations")]
        public async Task<IActionResult> GetOperations()
        {
            var operations = await _calculatorService.GetOperationsAsync();
            return Ok(operations.Select(o => new { o.Code, o.Type, o.Expression }));
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                var result = await _calculatorService.CalculateAsync(request);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("history/{id}")]
        public async Task<IActionResult> GetHistory(int id)
        {
            return Ok(await _calculatorService.GetHistoryAsync(id));
        }
    }
}