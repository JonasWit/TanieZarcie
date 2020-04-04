using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Shop.Application.Products;
using WEB.Shop.Application.Session;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string ShopDirection { get; set; }

        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public void OnGet([FromServices] GetProducts getProducts) => Products = getProducts.GetAllProducts();

        public IActionResult OnPostAllShops([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Wszystkie");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnPostBiedronka([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Biedronka");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnPostLidl([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Lidl");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnPostKaufland([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Kaufland");
            return RedirectToPage("MainProducts");
        }
    }
}