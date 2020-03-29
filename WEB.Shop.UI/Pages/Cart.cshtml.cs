using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WEB.Shop.Application.Cart;

namespace WEB.Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<GetCart.Response> Cart { get; set; }

        public IActionResult OnGet([FromServices] GetCart getCart)
        {
            Cart = getCart.Do();
            return Page();
        }

        //public IActionResult OnGetReturn()
        //{
        //    return RedirectToPage("MainProducts");
        //}

        //public IActionResult OnPostReturn()
        //{
        //    return RedirectToPage("MainProducts");
        //}

        //todo: zrobic metode na wyczyszczenia koszyka
    }
}
