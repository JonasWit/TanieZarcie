using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {  
            return View(view, new GetCart(HttpContext.Session, _context).Do());
        }
    }
}
