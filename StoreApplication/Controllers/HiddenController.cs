using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.Controllers
{
    public class HiddenController : Controller
    {
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
    }
}
