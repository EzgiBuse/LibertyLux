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
        Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableId);
        Task<IEnumerable<OrderItem>> GetAllOrderDetailsForTableAsync(int tableId);
        // Additional methods specific to order operations can be added here
        
       Task<Order> GetByOrderItemIdAsync(int orderItemId);

        Task<bool> DeleteOrderItemByOrderItemIdAsync(int orderItemId);
    }
}
