using System.Threading.Tasks;
using WEB.Shop.Application.ProductsAdmin;

namespace WEB.Shop.Application.BlazorServices
{
    [ScopedService]
    public class AdminProductsBlazorService
    {
        private readonly GetProducts _getProducts;
        private readonly GetProduct _getProduct;
        private readonly CreateProduct _createProduct;
        private readonly DeleteProduct _deleteProduct;
        private readonly UpdateProduct _updateProduct;

        public AdminProductsBlazorService(GetProducts getProducts, GetProduct getProduct, CreateProduct createProduct, DeleteProduct deleteProduct, UpdateProduct updateProduct)
        {
            _getProducts = getProducts;
            _getProduct = getProduct;
            _createProduct = createProduct;
            _deleteProduct = deleteProduct;
            _updateProduct = updateProduct;
        }

        public void GetProducts() => _getProducts.Do();

        public void GetProduct(int id) => _getProduct.Do(id);

        public async Task CreateProduct(CreateProduct.Request request) => await _createProduct.DoAsync(request);

        public async Task DeleteProduct(int id) => await _deleteProduct.Do(id);

        public async Task UpdateProduct(UpdateProduct.Request request) => await _updateProduct.DoAsync(request);
    }
}
