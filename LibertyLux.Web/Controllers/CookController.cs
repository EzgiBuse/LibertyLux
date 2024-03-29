using LibertyLux.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace LibertyLux.Web.Controllers
{
    public class CookController : Controller
    {
        private readonly HttpClient _httpClient;

        public CookController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116/");
        }

        public async Task<IActionResult> Index()
        {

            // Status that will be filtered out
            var nonDisplayStatuses = new[] { Status.Cancelled, Status.Served, Status.Ready };

            //API call to get all orders
            HttpResponseMessage responseOrder = await _httpClient.GetAsync("api/Orders");
            string responseOrderData = await responseOrder.Content.ReadAsStringAsync();
            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(responseOrderData);

            //List of orders that will be shown
            List<Order> waitingOrders = orders.Where(order => !nonDisplayStatuses.Contains(order.Status)).ToList();

            //List of order items that will be shown
            List<OrderItem> waitingOrderItems = new List<OrderItem>();

            //API call to get all order items
            HttpResponseMessage response = await _httpClient.GetAsync("api/Orders/GetAllOrderItems");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                List<OrderItem> orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(responseData);

                //API call to get all menu items
                HttpResponseMessage responseMenu = await _httpClient.GetAsync("api/Menu/all");
                if (responseMenu.IsSuccessStatusCode)
                {
                    string responseMenuData = await responseMenu.Content.ReadAsStringAsync();
                    List<MenuItem> menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(responseMenuData);

                    //Assigning menu items to order items
                    foreach (OrderItem orderItem in orderItems)
                    {
                        orderItem.MenuItem = menuItems.FirstOrDefault(item => item.MenuItemId == orderItem.MenuItemId);
                    }

                    //Creating a list of orderids that will be shown
                    List<int> orderIds = new List<int>();
                    foreach (Order order in waitingOrders)
                    {
                        orderIds.Add(order.OrderId);
                    }

                    //Creating a list of order items that will be shown
                    foreach (OrderItem orderItem in orderItems)
                    {
                        if (orderIds.Contains(orderItem.OrderId))
                        {
                            waitingOrderItems.Add(orderItem);
                        }
                    }
                }



                return View("Index", waitingOrderItems);
            }
            List<OrderItem> emptyList = new List<OrderItem>();
            return View("Index", emptyList);
        }

        public async Task<IActionResult> UpdateOrderStatus(int orderItemId,string status)
        {
            try
            {
                if(status == "Cancelled")
                {
                    var requestUriCancel = $"api/Orders/DeleteOrderItemByOrderOtemId/{orderItemId}";
                    var requestCancel = new HttpRequestMessage(HttpMethod.Delete, requestUriCancel);

                    HttpResponseMessage responseCancel = await _httpClient.SendAsync(requestCancel);
                    return RedirectToAction("Index");
                }

                var requestUri = $"api/Orders/UpdateOrderStatusByOrderItemId/{orderItemId}/{status}";
                var request = new HttpRequestMessage(HttpMethod.Put, requestUri);

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
