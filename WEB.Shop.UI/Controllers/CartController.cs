using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;
using WEB.Shop.Domain.Extensions;

namespace WEB.Shop.UI.Controllers
{
    //[Route("[controller]/[action]")]
    //public class CartController : Controller
    //{
    //    private readonly GetCart _getCart;

    //    public CartController(GetCart getCart)
    //    {
    //        _getCart = getCart;
    //    }

    //    [HttpPost("{stockId}")]
    //    public async Task<IActionResult> AddOneAsync(int stockId, [FromServices] AddToCart addToCart)
    //    {
    //        var request = new AddToCart.Request
    //        {
    //            StockId = stockId,
    //            Quantity = 1
    //        };

    //        var success = await addToCart.DoAsync(request);

    //        if (success)
    //        {
    //            return Ok("Dodane!");
    //        }

    //        return BadRequest("Nie udało się dodać!");
    //    }

    //    [HttpPost("{stockId}/{quantity}")]
    //    public async Task<IActionResult> Remove(int stockId, int quantity, [FromServices] RemoveFromCart removeFromCart)
    //    {
    //        var request = new RemoveFromCart.Request
    //        {
    //            StockId = stockId,
    //            Quantity = quantity
    //        };

    //        var success = await removeFromCart.DoAsync(request);

    //        if (success)
    //        {
    //            return Ok("Usunięte!");
    //        }

    //        return BadRequest("Nie udało się usunąć!");
    //    }

    //    [HttpGet]
    //    public IActionResult GetCartComponent()
    //    {
    //        var totalValue = _getCart.Do().Sum(x => x.Value * x.Quantity);
    //        return PartialView("Components/Cart/Small", totalValue.MonetaryValue(false));
    //    }

    //    [HttpGet]
    //    public IActionResult GetCartMain([FromServices] GetCart getCart)
    //    {
    //        var cart = getCart.Do();
    //        return PartialView("_CartPartial", cart);
    //    }

    //    [HttpGet]
    //    public IEnumerable<GetCart.Response> GetCart([FromServices] GetCart getCart) => getCart.Do().ToList();

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