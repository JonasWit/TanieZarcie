using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;

        public AddToCart(ISession session)
        {
            _session = session;
        }

        public void Do(Request request)
        {
            var cartProduct = new CartProduct
            {
                StockId = request.StockId,
                Quantity = request.Quantity
            };

            var stringObject = JsonConvert.SerializeObject(cartProduct);
            _session.SetString("Cart", stringObject);
        
            //todo: append the cart
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
