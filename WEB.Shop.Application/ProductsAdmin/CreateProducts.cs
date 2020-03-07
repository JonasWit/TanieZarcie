using System;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.ProductsAdmin
{
    [Service]
    class CreateProducts
    {
        private IProductManager _productManager;

        public CreateProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        //todo: dodac od razu sklepy do stocku
        public async Task<bool> Do(Request request)
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

            return true;
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
        }




    }
}
