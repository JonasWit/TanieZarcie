using System.Collections.Generic;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Cart
{
    [Service]
    public class GetCart
    {
        private ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public decimal TotalValue { get { return Value * Quantity; } }
            public decimal Value { get; set; }

            public string TotalValueDisplay { get { return TotalValue.MonetaryValue(); } }
            public string ValueDisplay { get { return Value.MonetaryValue(); } }

            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public IEnumerable<Response> Do() =>
            _sessionManager
                .GetCart(x => new Response
                {
                    Name = x.ProductName,
                    Description = x.Description,
                    Seller = x.Seller,
                    Category = x.Category,
                    SourceUrl = x.SourceUrl,
                    Value = x.Value,
                    StockId = x.StockId,
                    Quantity = x.Quantity
                });
    }
}
