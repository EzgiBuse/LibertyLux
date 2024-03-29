using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibertyLux.Entity;

namespace LibertyLux.Business.Services.Abstract
{
    public interface IMenuService
    {
        Task<List<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(int id);
        Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int category);
    }
}
