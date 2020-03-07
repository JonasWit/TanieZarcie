using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB.Shop.Application.ProductsAdmin;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        [HttpGet("")]
        public IActionResult GetProducts([FromServices] GetProducts getProducts) => 
            Ok(getProducts.Do());

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id, [FromServices] GetProduct getProduct) => 
            Ok(getProduct.Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request, [FromServices] CreateProduct createProduct) => 
            Ok(await createProduct.DoAsync(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromBody] DeleteProduct deleteProduct) => 
            Ok(await deleteProduct.Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request, [FromServices] UpdateProduct updateProduct) => 
            Ok(await updateProduct.DoAsync(request));

    }
}