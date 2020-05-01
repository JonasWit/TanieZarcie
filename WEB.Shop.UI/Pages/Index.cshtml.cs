using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Enums;
using WEB.Shop.Application.News;
using WEB.Shop.Application.Products;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string ShopDirection { get; set; }

        public Dictionary<string, int> Products { get; set; }
        public Dictionary<string, int> ProductsOnSale { get; set; }
        public List<GetNews.Response> News { get; set; }

        public List<string> Shops { get; set; }

        public void OnGet([FromServices] GetProducts getProducts, [FromServices] GetNews getNews)
        {
            Products = new Dictionary<string, int>();
            ProductsOnSale = new Dictionary<string, int>();
            Shops = new List<string>();
            News = getNews.Do().ToList();

            foreach (Shops shop in (Shops[])Enum.GetValues(typeof(Shops)))
            {
                Products.Add(shop.ToString(), getProducts.CountProductForShop(shop.ToString()));
                ProductsOnSale.Add(shop.ToString(), getProducts.CountProductOnSaleForShop(shop.ToString()));
                Shops.Add(shop.ToString());
            }
        }

        public IActionResult OnPostAllShops() => RedirectToPage("ProductsOverview", new { selectedShop = "Wszystkie" });

        public IActionResult OnGetBiedronka() => RedirectToPage("ProductsOverview", new { selectedShop = "Biedronka" });

        public IActionResult OnGetZabka() => RedirectToPage("ProductsOverview", new { selectedShop = "Zabka" });

        public IActionResult OnGetLidl() => RedirectToPage("ProductsOverview", new { selectedShop = "Lidl" });

        public IActionResult OnGetKaufland() => RedirectToPage("ProductsOverview", new { selectedShop = "Kaufland" });

        public IActionResult OnGetCarrefour() => RedirectToPage("ProductsOverview", new { selectedShop = "Carrefour" });

        public IActionResult OnGetAuchan() => RedirectToPage("ProductsOverview", new { selectedShop = "Auchan" });

    }
}