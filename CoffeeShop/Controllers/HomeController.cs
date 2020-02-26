using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Transactions;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //these private fields will be used as a way to load in the user and items data
        private List<Items> itemList;
        private List<Users> userList;
        private CoffeeShopContext db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //GetData();
        }

        public async Task<IActionResult> Index()
        {
            await GetData();
            //AddItems(new Items());

            //make a session object and store data in it for later retrieval
            HttpContext.Session.SetString("TempKey", "Hello World");
            //use get string to retrieve the data in the session
            var testSession = HttpContext.Session.GetString("TempKey");

            return View(itemList);
        }

        //this method will load in my DB data
        private async Task GetData()
        {
            db = new CoffeeShopContext();

            //call the items table and pull in the data, to hold in our private field
            //first called the items table
            itemList = await GetItems();
            //now call the users table
            userList = db.Users.ToList();

        }

        //make a private method to call our CoffeeShop API and get a list of items
        private async Task<List<Items>> GetItems()
        {
            var stringResponse = "";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient
                    .GetAsync("https://localhost:44356/api/coffeeshop/getitems"))
                {
                    stringResponse = await response.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<List<Items>>(stringResponse);

                }
            }
        }

        private async void AddItems(Items item)
        {
            item.id = 10;
            item.name = "Transaction RollBack 2";
            item.quantity = 20;

            var tempItemJson = JsonSerializer.Serialize(item);


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient
                    .GetAsync($"https://localhost:44356/api/coffeeshop/additems?item={tempItemJson}"))
                {

                }
            }
        }

        public async Task<IActionResult> DeleteItem(string itemID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient
                    .GetAsync($"https://localhost:44356/api/coffeeshop/deleteitem?itemID={itemID}"))
                {

                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            //use get string to retrieve the data in the session
            var testSession = HttpContext.Session.GetString("TempKey");
            var user = User.Identity;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
