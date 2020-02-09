using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Shop.Application.Cart;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI.Pages
{
    public class CartModel : PageModel
    {
        private ApplicationDbContext _context;

        public GetCart.Response Cart { get; set; }

        public CartModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, _context).Do();


            return Page();
        }
    }
}
