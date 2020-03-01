using System;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private IProductManager _productManager;

        public CreateProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<Response> Do(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = decimal.Parse(request.Value)
            };

            if (await _productManager.CreateProduct(product) <= 0)
            {
                throw new Exception("Failed to create product");
            }
            
            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }



    }
}
