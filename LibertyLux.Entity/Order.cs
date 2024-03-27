using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Entity
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public Status Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        // Additional properties as needed
    }

    public enum Status
    {
        Pending,
        InProgress,
        Ready,
        Served,
        Cancelled
    }
}
