using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDailySummary([FromQuery] DateTime? date)
        {
            var result = await _reportsService.GetDailyEarningsAsync(date);
            return Ok(result);
        }
        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetTotalSales(DateTime startDate, DateTime endDate)
        {
            var result = await _reportsService.GetTotalSalesAsync(startDate, endDate);
            return Ok(result);
        }
    }
}
