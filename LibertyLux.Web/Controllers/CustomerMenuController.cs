using LibertyLux.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibertyLux.Web.Controllers
{
    public class CustomerMenuController : Controller
    {
          private readonly HttpClient _httpClient;

            public CustomerMenuController()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://localhost:7116/"); 
            }

        public async Task<IActionResult> Index(string category = null)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            
            if (string.IsNullOrEmpty(category) || category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return await GetAllItems();
            }

            return await FilterByCategory(category);
        }

        public async Task<ActionResult> GetAllItems()
        {
           
            List<MenuItem> menuItems = new List<MenuItem>();

            HttpResponseMessage response = await _httpClient.GetAsync("api/Menu/all");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(responseData, new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new MenuItemCategoryConverter() }
                });
            }
            return View("Index", menuItems);
        }


        public class MenuItemCategoryConverter : JsonConverter<MenuItemCategory>
        { // Custom JSON converter for MenuItemCategory enum
            public override MenuItemCategory ReadJson(JsonReader reader, Type objectType, MenuItemCategory existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Integer)
                {
                    int categoryValue = Convert.ToInt32(reader.Value);
                    if (Enum.IsDefined(typeof(MenuItemCategory), categoryValue))
                    {
                        return (MenuItemCategory)categoryValue;
                    }
                }

                return MenuItemCategory.Hamburger; // Default value 
            }

            public override void WriteJson(JsonWriter writer, MenuItemCategory value, JsonSerializer serializer)
            {
                writer.WriteValue((int)value);
            }
        }

       
            public async Task<IActionResult> FilterByCategory(string category)
            {
           
            List<MenuItem> menuItems = new List<MenuItem>();

            if (!Enum.TryParse(category, true, out MenuItemCategory categoryEnum))
            {
                
                return BadRequest("Invalid category string");
            }

            
            int index = (int)categoryEnum;

            HttpResponseMessage response = await _httpClient.GetAsync($"api/menu/category/{index}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

               
                menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(responseData, new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new MenuItemCategoryConverter() }
                });
            }
            else
            {
               
                return View("Error");
            }

            return View("Index", menuItems);

        }
      


    }
}
