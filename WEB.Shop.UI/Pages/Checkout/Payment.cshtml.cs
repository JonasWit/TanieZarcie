using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Shop.Application.ApplicationCore;
using GetOrderCart = WEB.Shop.Application.Cart.GetOrder;

namespace WEB.Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private AppCore _core;

        public PaymentModel(AppCore core)
        {
            _core = core;
        }

        public IActionResult OnGet([FromServices] GetOrderCart getOrder)
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