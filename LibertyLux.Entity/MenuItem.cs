using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.Entity
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }
        public double Price { get; set; }
        public MenuItemCategory Category { get; set; }
        
    }

    public enum MenuItemCategory
    {
        Hamburger,
        Pizza,
        MainDish,
        SideDish,
        Salad,
        Pasta,
        Dessert,
        HotDrink,
        ColdDrink,
        AlcoholicDrink
    }
}
