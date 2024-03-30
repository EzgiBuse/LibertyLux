using LibertyLux.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using static LibertyLux.Web.Controllers.CustomerMenuController;

namespace LibertyLux.Web.Controllers
{

    public class ServerController : Controller
    {
        private readonly HttpClient _httpClient;

        public ServerController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116/");
        }


        public async Task<IActionResult> Index()
        {

            List<RestaurantTable> restaurantTables = new List<RestaurantTable>();
            HttpResponseMessage response = await _httpClient.GetAsync("api/Tables");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                restaurantTables = JsonConvert.DeserializeObject<List<RestaurantTable>>(responseData);
            }
           
            
            return View(restaurantTables);

        }




        public async Task<IActionResult> ServerOrder(int tableId)
        {
            try
            {
                List<MenuItem> menuItems = new List<MenuItem>();
                List<MenuItem> sortedMenuItems = new List<MenuItem>();
                HttpResponseMessage response = await _httpClient.GetAsync("api/Menu/all");
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(responseData);
                    sortedMenuItems = menuItems.OrderBy(item => item.Category).ToList();
                    TempData["TableId"] = tableId;
                    return View("ServerOrderCreationView", sortedMenuItems);
                }

                return View("Index");


            }
            catch (Exception e)
            {

                return View("Index");

            }

        }

        public async Task<IActionResult> ServerCreateOrder(int tableId, string requestmodel)
        {
            try
            { //Mapping the request model to the OrderItem model and Creating an Order
                var order = new Order
                {

                    TableId = tableId,
                    Status = Status.Pending,
                    OrderItems = null
                };

                List<OrderItem> items = new List<OrderItem>();
                List<OrderCreateDto> orderCreateDtos = new List<OrderCreateDto>();
                var requestOrder = JsonConvert.DeserializeObject<List<ServerCreateOrderRequest>>(requestmodel);
                foreach (var item in requestOrder)
                {
                   
                    var orderCreateDto = new OrderCreateDto
                    {
                        MenuItemId = item.MenuItemId,
                        MenuItemPrice = double.Parse(item.price),
                        Quantity = int.Parse(item.quantity)
                    };
                    orderCreateDtos.Add(orderCreateDto);
                }
                var jsonContent = JsonConvert.SerializeObject(orderCreateDtos);
                 var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"api/Orders/AddOrder/{tableId}", contentString);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Order added succesfully";
                    
                    return RedirectToAction("Index");
                    
                }
                else
                {
                    
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                return View("Index");
            }
           
        } 




        public async Task<IActionResult> GetAllOrderDetailsForTable(int tableId)
        {
            try
            {
                // Fetch all orders for the table to get statuses
                HttpResponseMessage ordersResponse = await _httpClient.GetAsync($"api/Orders/tableorders/{tableId}");
                Dictionary<int, string> orderStatuses = new Dictionary<int, string>();
                if (ordersResponse.IsSuccessStatusCode)
                {
                    var ordersData = await ordersResponse.Content.ReadAsStringAsync();
                    var orders = JsonConvert.DeserializeObject<List<Order>>(ordersData);
                    // Convert statuses to string and map them by OrderId
                    orderStatuses = orders.ToDictionary(o => o.OrderId, o => Enum.GetName(typeof(Status), o.Status));
                }

                // Fetch all order items for the table
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Orders/tableorderdetails/{tableId}");
                List<OrderItemViewModel> viewModelList = new List<OrderItemViewModel>();
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var orderDetails = JsonConvert.DeserializeObject<List<OrderItem>>(responseData);

                    // Fetch all menu items to get the names
                    HttpResponseMessage menuResponse = await _httpClient.GetAsync("api/Menu/all");
                    Dictionary<int, string> menuItemNames = new Dictionary<int, string>();
                    if (menuResponse.IsSuccessStatusCode)
                    {
                        var menuResponseData = await menuResponse.Content.ReadAsStringAsync();
                        var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(menuResponseData);
                        menuItemNames = menuItems.ToDictionary(mi => mi.MenuItemId, mi => mi.Name);
                    }

                    // Map order details to the new ViewModel, including MenuItemName and Status
                    viewModelList = orderDetails.Select(oi =>
                    {
                        menuItemNames.TryGetValue(oi.MenuItemId, out var menuItemName);
                        orderStatuses.TryGetValue(oi.OrderId, out var status);

                        return new OrderItemViewModel
                        {
                            OrderItemId = oi.OrderItemId,
                            Quantity = oi.Quantity,
                            MenuItemPrice = oi.MenuItemPrice,
                            MenuItemName = menuItemName ?? "Unknown",
                            Status = status ?? "Status Unknown",
                            TableId = tableId
                        };
                    }).ToList();
                }

                // Return a partial view containing the mapped order details
                return PartialView("_OrderDetailsPartialView", viewModelList);
            }
            catch (Exception e)
            {

                return PartialView("_OrderDetailsPartialView", new List<OrderItemViewModel>());
            }
        }

      
        public async Task<IActionResult> ApproveOrder(int orderItemId)
        {
            
            try
            {

                
                var requestUri = $"api/Orders/ApproveOrderByOrderItemId/{orderItemId}";
                var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                var updateResponse = await _httpClient.PutAsync(requestUri, requestContent);

                if (!updateResponse.IsSuccessStatusCode)
                {
                  
                    return BadRequest();
                }



                // Redirect or return success
                return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

       
        public async Task<IActionResult> CancelOrderItemByOrderItemId(int orderItemId)
        {
            
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Orders/DeleteOrderItemByOrderOtemId/{orderItemId}");
               
                if (!response.IsSuccessStatusCode)
                {

                    return BadRequest();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
