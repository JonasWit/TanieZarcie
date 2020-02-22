using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB.Shop.Application.Cart;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne(int stockId)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var addToCart = new AddToCart(HttpContext.Session, _context);
            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("Dodane!");
            }

            return BadRequest("Nie udało się dodać!");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveOne(int stockId)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Quantity = 1
            };

            var removeFromCart = new RemoveFromCart(HttpContext.Session, _context);
            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Usunięte!");
            }

            return BadRequest("Nie udało się usunąć!");
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> RemoveAll(int stockId)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                All = true
            };

            var removeFromCart = new RemoveFromCart(HttpContext.Session, _context);
            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Usunięte!");
            }

            return BadRequest("Nie udało się usunąć!");
        }
    }
}