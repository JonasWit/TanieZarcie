using System.Collections.Generic;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.ProductsAdmin
{
    [Service]
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
                Value = x.Value
            });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
    }
}
