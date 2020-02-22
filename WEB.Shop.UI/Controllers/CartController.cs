using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("Dodane!");
            }

            return BadRequest("Nie udało się dodać!");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Usunięte!");
            }

            return BadRequest("Nie udało się usunąć!");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Usunięte!");
            }

            return BadRequest("Nie udało się usunąć!");
        }
    }
}