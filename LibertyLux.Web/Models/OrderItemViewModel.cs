namespace LibertyLux.Web.Models
{
    public class OrderItemViewModel
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public double MenuItemPrice { get; set; }
        public string MenuItemName { get; set; }
        public string Status { get; set; }
        public int TableId { get; set; }
    }

}
