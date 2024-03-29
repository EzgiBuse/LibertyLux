using LibertyLux.DataAccess.Repository.Generic;
using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.DataAccess.Repository.Menu
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrdersByTableAsync(int tableId);
        Task<List<OrderItem>> GetAllOrderDetailsForTableAsync(int tableId);
       
       Task<Order> GetByOrderItemIdAsync(int orderItemId);

        Task<bool> DeleteOrderItemByOrderItemIdAsync(int orderItemId);

        Task<bool> AddOrderItemsAsync(List<OrderItem> orderItems);

        Task<List<OrderItem>> GetAllOrderItemsAsync();
    }
}
