using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsConroller : ControllerBase
    {
        private readonly IReportsService _reportsService;
        public ReportsConroller(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDailySummary([FromQuery] DateTime? date)
        {
            var result = await _reportsService.GetDailyEarningsAsync(date);
            return Ok(result);
        }
    }
}
