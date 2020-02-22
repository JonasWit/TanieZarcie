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
    public class RemoveFromCart
    {
        private ISession _session;
        private ApplicationDbContext _context;

        public RemoveFromCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public async Task<bool> Do(Request request)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("Cart");

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == request.StockId))
            {
                return true;

            }

            if (request.All)
            {
                var item = cartList.Find(x => x.StockId == request.StockId);
                cartList.Remove(item);
            }
            else 
            { 
              cartList.Find(x => x.StockId == request.StockId).Quantity -= request.Quantity;
            }
          
            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString("Cart", stringObject);

            //var stockOnHold = _context.StocksOnHold
            //    .FirstOrDefault(x => x.StockId == request.StockId
            //    && x.SessionId == _session.Id);

            //var stock = _context.Stock.FirstOrDefault(x => x.Id == request.StockId);

            //if (request.All)
            //{
            //    stock.Quantity += stockOnHold.Quantity;
            //    stockOnHold.Quantity = 0;
            //}
            //else
            //{
            //    stock.Quantity += stockOnHold.Quantity;
            //    stockOnHold.Quantity -= request.Quantity;
            //}

            //if (stockOnHold.Quantity <= 0)
            //{
            //    _context.Remove(stockOnHold);
            //}

            //await _context.SaveChangesAsync();

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
            public bool All { get; set; }
        }
    }
}
