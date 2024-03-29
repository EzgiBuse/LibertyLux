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
    public class MenuRepository : GenericRepository<MenuItem>, IMenuRepository 
    {
        private readonly ApplicationDbContext _dbContext;

        public MenuRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MenuItem>> GetByCategoryAsync(int category)
        {
            return await _dbContext.Set<MenuItem>().Where(item => ((int)item.Category) == category).ToListAsync();
        }

    }

}

