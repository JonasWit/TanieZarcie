using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDbContext _context;

        public AddToCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public async Task<bool> Do(Request request)
        {
            var stockToHold = _context.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();

            if (stockToHold.Quantity >= request.Quantity)
            {
                return false;
            }

            _context.StocksOnHold.Add(new StockOnHold
            {
                StockId = stockToHold.Id,
                Quantity = request.Quantity,
                ExpiryDate = DateTime.Now.AddMinutes(20)
            });

            stockToHold.Quantity = stockToHold.Quantity - request.Quantity;

            await _context.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("Cart");
            
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Quantity += request.Quantity;
            }
            else 
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Quantity = request.Quantity
                });
            }

            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString("Cart", stringObject);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
