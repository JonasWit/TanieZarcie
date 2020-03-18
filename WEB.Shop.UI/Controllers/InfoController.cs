using Microsoft.AspNetCore.Mvc;

namespace WEB.Shop.UI.Controllers
{
    public class InfoController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}