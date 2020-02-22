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

        public class Response
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public string Value { get { return $"{ValueDecimal.ToString("N2")} Zł";  } }
            public string TotalValue { get { return $"{(ValueDecimal * Quantity).ToString("N2")} Zł"; } }
            public decimal ValueDecimal { get; set; }

            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var stringObject = _session.GetString("Cart");

            if (string.IsNullOrEmpty(stringObject))
            {
                return new List<Response>();
            }

            var cartList =  JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            var response = _context.Stock
                .Include(x => x.Product)
                .AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Description = x.Product.Description,
                    Seller = x.Product.Seller,
                    Category = x.Product.Category,
                    SourceUrl = x.Product.SourceUrl,
                    ValueDecimal = x.Product.Value,
                    StockId = x.Id,
                    Quantity = cartList.FirstOrDefault(y => y.StockId == x.Id).Quantity
                })
                .ToList();

            return response;
        }
    }
}
