using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WEB.Shop.Application.Products;
using WEB.Shop.UI.ViewModels.Summaries;

namespace WEB.Shop.UI.Controllers
{
    public class SummariesController : Controller
    {
        public IActionResult Index([FromServices] GetProducts getProducts)
        {
            var vm = new SummariesIndexViewModel();
            var products = getProducts.GetAllProducts();

            vm.DiscountsFood = products.Where(x => x.Category == "Markety Spożywcze").OrderByDescending(item => item.Discount).Take(15).ToList();
            vm.DiscountsDiy = products.Where(x => x.Category == "Markety Budowlane").OrderByDescending(item => item.Discount).Take(15).ToList();

            return View(vm);
        }
    }
}