using LibertyLux.DataAccess.DbContext;
using LibertyLux.DataAccess.Repository.Generic;
using LibertyLux.DataAccess.Repository.Menu;
using LibertyLux.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.DataAccess.Repository.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderDetailsForTableAsync(int tableId)
        {
            try
            {
                var orderItemsForTable = await _dbContext.Orders
             .Where(o => o.TableId == tableId)
             .SelectMany(o => o.OrderItems)
             .ToListAsync();

                return orderItemsForTable;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<OrderItem>();
            }
        }


        public async Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableId)
        {
            try
            {
                return await _dbContext.Orders.Where(order => order.TableId == tableId).ToListAsync();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Order>();
            }
        }
        
            public async Task<bool> DeleteOrderItemByOrderItemIdAsync(int orderItemId)
             {
            var orderItem = await _dbContext.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                _dbContext.OrderItems.Remove(orderItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
             }


            public async Task<Order> GetByOrderItemIdAsync(int orderItemId)
           {
            try
            {
                // Find the OrderItem to get its OrderId
                var orderItem = await _dbContext.OrderItems
                    .AsNoTracking() // Use AsNoTracking for read-only operations for better performance
                    .FirstOrDefaultAsync(oi => oi.OrderItemId == orderItemId);

                if (orderItem == null)
                {
                   
                    return null;
                }

                //OrderId to find the Order
                var order = await _dbContext.Orders
                    .AsNoTracking() 
                    .FirstOrDefaultAsync(o => o.OrderId == orderItem.OrderId);

               
                return order;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
