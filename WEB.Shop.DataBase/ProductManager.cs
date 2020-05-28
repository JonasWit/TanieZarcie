using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class ProductManager : IProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context) => _context = context;

        public Task<int> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);

            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateProduct(Product product)
        {
            _context.Products.Update(product);

            return _context.SaveChangesAsync();
        }

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector) =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
        
        public TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector) =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .Where(x => x.Name == name)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector) =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .Select(selector)
                .ToList();

        public IEnumerable<TResult> GetProductsWithStockWithCondition<TResult>(Func<Product, TResult> selector, Func<Product, bool> predicate) =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .AsEnumerable()
                .Where(predicate)
                .Select(selector)
                .ToList();

        public IEnumerable<TResult> GetProductsWithStockWithPagination<TResult>(int currentPage, int pageSize, Func<Product, TResult> selector) =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .AsEnumerable()
                .Select(selector)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public IEnumerable<TResult> GetProductsWithStockWithPaginationAndCondition<TResult>(int currentPage, int pageSize, Func<Product, TResult> selector, Func<Product, bool> predicate) =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .AsEnumerable()
                .Where(predicate)
                .Select(selector)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public int CountProductsWithStockWithCondition(Func<Product, bool> predicate) =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .AsEnumerable()
                .Count(predicate);

        public int CountAllProductsWithStock() =>
            _context.Products
                .Include(x => x.Stock)
                .Include(x => x.Producer)
                .Include(x => x.Distributor)
                .Include(x => x.Category)
                .AsEnumerable()
                .Count();
    }
}
