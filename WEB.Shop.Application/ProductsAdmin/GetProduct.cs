using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.ProductsAdmin
{
    [TransientService]
    public class GetProduct
    {
        private IProductManager _productManager;

        public GetProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public Response Do(int id) =>
            _productManager.GetProductById(id, product => new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
}
