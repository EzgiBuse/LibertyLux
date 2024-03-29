using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Web.Models;
    public class OrderCreateDto
    {
        public int MenuItemId { get; set; }
        public double MenuItemPrice { get; set; }
        public int Quantity { get; set; }
        
            
            
    }

