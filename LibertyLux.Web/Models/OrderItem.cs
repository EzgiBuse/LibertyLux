using LibertyLux.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Web.Models
{
    public class OrderItem
    {
       
        public int OrderItemId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public double MenuItemPrice { get; set; }

        public int OrderId { get; set; }

        public MenuItem MenuItem { get; set; }
    }
}
