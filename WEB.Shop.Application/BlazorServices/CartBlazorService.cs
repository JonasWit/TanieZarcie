using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;

namespace WEB.Shop.Application.BlazorServices
{
    [ScopedService]
    public class CartBlazorService
    {
        private readonly AddToCart _addToCart;
        private readonly GetCart _getCart;
        private readonly RemoveFromCart _removeFromCart;

        public CartBlazorService(AddToCart addToCart, GetCart getCart, RemoveFromCart removeFromCart)
        {
            _addToCart = addToCart;
            _getCart = getCart;
            _removeFromCart = removeFromCart;
        }

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

    //[Route("[controller]/[action]")]
    //public class CartController : Controller
    //{


    //    [HttpGet]
    //    public IActionResult GetCartComponent([FromServices] GetCart getCart)
    //    {
    //        var totalValue = getCart.Do().Sum(x => x.Value * x.Quantity);
    //        return PartialView("Components/Cart/Small", totalValue.MonetaryValue());
    //    }

    //    [HttpGet]
    //    public IActionResult GetCartMain([FromServices] GetCart getCart)
    //    {
    //        var cart = getCart.Do();
    //        return PartialView("_CartPartial", cart);
    //    }

    //    [HttpPost("{stockId}")]
    //    public async Task<IActionResult> RemoveAll(int stockId, [FromServices] RemoveFromCart removeFromCart)
    //    {
    //        var request = new RemoveFromCart.Request
    //        {
    //            StockId = stockId,
    //            All = true
    //        };

    //        var success = await removeFromCart.DoAsync(request);

    //        if (success)
    //        {
    //            return Ok("Usunięte!");
    //        }

    //        return BadRequest("Nie udało się usunąć!");
    //    }
    //}

}
