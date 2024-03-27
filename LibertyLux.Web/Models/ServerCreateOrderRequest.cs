using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace LibertyLux.Web.Models
{
    public class ServerCreateOrderRequest
    {
        public int MenuItemId { get; set; }
        public string  name { get; set; }
        public string  image { get; set; }
        public string  price { get; set; }
        public string  quantity { get; set; }

    }
    }

