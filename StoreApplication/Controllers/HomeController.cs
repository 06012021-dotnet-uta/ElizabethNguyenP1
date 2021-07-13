using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using P0DbContext;
using P1;

namespace StoreApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Fetch Fetch = new Fetch();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //Initializing Starting Session Objects
            HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(0));
            List<Inventory> cart = new List<Inventory>();
            HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));

            HttpContext.Session.SetString("SessionLocation", JsonConvert.SerializeObject(Fetch.LocationInfo(2)));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Menu()
        {
            Location currentLocation = JsonConvert.DeserializeObject<Location>(HttpContext.Session.GetString("SessionLocation"));
            ViewBag.StoreLocation = currentLocation.LocationName;
            return View(Fetch.DetailedInventory(currentLocation.LocationId));
        }
    }
}
