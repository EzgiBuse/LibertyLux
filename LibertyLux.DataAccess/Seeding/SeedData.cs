using LibertyLux.DataAccess.DbContext;
using LibertyLux.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyLux.DataAccess.Seeding
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.MenuItems.Any())
            {
                var menuItems = new List<MenuItem>
                {
                   new MenuItem
                    {
                        Name = "Margherita Pizza",
                        Description = "Classic Italian pizza topped with fresh tomato sauce, mozzarella cheese, and basil leaves, baked to perfection in a wood-fired oven.",
                        Price = 10.99,
                        Category = MenuItemCategory.Pizza,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Cheeseburger",
                        Description = "Juicy beef patty topped with melted cheddar cheese, crisp lettuce, ripe tomatoes, and tangy pickles, served on a toasted sesame seed bun.",
                        Price = 9.99,
                        Category = MenuItemCategory.Hamburger,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Pepperoni Pizza",
                        Description = "Classic pizza topped with spicy pepperoni slices and gooey mozzarella cheese, baked to perfection.",
                        Price = 11.99,
                        Category = MenuItemCategory.Pizza,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Bacon Cheeseburger",
                        Description = "Savory beef patty topped with crispy bacon, melted American cheese, lettuce, tomato, and tangy mayonnaise, served on a toasted bun.",
                        Price = 10.99,
                        Category = MenuItemCategory.Hamburger,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Spaghetti Bolognese",
                        Description = "Traditional Italian pasta dish with al dente spaghetti noodles, savory Bolognese sauce made with ground beef, onions, and tomatoes, garnished with Parmesan cheese.",
                        Price = 12.99,
                        Category = MenuItemCategory.Pasta,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Caesar Salad",
                        Description = "Crisp romaine lettuce tossed in creamy Caesar dressing, topped with crunchy croutons and shaved Parmesan cheese.",
                        Price = 8.99,
                        Category = MenuItemCategory.Salad,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Chocolate Brownie Sundae",
                        Description = "Decadent chocolate brownie served warm with a scoop of vanilla ice cream, drizzled with chocolate syrup and topped with whipped cream and a cherry.",
                        Price = 7.99,
                        Category = MenuItemCategory.Dessert,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Iced Caramel Macchiato",
                        Description = "Refreshing iced coffee beverage made with espresso, milk, and caramel syrup, topped with whipped cream.",
                        Price = 5.49,
                        Category = MenuItemCategory.ColdDrink,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Classic Cheese Pizza",
                        Description = "Traditional pizza topped with rich tomato sauce and gooey mozzarella cheese, baked to perfection.",
                        Price = 9.99,
                        Category = MenuItemCategory.Pizza,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Double Cheeseburger",
                        Description = "Juicy beef patties topped with two slices of melted American cheese, lettuce, tomato, and pickles, served on a toasted sesame seed bun.",
                        Price = 12.99,
                        Category = MenuItemCategory.Hamburger,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Fettuccine Alfredo",
                        Description = "Creamy Alfredo sauce tossed with tender fettuccine noodles, garnished with chopped parsley and Parmesan cheese.",
                        Price = 13.99,
                        Category = MenuItemCategory.Pasta,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "Greek Salad",
                        Description = "Fresh salad featuring crisp lettuce, tomatoes, cucumbers, red onions, Kalamata olives, and crumbled feta cheese, drizzled with Greek vinaigrette.",
                        Price = 9.49,
                        Category = MenuItemCategory.Salad,
                        Image = ""
                    },
                    new MenuItem
                    {
                        Name = "New York Cheesecake",
                        Description = "Creamy and indulgent cheesecake with a graham cracker crust, topped with your choice of fruit compote or caramel sauce.",
                        Price = 8.49,
                        Category = MenuItemCategory.Dessert,
                        Image = ""
                    },





                };

                context.MenuItems.AddRange(menuItems);
                context.SaveChanges();
            }
        }
    }
}
