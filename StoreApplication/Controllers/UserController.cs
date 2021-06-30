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
    public class UserController : Controller
    {
        Fetch Fetch = new Fetch();

        public IActionResult Index()
        {
            //Get Session Info
            
            if(HttpContext.Session.GetString("UserSession") != "0")
            {
                var user = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("UserSession"));
                return View(user);
            }
            else
            {
                /*
                List<Inventory> cart = new List<Inventory>();
                Inventory item = new Inventory();
                item.Amount = -1;
                item.ProductId = -1;
                item.LocationId = -1;
                cart.Add(item);
                HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));
                */
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult CreateAccount()
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

                return RedirectToAction("Menu", "Home");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public IActionResult GuestEntry()
        {
            Customer user = new Customer();
            user.CustomerId = 20;
            user.FirstName = "Guest";
            user.LastName = "User";
            user.UserName = "VOID";
            user.UserEmail = "VOID@VOID";
            user.UserPassword = "VOID";
            user.Age = 0;
            //user.SecretUser = 0;
            user.DefaultStore = 1;
            
            Location location = Fetch.LocationInfo(2);
            List<Inventory> cart = new List<Inventory>();

            HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));
            HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(user));
            HttpContext.Session.SetString("SessionLocation", JsonConvert.SerializeObject(location));
            
            return RedirectToAction("Menu", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(0));
            List<Inventory> cart = new List<Inventory>();
            HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult PopulateAccount(Customer newAccount)
        {
            Fetch.AddUserAccount(newAccount);
            return RedirectToAction("Login", "User");
        }

        public ActionResult OrderHistory()
        {
            var user = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("UserSession"));
            return View(Fetch.OrderHistoryByUser(user.CustomerId));
        }

        public ActionResult Search(string name)
        {
            ViewBag.Name = name;
            return View(Fetch.UserNamesByFirstName(name));
        }
    }
}
