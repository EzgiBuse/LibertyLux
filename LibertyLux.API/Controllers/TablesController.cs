using LibertyLux.Business.Services.Abstract;
using LibertyLux.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibertyLux.API.Controllers
{
    [Route("api/[controller]")]
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
            try
            {
                var tables = await _tableService.GetAllTablesAsync();
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            try
            {
                var table = await _tableService.GetTableByIdAsync(id);
                if (table == null)
                {
                    return NotFound($"Table with ID {id} not found.");
                }
                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] RestaurantTable table)
        {
            try
            {
                if (table == null)
                    return BadRequest("Table is null.");

                await _tableService.AddTableAsync(table);
                return CreatedAtAction(nameof(GetTableById), new { id = table.TableId }, table);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] RestaurantTable table)
        {
            try
            {
                if (table == null)
                    return BadRequest("Table is null.");
                if (id != table.TableId)
                    return BadRequest("ID mismatch.");

                var tableToUpdate = await _tableService.GetTableByIdAsync(id);
                if (tableToUpdate == null)
                {
                    return NotFound($"Table with ID {id} not found.");
                }

                await _tableService.UpdateTableAsync(table);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{tableId}/{occupation}")]
        public async Task<IActionResult> UpdateTableOccupation(int tableId, int occupation)
        {
            try
            {
                var tableToUpdate = await _tableService.GetTableByIdAsync(tableId);
                if (tableToUpdate == null)
                {
                    return NotFound($"Table with ID {tableId} not found.");
                }
                tableToUpdate.Occupancy= occupation;
               
                await _tableService.UpdateTableAsync(tableToUpdate);

                return NoContent(); // Indicates success without returning data
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            try
            {
                var tableToDelete = await _tableService.GetTableByIdAsync(id);
                if (tableToDelete == null)
                {
                    return NotFound($"Table with ID {id} not found.");
                }

                await _tableService.DeleteTableAsync(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




    }
}
