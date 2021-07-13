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
    public class HiddenController : Controller
    {

        Fetch Fetch = new Fetch();

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
        public IActionResult SecretMenu()
        {
            return View();
        }

        public IActionResult Identification(string name)
        {
            ViewBag.Message = "Hello " + name + "!";

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserVerification(string password, string username)
        {

            Customer user = new Customer();
            Location location = new Location();
            user = Fetch.IfValidUser(username, password);
            location = Fetch.LocationInfo((int)user.DefaultStore);

            if (user != null)
            {

                HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(user, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
                ));
                HttpContext.Session.SetString("SessionLocation", JsonConvert.SerializeObject(location, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
                ));
                //HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(user));
                //HttpContext.Session.SetString("SessionLocation", JsonConvert.SerializeObject(location));

                List<Inventory> cart = new List<Inventory>();
                HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));

                return RedirectToAction("SecretMenu", "Hidden");
            }
            else
            {
                return RedirectToAction("Identification", "Hidden");
            }
        }
    }
}
