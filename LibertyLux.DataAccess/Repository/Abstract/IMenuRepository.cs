using LibertyLux.DataAccess.Repository.Generic;
using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.DataAccess.Repository.Menu
{
    public interface IMenuRepository : IGenericRepository<MenuItem>
    {
        Task<IEnumerable<MenuItem>> GetByCategoryAsync(int category);
    }
}
