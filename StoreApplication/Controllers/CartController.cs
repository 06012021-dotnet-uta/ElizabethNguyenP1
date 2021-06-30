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
    public class CartController : Controller
    {
        Fetch Fetch = new Fetch();
        public IActionResult Index()
        {
            //Get Cart info
            var cart = JsonConvert.DeserializeObject<List<Inventory>>(HttpContext.Session.GetString("SessionCart"));
            ViewBag.TotalCost = Fetch.TotalCost(Fetch.DetailedCart(cart));
            return View(Fetch.DetailedCart(cart));
        }

        public ActionResult AddCart(int amount, int productID)
        {
            Inventory updateCartItem = new Inventory();
            //Location currentLocation = JsonConvert.DeserializeObject<Location>(HttpContext.Session.GetString("SessionLocation"));
            //updateCartItem.LocationId = currentLocation.LocationId;
            updateCartItem.ProductId = productID;
            updateCartItem.Amount = amount;
            bool InCart = false;

            List<Inventory> cart = JsonConvert.DeserializeObject<List<Inventory>>(HttpContext.Session.GetString("SessionCart"));
            foreach(var item in cart)
            {
                if(item.ProductId == productID)
                {
                    item.Amount += amount;
                    InCart = true;
                }
            }
            
            if(InCart == false)
            {
                cart.Add(updateCartItem);
            }

            //Set the value into a session key
            HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index","Cart");
        }

        public ActionResult UpdateCart(int amount, int productID)
        {
            List<Inventory> cart = JsonConvert.DeserializeObject<List<Inventory>>(HttpContext.Session.GetString("SessionCart"));

            List<int> indexToRemove = new List<int>();
            var index = 0;
            foreach (var item in cart)
            {
                if (item.ProductId == productID)
                {
                    item.Amount -= amount;
                    if(item.Amount == 0)
                    {
                        indexToRemove.Add(index);
                    }
                    index++;
                }
            }
            foreach (var i in indexToRemove)
            {
                cart.RemoveAt(i);
            }

            HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));
            
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Checkout()
        {
            if (HttpContext.Session.GetString("UserSession") != "0")
            {
                List<Inventory> cart = JsonConvert.DeserializeObject<List<Inventory>>(HttpContext.Session.GetString("SessionCart"));

                //Create Order Record
                Order CurrentOrder = new Order();
                Location CurrentLocation = JsonConvert.DeserializeObject<Location>(HttpContext.Session.GetString("SessionLocation"));
                Customer CurrentCustomer = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("UserSession"));
                CurrentOrder.LocationId = CurrentLocation.LocationId;
                CurrentOrder.OrderDate = DateTime.Now;
                if (CurrentCustomer.FirstName == "Guest")
                {
                    CurrentOrder.CustomerId = 20;
                }
                else
                {
                    CurrentOrder.CustomerId = CurrentCustomer.CustomerId;
                }

                //Decrement Inventory & Add Order to DB
                Order mostRecentOrder = Fetch.AddOrder(CurrentOrder);
                Fetch.ProcessOrder(mostRecentOrder, cart);

                //Empty Cart
                cart = new List<Inventory>();
                HttpContext.Session.SetString("SessionCart", JsonConvert.SerializeObject(cart));

                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
                
        }
    }
}
