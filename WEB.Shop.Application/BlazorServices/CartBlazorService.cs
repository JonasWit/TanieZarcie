using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;
using WEB.Shop.Domain.Extensions;

namespace WEB.Shop.Application.BlazorServices
{
    [ScopedService]
    public class CartBlazorService
    {
        private readonly AddToCart _addToCart;
        private readonly GetCart _getCart;
        private readonly RemoveFromCart _removeFromCart;

        public event Action RefreshRequested;

        public string TotalValue => _getCart.Do().Sum(x => x.Value * x.Quantity).MonetaryValue(false);

        public CartBlazorService(AddToCart addToCart, GetCart getCart, RemoveFromCart removeFromCart)
        {
            _addToCart = addToCart;
            _getCart = getCart;
            _removeFromCart = removeFromCart;
        }

        public void CallRequestRefresh() => RefreshRequested?.Invoke();

        public IEnumerable<GetCart.Response> GetCartProducts() => _getCart.Do();

        public async Task<bool> AddOneAsync(int stockId)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var success = await _addToCart.DoAsync(request);

            if (success)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Remove(int stockId, int quantity)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = quantity
            };

            var success = await _removeFromCart.DoAsync(request);

            if (success)
            {
                return true;
            }

            return false;
        }
    }
}
