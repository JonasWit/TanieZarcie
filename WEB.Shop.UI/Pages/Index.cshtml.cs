using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using WEB.Shop.Application.Enums;
using WEB.Shop.Application.Products;
using WEB.Shop.Application.Session;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string ShopDirection { get; set; }

        public Dictionary<string, int> Products { get; set; }
        public Dictionary<string, int> ProductsOnSale { get; set; }

        public List<string> Shops { get; set; }

        public void OnGet([FromServices] GetProducts getProducts)
        {
            Products = new Dictionary<string, int>();
            ProductsOnSale = new Dictionary<string, int>();
            Shops = new List<string>();

            foreach (Shops shop in (Shops[])Enum.GetValues(typeof(Shops)))
            {
                Products.Add(shop.ToString(), getProducts.CountProductForShop(shop.ToString()));
                ProductsOnSale.Add(shop.ToString(), getProducts.CountProductOnSaleForShop(shop.ToString()));
                Shops.Add(shop.ToString());
            }
        }

        public IActionResult OnPostAllShops([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Wszystkie");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnGetBiedronka([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Biedronka");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnGetLidl([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Lidl");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnGetKaufland([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Kaufland");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnGetCarrefour([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Carrefour");
            return RedirectToPage("MainProducts");
        }

        public IActionResult OnGetAuchan([FromServices] SaveSelectedShop saveSelectedShop)
        {
            saveSelectedShop.Do("Auchan");
            return RedirectToPage("MainProducts");
        }
    }
}