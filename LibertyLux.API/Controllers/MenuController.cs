using LibertyLux.Business.Services.Abstract;
using LibertyLux.Entity.APIResponseDto;
using LibertyLux.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibertyLux.Entity.APIResponseDto;

namespace LibertyLux.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuService.GetAllMenuItemsAsync();
                return Ok(menuItems); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetMenuItemsByCategory(int category)
        {
            try
            {
                var menuItems = await _menuService.GetMenuItemsByCategoryAsync(category);
                return Ok(menuItems); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            try
            {
                var menuItem = await _menuService.GetMenuItemByIdAsync(id);
                if (menuItem == null)
                {
                    return NotFound($"Menu item with ID {id} not found."); 
                }
                return Ok(menuItem); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMenuItem(MenuItem menuItem)
        {
            try
            {
                await _menuService.AddMenuItemAsync(menuItem);
                return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.MenuItemId }, menuItem);
                // Returns HTTP 201 with the created entity as body and Location header.
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, MenuItem menuItem)
        {
            try
            {
                var existingItem = await _menuService.GetMenuItemByIdAsync(id);
                if (existingItem == null)
                {
                    return NotFound($"Menu item with ID {id} not found.");
                }
                menuItem.MenuItemId = id;
                await _menuService.UpdateMenuItemAsync(menuItem);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuService.GetMenuItemByIdAsync(id);
                if (menuItem == null)
                {
                    return NotFound($"Menu item with ID {id} not found.");
                }
                await _menuService.DeleteMenuItemAsync(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
