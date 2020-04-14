using Microsoft.AspNetCore.Mvc;

namespace WEB.Shop.UI.Controllers
{
    public class SummariesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}