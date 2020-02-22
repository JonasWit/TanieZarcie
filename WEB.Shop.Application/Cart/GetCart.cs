using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Infrastructure;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.Cart
{
    public class GetCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _context;

        public GetCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
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
            var cartList = _sessionManager.GetCart();

            if (cartList == null)
            {
                return new List<Response>();
            }

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
