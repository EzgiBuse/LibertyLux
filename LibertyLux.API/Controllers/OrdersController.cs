using LibertyLux.Business.Services.Abstract;
using LibertyLux.Business.Services.Concrete;
using LibertyLux.Entity;
using LibertyLux.Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibertyLux.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllOrderItems")]
        public async Task<IActionResult> GetAllOrderItems()
        {
            try
            {
                var orderItems = await _orderService.GetAllOrderItemsAsync();
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetOrderByOrderId/{id}")]
        public async Task<IActionResult> GetOrderByOrderId(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetOrderByOrderItemId/{id}")]
        public async Task<IActionResult> GetOrderByOrderItemId(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("ApproveOrderByOrderItemId/{id}")]
        public async Task<IActionResult> ApproveOrderByOrderItemId(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByOrderItemIdAsync(id);
                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                order.Status = Status.Served;
                await _orderService.UpdateOrderAsync(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateOrderStatusByOrderItemId/{id}/{status}")]
        public async Task<IActionResult> UpdateOrderStatusByOrderItemId(int id,string status)
        {
            try
            {
                var order = await _orderService.GetOrderByOrderItemIdAsync(id);
                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                switch (status)
                {
                    case "Pending":
                        order.Status = Status.Pending;
                        break;
                    case "InProgress":
                        order.Status = Status.Cancelled;
                        break;
                    case "Ready":
                        order.Status = Status.Ready;
                        break;
                    case "Served":
                        order.Status = Status.Served;
                        break;
                    case "Cancelled":
                        order.Status = Status.Cancelled;
                        break;
                    default:
                        break;
                }

                
                await _orderService.UpdateOrderAsync(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("tableorders/{tableId}")]
        public async Task<IActionResult> GetOrdersByTable(int tableId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByTableAsync(tableId);
                if (orders == null)
                {
                    return NotFound($"Orders for table ID {tableId} not found.");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("tableorderdetails/{tableId}")]
        public async Task<IActionResult> GetAllOrderDetailsByTable(int tableId)
        {
            try
            {
                var orders = await _orderService.GetAllOrderDetailsByTableAsync(tableId);
                if (orders == null)
                {
                    return NotFound($"Orders for table ID {tableId} not found.");
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        

        [HttpPost("AddOrder/{tableId}")]
        public async Task<IActionResult> AddOrder([FromRoute] int tableId, [FromBody] List<OrderCreateDto> orderDtos)
        {
            try
            {

                if (orderDtos == null || !orderDtos.Any())
                {
                    return BadRequest("Invalid order details.");
                }

                var order = new Order
                {
                    TableId = tableId,
                    Status = Status.Pending,
                    OrderItems = new List<OrderItem>()
                };

                await _orderService.AddOrderAsync(order);

                // Convert OrderCreateDtos to OrderItems
                foreach (var dto in orderDtos)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        MenuItemId = dto.MenuItemId,
                        Quantity = dto.Quantity,
                        MenuItemPrice = dto.MenuItemPrice
                    };

                   order.OrderItems.Add(orderItem);
                }

                await _orderService.UpdateOrderAsync(order);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("AddOrderItems")]
        public async Task<IActionResult> AddOrderItems([FromBody] List<OrderItem> orderItems)
        {
            try
            {
                if (orderItems == null)
                    return BadRequest("Order Items are null.");

                await _orderService.AddOrderItemsAsync(orderItems);
                return Ok("OrderItems added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                if (order == null)
                    return BadRequest("Order is null.");
                if (id != order.OrderId)
                    return BadRequest("ID mismatch.");

                var orderToUpdate = await _orderService.GetOrderByIdAsync(id);
                if (orderToUpdate == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                await _orderService.UpdateOrderAsync(order);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var orderToDelete = await _orderService.GetOrderByIdAsync(id);
                if (orderToDelete == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                await _orderService.DeleteOrderAsync(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteOrderItemByOrderOtemId/{id}")]
        public async Task<IActionResult> DeleteOrderItemByOrderOtemId(int id)
        {
            try
            {
                var isSuccess = await _orderService.DeleteOrderItemByOrderItemIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}

