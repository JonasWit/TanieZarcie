using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IProductManager
    {
        Task<int> CreateProduct(Product product);
        Task<int> DeleteProduct(int id);
        Task<int> UpdateProduct(Product product);

        TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector);
        TResult GetProductById<TResult>(int id, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockSearchString<TResult>(string serarchString, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockShop<TResult>(string shop, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockSearchStringShop<TResult>(string shop, string serarchString, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockPaginationShop<TResult>(int currentPage, int pageSize, string shop, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockPagination<TResult>(int currentPage, int pageSize, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockPaginationSearchString<TResult>(int currentPage, int pageSize, string serarchString, Func<Product, TResult> selector);
        IEnumerable<TResult> GetProductsWithStockPaginationSearchStringShop<TResult>(int currentPage, int pageSize, string serarchString, string shop, Func<Product, TResult> selector);
    }
}
