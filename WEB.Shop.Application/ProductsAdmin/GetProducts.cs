using System;
using System.Collections.Generic;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.ProductsAdmin
{
    [TransientService]
    public class GetProducts
    {
        private IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<Response> Do() =>
            _productManager.GetProductsWithStock(x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Producer = x.Producer,
                Seller = x.Seller,
                Category = x.Category,
                SourceUrl = x.SourceUrl,
                Value = x.Value,
                TimeStamp = x.TimeStamp
            });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public decimal Value { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    }
}
