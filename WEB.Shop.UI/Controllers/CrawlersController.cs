using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Shop.Application.Crawlers;

namespace WEB.Shop.UI.Controllers
{
    public class CrawlersController : Controller
    {
        private CrawlersCommander _commander;

        public CrawlersController(CrawlersCommander commander)
        {
            //todo: odpalic crawlera commandera


            _commander = commander;
        }

        public IActionResult Index()
        {
           


            return View(_commander);
        }



    }
}