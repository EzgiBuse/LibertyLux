using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Web.Models
{
    public class RestaurantTable
    {
       
        public int TableId { get; set; }
       
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public int Occupancy { get; set; }
       
    }
}
