using LibertyLux.Business.Services.Abstract;
using LibertyLux.DataAccess.Repository.Generic;
using LibertyLux.DataAccess.Repository.Menu;
using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Business.Services.Concrete
{
    public class MenuService : IMenuService
    {
            private readonly IMenuRepository _menuItemRepository;

            public MenuService(IMenuRepository menuItemRepository)
            {
                _menuItemRepository = menuItemRepository;
            }

            public async Task<List<MenuItem>> GetAllMenuItemsAsync()
            {
                try
                {
                    return await _menuItemRepository.GetAllAsync();
                }
                catch (Exception ex)
                {
                   
                    throw;
                }
            }

            public async Task<MenuItem> GetMenuItemByIdAsync(int id)
            {
                try
                {
                    return await _menuItemRepository.GetByIdAsync(id);
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
            }

            public async Task AddMenuItemAsync(MenuItem menuItem)
            {
                try
                {
                    await _menuItemRepository.AddAsync(menuItem);
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
            }

            public async Task UpdateMenuItemAsync(MenuItem menuItem)
            {
                try
                {
                    await _menuItemRepository.UpdateAsync(menuItem);
                }
                catch (Exception ex)
                {
                   
                    throw;
                }
            }

            public async Task DeleteMenuItemAsync(int id)
            {
                try
                {
                    await _menuItemRepository.DeleteAsync(id);
                }
                catch (Exception ex)
                {
                   
                    throw;
                }
            }
        public async Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int category)
        {
            try
            {
                return await _menuItemRepository.GetByCategoryAsync(category);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
    }

    }
