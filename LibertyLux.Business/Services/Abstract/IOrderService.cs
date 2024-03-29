using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Business.Services.Abstract
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> GetOrderByOrderItemIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<List<Order>> GetOrdersByTableAsync(int tableId);
        Task<List<OrderItem>> GetAllOrderDetailsByTableAsync(int tableId);
        Task DeleteOrderAsync(int id);
        Task<bool> DeleteOrderItemByOrderItemIdAsync(int id);
        Task<List<OrderItem>> GetAllOrderItemsAsync();
        Task<bool> AddOrderItemsAsync(List<OrderItem> orderItems);
    }
}
