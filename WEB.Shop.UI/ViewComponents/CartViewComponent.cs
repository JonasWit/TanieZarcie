using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WEB.Shop.Application.Cart;
using WEB.Shop.Domain.Extensions;


namespace WEB.Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private GetCart _getCart;

        public CartViewComponent(GetCart getCart)
        {
            _getCart = getCart;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            if (view == "Small")
            {
                var totalValue = _getCart.Do().Sum(x => x.Value * x.Quantity);
                return View(view, totalValue.MonetaryValue());
            }
            else if (view == "Summary")
            {
                //todo: dodać podsumowanie sklep, cena promocja itp.

                var totalValue = _getCart.Do().Sum(x => x.Value * x.Quantity);
                return View(view, totalValue.MonetaryValue());
            }

            return View(view, _getCart.Do());
        }
    }
}
