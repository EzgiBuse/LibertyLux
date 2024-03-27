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
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> GetOrderByOrderItemIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableId);
        Task<IEnumerable<OrderItem>> GetAllOrderDetailsByTableAsync(int tableId);
        Task DeleteOrderAsync(int id);
        Task<bool> DeleteOrderItemByOrderItemIdAsync(int id);
    }
}
