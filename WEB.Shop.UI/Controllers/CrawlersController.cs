using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB.Shop.Application.Crawlers;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class CrawlersController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index([FromServices] CrawlersCommander commander)
        {
            return View(commander);
        }

        [HttpGet("Add")]
        public IActionResult Add([FromServices] CrawlersCommander commander)
        {
            commander.Add();

            return View("~/Views/Crawlers/Index.cshtml", commander);
        }

        [HttpGet("Remove")]
        public IActionResult Remove([FromServices] CrawlersCommander commander)
        {
            commander.Remove();

            return View("~/Views/Crawlers/Index.cshtml", commander);
        }

    }
}