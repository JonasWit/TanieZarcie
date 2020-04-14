using System.Collections.Generic;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Cart
{
    [TransientService]
    public class GetCart
    {
        private ISessionManager _sessionManager;

        public GetCart(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public decimal TotalValue { get { return Value * Quantity; } }
            public decimal Value { get; set; }

            public string TotalValueDisplay { get { return TotalValue.MonetaryValue(false); } }
            public string ValueDisplay { get { return Value.MonetaryValue(false); } }

            public int StockId { get; set; }
            public int Quantity { get; set; }

        }

        public IEnumerable<Response> Do() =>
            _sessionManager
                .GetCart(cartProduct => new Response
                {
                    Id = cartProduct.ProductId,
                    Name = cartProduct.ProductName,
                    Description = cartProduct.Description,
                    Seller = cartProduct.Seller,
                    Category = cartProduct.Category,
                    SourceUrl = cartProduct.SourceUrl,
                    Value = cartProduct.Value,
                    StockId = cartProduct.StockId,
                    Quantity = cartProduct.Quantity
                });
    }
}
