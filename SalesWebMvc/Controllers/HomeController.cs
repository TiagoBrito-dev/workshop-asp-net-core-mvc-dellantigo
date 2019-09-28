using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Project SalesWebMvc";
            ViewData["Message2"] = "Created by Tiago Brito";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Tiago Brito";
            ViewData["Facebook"] = "https://web.facebook.com/tiago.brito.94801";
            ViewData["Instagram"] = "https://www.instagram.com/tiago_brievan/?hl=pt-br";
            ViewData["SocialMidia"] = "Social Midias:";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
