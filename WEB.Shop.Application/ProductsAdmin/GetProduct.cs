using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private IProductManager _productManager;

        public GetProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public Response Do(int id) =>
            _productManager.GetProductById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
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
