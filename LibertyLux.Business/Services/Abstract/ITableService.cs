using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Business.Services.Abstract
{
    public interface ITableService
    {
        Task<IEnumerable<RestaurantTable>> GetAllTablesAsync();
        Task<RestaurantTable> GetTableByIdAsync(int id);
        Task AddTableAsync(RestaurantTable table);
        Task UpdateTableAsync(RestaurantTable table);
        Task DeleteTableAsync(int id);
    }
}
