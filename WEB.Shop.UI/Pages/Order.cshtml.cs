using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Shop.Application.Orders;

namespace WEB.Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        public GetOrder.Response Order { get; set; }

        public void OnGet(int id, [FromServices] GetOrder getOrder)
        {
            Order = getOrder.Do(id);
        }
    }
}
