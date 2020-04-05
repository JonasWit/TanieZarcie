using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Shop.UI.Controllers
{
    public class NewsController : Controller
    {
        //todo JW - v1.1 - zrobic ta funkcjonalnosc tak jak w blogu
        public IActionResult Index()
        {
            return View();
        }
    }
}