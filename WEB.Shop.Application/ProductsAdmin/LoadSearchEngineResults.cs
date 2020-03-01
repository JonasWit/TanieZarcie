using System.Threading.Tasks;

namespace WEB.Shop.Application.ProductsAdmin
{
    public class LoadSearchEngineResults
    {
        public async Task<Response> Do(Request request)
        {
            //todo: SE delete all database

            //todo: SE add new data

            //var product = new Product
            //{
            //    Name = request.Name,
            //    Description = request.Description,
            //    Value = request.Value
            //};

            //_context.Products.Add(product);

            //await _context.SaveChangesAsync();

            //return new Response
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    Value = product.Value
            //};

            return new Response();
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
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
