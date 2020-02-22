using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WEB.Shop.Application.Cart;
using WEB.Shop.DataBase;

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
                var totalValue = _getCart.Do().Sum(x => x.ValueDecimal * x.Quantity);
                return View(view, $"{totalValue} Zł");
            }

            return View(view, _getCart.Do());
        }
    }
}
