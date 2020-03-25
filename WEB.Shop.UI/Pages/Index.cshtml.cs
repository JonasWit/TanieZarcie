using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string ShopDirection { get; set; }

        public void OnGet()
        {

        }

        [HttpPost]
        public IActionResult OnPostAllShops()
        {
            return RedirectToPage("MainProducts", new { selectedShop = "All" });
        }

        [HttpPost]
        public IActionResult OnPostBiedronka()
        {
            return RedirectToPage("MainProducts", new { selectedShop = "Biedronka" });
        }

        [HttpPost]
        public IActionResult OnPostLidl()
        {
            return RedirectToPage("MainProducts", new { selectedShop = "Lidl" });
        }

        [HttpPost]
        public IActionResult OnPostKaufland()
        {
            return RedirectToPage("MainProducts", new { selectedShop = "Kaufland" });
        }
    }
}