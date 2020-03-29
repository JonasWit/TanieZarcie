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

        public IActionResult OnPostReturn()
        {
            return RedirectToPage("MainProducts");
        }

        //todo: inject session and add button to clear cart
    }
}
