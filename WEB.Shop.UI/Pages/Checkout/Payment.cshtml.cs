using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Shop.Application.Cart;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private ApplicationDbContext _context;

        public PaymentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //Here handle display of the cart and charge

            var cartOrder = new GetOrder(HttpContext.Session, _context).Do();
            var value = cartOrder.GetTotalCharge();
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }


        public IActionResult OnPost(string stripeEmail, string stripeToken)
        {
            var cartOrder = new GetOrder(HttpContext.Session, _context).Do();





            return RedirectToPage("/Index");
        }
    }
}