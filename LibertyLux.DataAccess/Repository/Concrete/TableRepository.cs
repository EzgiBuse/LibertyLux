using LibertyLux.DataAccess.DbContext;
using LibertyLux.DataAccess.Repository.Generic;
using LibertyLux.DataAccess.Repository.Menu;
using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.DataAccess.Repository.Concrete
{
    public class TableRepository : GenericRepository<RestaurantTable>, ITableRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TableRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // If you want to add any additional methods specific to tables, you can do so here.
    }
}
