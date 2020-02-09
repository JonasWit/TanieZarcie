using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Cart
{
    public class GetCart
    {
        private ISession _session;
        private ApplicationDbContext _context;

        public GetCart(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public Response Do()
        {
            //todo: accou tfor multiple items in the cart
            var stringObject = _session.GetString("Cart");
            var cartProduct =  JsonConvert.DeserializeObject<CartProduct>(stringObject);

            var response = _context.Stock
                .Include(x => x.Product)
                .Where(x => x.Id == cartProduct.StockId)
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Value = $"{x.Product.Value.ToString("N2")} pln",
                    StockId = x.Id,
                    Quantity = cartProduct.Quantity

                })
                .FirstOrDefault();

            return response;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
