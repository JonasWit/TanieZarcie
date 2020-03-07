using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;
using WEB.Shop.Domain.Extensions;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOneAsync(int stockId, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var success = await addToCart.DoAsync(request);

            if (success)
            {
                return Ok("Dodane!");
            }

            return BadRequest("Nie udało się dodać!");
        }

        [HttpPost("{stockId}/{quantity}")]
        public async Task<IActionResult> Remove(int stockId, int quantity, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = quantity
            };

            var success = await removeFromCart.DoAsync(request);

            if (success)
            {
                return Ok("Usunięte!");
            }

            return BadRequest("Nie udało się usunąć!");
        }

        [HttpGet]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Do().Sum(x => x.Value * x.Quantity);
            return PartialView("Components/Cart/Small", totalValue.MonetaryValue());
        }

        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Do();
            return PartialView("_CartPartial", cart);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var success = await removeFromCart.DoAsync(request);

            if (success)
            {
                return Ok("Usunięte!");
            }

            return BadRequest("Nie udało się usunąć!");
        }
    }
}