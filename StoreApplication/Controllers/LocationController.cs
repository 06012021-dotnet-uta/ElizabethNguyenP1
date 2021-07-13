using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StoreApplication.Models;
using P0DbContext;
using P1;

namespace StoreApplication.Controllers
{
    public class LocationController : Controller
    {
        Fetch Fetch = new Fetch();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectLocation()
        {

            return View(Fetch.ListOfLocations());
        }

        public IActionResult UpdateLocation(int LocationId, string button)
        {
            switch(button)
            {
                case "Update Location":
                    //Empty Cart HERE
                    List<Inventory> cart = new List<Inventory>();
                    HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));

                    Location location = Fetch.LocationInfo(LocationId);
                    HttpContext.Session.SetString("SessionLocation", JsonConvert.SerializeObject(location));

                    return RedirectToAction("Menu", "Home");
                    break;
                
                case "Update Default":
                    Customer user = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("UserSession"));
                    Fetch.UpdateDefaultLocation(LocationId, user);
                    HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(user));
                    return RedirectToAction("Index", "User");
                    break;
                
                default:
                    //Log Error
                    break;
            }
            return RedirectToAction("SelectLocation", "Location");

        }

        public ActionResult OrderHistory()
        {
            var location = JsonConvert.DeserializeObject<Location>(HttpContext.Session.GetString("SessionLocation"));
            return View(Fetch.OrderHistoryByLocation(location.LocationId));
        }
    }
}
