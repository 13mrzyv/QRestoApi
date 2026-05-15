using Business.Abstract;
using Business.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QRestoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTableResponses()
        {
            var tables = await _tableService.GetAllTableResponsesAsync();
            return Ok(tables);
        }
        // 2. Yeni masa yarat (Ancaq Admin)
        [HttpPost]
        public async Task<IActionResult> InsertTable(InsertTableRequest ınsertTableRequest)
        {
            if (string.IsNullOrWhiteSpace(ınsertTableRequest.TableNumber))
            {
                return BadRequest(new { message = "Masa nömrəsini daxil etmək mütləqdir." });
            }

            await _tableService.CreateTableAsync(ınsertTableRequest);

            return Ok(new { message = "Masa və QR Token sistem tərəfindən avtomatik yaradıldı." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] int status)
        {
            // 0: Boş, 1: Dolu, 2: Rezerv olduğunu yoxlayaq
            if (status < 0 || status > 2)
            {
                return BadRequest(new { message = "Status 0, 1 və ya 2 olmalıdır." });
            }

            try
            {
                await _tableService.UpdateTableStatusAsync(id, status);
                return Ok(new { message = "Masa statusu uğurla yeniləndi." });
            }
            catch (Exception ex)
            {
                // Xəta baş verərsə (məsələn bazada id tapılmasa)
                return StatusCode(500, new { message = "Xəta baş verdi: " + ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTableNumberByTableId(int tableId)
        {
            var tableNumber = await _tableService.GetTableNumberByTableIdAsync(tableId);
            return Ok(tableNumber);
        }
    }
}
