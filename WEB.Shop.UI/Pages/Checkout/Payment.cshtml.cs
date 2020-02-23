using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.ApplicationCore;
using WEB.Shop.Application.Cart;
using WEB.Shop.Application.Orders;
using WEB.Shop.DataBase;
using GetOrderCart = WEB.Shop.Application.Cart.GetOrder;

namespace WEB.Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private ApplicationDbContext _context;
        private AppCore _core;

        public PaymentModel(ApplicationDbContext context, AppCore core)
        {
            _context = context;
            _core = core;
        }

        public async Task<IActionResult> OnGet([FromServices] GetOrderCart getOrder)
        {
            //Here handle saving Cart To database

            var cartOrder = getOrder.Do();
            var sessionId = HttpContext.Session.Id;

            //await new CreateOrder(_context).Do(new CreateOrder.Request
            //{
            //    StripeReference = "none",
            //    SessionId = sessionId,

            //    Stocks = cartOrder.Products.Select(x => new CreateOrder.Stock
            //    {
            //        StockId = x.StockId,
            //        Quantity = x.Quantity
            //    }).ToList()
            //});

            return Page();
        }
    }
}