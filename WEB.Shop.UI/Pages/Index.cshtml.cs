using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Enums;
using WEB.Shop.Application.Products;
using WEB.Shop.Application.Session;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string ShopDirection { get; set; }

        public Dictionary<Shops, int> Products { get; set; }
        public Dictionary<Shops, int> ProductsOnSale { get; set; }

        public List<string> Shops { get; set; }

        public void OnGet([FromServices] GetProducts getProducts)
        {
            Products = new Dictionary<Shops, int>();
            ProductsOnSale = new Dictionary<Shops, int>();

            foreach (Shops shop in (Shops[])Enum.GetValues(typeof(Shops)))
            {
                Products.Add(shop, getProducts.CountProductForShop(shop.ToString()));
                ProductsOnSale.Add(shop, getProducts.CountProductOnSaleForShop(shop.ToString()));
            }
        }

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

        public IActionResult OnPostCarrefour([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Carrefour");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnPostAuchan([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Auchan");
            return RedirectToPage("MainProducts");
        }
    }
}