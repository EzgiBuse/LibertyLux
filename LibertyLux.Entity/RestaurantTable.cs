using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Entity
{
    public class RestaurantTable
    {
        [Key]
        public int TableId { get; set; }
        [Required]
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public int Occupancy { get; set; }
        // Additional properties as needed
    }
}
