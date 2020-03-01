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
        /// <summary>
        /// Get products but with search string to filter
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="serarchString"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        IEnumerable<TResult> GetProductsWithStock<TResult>(string serarchString, Func<Product, TResult> selector);
    }
}
