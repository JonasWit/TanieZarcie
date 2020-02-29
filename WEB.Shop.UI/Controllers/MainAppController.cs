using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Shop.UI.Controllers
{
    //todo:TEMPORARY
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class MainAppController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Index()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
