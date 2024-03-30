using LibertyLux.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace LibertyLux.Web.Controllers
{
    public class CashierController : Controller
    {
        private readonly HttpClient _httpClient;

        public CashierController()
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
                return PartialView("_CashierOrderDetailsPartialView", viewModelList);
            }
            catch (Exception e)
            {

                return PartialView("_CashierOrderDetailsPartialView", new List<OrderItemViewModel>());
            }
        }

        
        public async Task<IActionResult> ApprovePayment(int tableId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Orders/DeleteOrdersAndOrderDetailsByTableId/{tableId}");
                
                if (response.IsSuccessStatusCode)
                {
                   
                    var requestUri = $"api/Orders/DeleteOrdersAndOrderDetailsByTableId/{tableId}/0";
                    var emptyContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                    HttpResponseMessage tableResponse = await _httpClient.PutAsync(requestUri, emptyContent);

                    return Json(new { success = true, message = "Payment approved successfully." });
                }
                else
                {
                    
                    return Json(new { success = false, message = "Failed to approve payment." });
                }

                
            }
            catch (Exception ex)
            {
                
               
                return Json(new { success = false, message = $"An error occurred while approving payment: {ex.Message}" });
            }
        }
    }
}
