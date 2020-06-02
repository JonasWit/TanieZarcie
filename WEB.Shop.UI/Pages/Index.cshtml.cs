using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
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

        public Dictionary<string, int> Products { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ProductsOnSale { get; set; } = new Dictionary<string, int>();
        public List<GetNews.Response> News { get; set; }
        public List<ShopViewModel> Shops { get; set; } = new List<ShopViewModel>();

        public class ShopViewModel
        {
            public string Name { get; set; }
            public string SmallImagePath { get; set; } = null;
            public string LargeImagePath { get; set; } = null;
        }

        public void OnGet([FromServices] GetProducts getProducts, [FromServices] GetNews getNews)
        {
            News = getNews.Do().ToList();

            foreach (Shops shop in (Shops[])Enum.GetValues(typeof(Shops)))
            {
                if (getProducts.CountProductOnSaleForShop(shop.ToString()) != 0)
                {
                    var shopVm = new ShopViewModel { Name = shop.ToString(), SmallImagePath = $"{shop}-s.jpg", LargeImagePath = $"{shop}.jpg" };
                    Products.Add(shop.ToString(), getProducts.CountProductForShop(shop.ToString()));
                    ProductsOnSale.Add(shop.ToString(), getProducts.CountProductOnSaleForShop(shop.ToString()));
                    Shops.Add(shopVm);
                }
            }
        }

        //public IActionResult OnPostAllShops() => RedirectToPage("ProductsOverview", new { selectedShop = "Wszystkie" });

        public IActionResult OnGetShop(string shop) => RedirectToPage("ProductsOverview", new { selectedShop = shop });

        //public IActionResult OnGetBiedronka() => RedirectToPage("ProductsOverview", new { selectedShop = "Biedronka" });

        //public IActionResult OnGetZabka() => RedirectToPage("ProductsOverview", new { selectedShop = "Zabka" });

        //public IActionResult OnGetLidl() => RedirectToPage("ProductsOverview", new { selectedShop = "Lidl" });

        //public IActionResult OnGetKaufland() => RedirectToPage("ProductsOverview", new { selectedShop = "Kaufland" });

        //public IActionResult OnGetCarrefour() => RedirectToPage("ProductsOverview", new { selectedShop = "Carrefour" });

        //public IActionResult OnGetAuchan() => RedirectToPage("ProductsOverview", new { selectedShop = "Auchan" });

        //public IActionResult OnGetObi() => RedirectToPage("ProductsOverview", new { selectedShop = "Obi" });

        //public IActionResult OnGetLeroyMerlin() => RedirectToPage("ProductsOverview", new { selectedShop = "LeroyMerlin" });

    }
}