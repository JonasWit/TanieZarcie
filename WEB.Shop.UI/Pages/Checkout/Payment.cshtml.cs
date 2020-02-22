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

        public IActionResult OnGet()
        {

            //Here handle display of the cart and charge

            //var cartOrder = new GetOrder(HttpContext.Session, _context).Do();
            //var value = cartOrder.GetTotalCharge();
            //var information = new GetCustomerInformation(HttpContext.Session).Do();

            //if (information == null)
            //{
            //    return RedirectToPage("/Checkout/CustomerInformation");
            //}

            return Page();
        }

        public async Task<IActionResult> CreateOrder([FromServices] GetOrderCart getOrder)
        {
            var cartOrder = getOrder.Do();
            var sessionId = HttpContext.Session.Id;

            await new CreateOrder(_context).Do(new CreateOrder.Request
            {
                StripeReference = "none",
                SessionId = sessionId,

                FirstName = cartOrder.CustomerInformation.FirstName,
                LastName = cartOrder.CustomerInformation.LastName,
                Email = cartOrder.CustomerInformation.Email,
                PhoneNumber = cartOrder.CustomerInformation.PhoneNumber,
                Address1 = cartOrder.CustomerInformation.Address1,
                Address2 = cartOrder.CustomerInformation.Address2,
                City = cartOrder.CustomerInformation.City,
                PostCode = cartOrder.CustomerInformation.PostCode,
                Stocks = cartOrder.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Quantity = x.Quantity
                }).ToList()
            });

            return RedirectToPage("/Index");
        }
    }
}