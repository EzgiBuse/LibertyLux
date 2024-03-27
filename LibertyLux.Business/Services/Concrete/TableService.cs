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
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<RestaurantTable>> GetAllTablesAsync()
        {
            try
            {
                return await _tableRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public async Task<RestaurantTable> GetTableByIdAsync(int id)
        {
            try
            {
                return await _tableRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public async Task AddTableAsync(RestaurantTable table)
        {
            try
            {
                await _tableRepository.AddAsync(table);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public async Task UpdateTableAsync(RestaurantTable table)
        {
            try
            {
                await _tableRepository.UpdateAsync(table);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }

        public async Task DeleteTableAsync(int id)
        {
            try
            {
                await _tableRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }
    }

}
