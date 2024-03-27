using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Entity
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal MenuItemPrice { get; set; } 

        public int OrderId { get; set; }

        [NotMapped] 
        public MenuItem MenuItem { get; set; }
    }
}
