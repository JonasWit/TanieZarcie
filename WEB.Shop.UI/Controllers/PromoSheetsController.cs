using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WEB.Shop.Application.ShopAdmin;
using WEB.Shop.UI.ViewModels.PromoSheets;

namespace WEB.Shop.UI.Controllers
{
    public class PromoSheetsController : Controller
    {
        public IActionResult Index([FromServices] GetShops getShops) => View(new PromoSheetsViewModel { Shops = getShops.Do().ToList() });
    }
}