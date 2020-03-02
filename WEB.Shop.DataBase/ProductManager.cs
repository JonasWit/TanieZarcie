using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class ProductManager : IProductManager
    {
        private ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public TResult GetProductById<TResult>(int id, Func<Product, TResult> selector)
        {
            return _context.Products
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetProductByName<TResult>(string name, Func<Product, TResult> selector)
        {
            return _context.Products
                .Include(x => x.Stock)
                .Where(x => x.Name == name)
                .Select(selector)
                .FirstOrDefault();
        }

        public IEnumerable<TResult> GetProductsWithStock<TResult>(Func<Product, TResult> selector)
        {
            return _context.Products
                .Include(x => x.Stock)
                .OrderByDescending(x => x.Value)
                .Select(selector)
                .Reverse()
                .ToList();
        }

        public IEnumerable<TResult> GetProductsWithStock<TResult>(string searchString, Func<Product, TResult> selector)
        {
            //todo: use regex match

            return _context.Products
                .Include(x => x.Stock)
                .AsEnumerable()
                .Where(x => x.Name.ToUpper().Contains(searchString.ToUpper()))
                .OrderByDescending(x => x.Value)
                .Select(selector)
                .Reverse()
                .ToList();
        }

        public IEnumerable<TResult> GetProductsWithStock<TResult>(int pageNumber, Func<Product, TResult> selector)
        {
            return _context.Products
                .Include(x => x.Stock)
                .OrderByDescending(x => x.Value)
                .Select(selector)
                .Reverse()
                .ToList();
        }

        public IEnumerable<TResult> GetProductsWithStock<TResult>(int pageNumber, string searchString, Func<Product, TResult> selector)
        {
            //todo: use regex match

            return _context.Products
                .Include(x => x.Stock)
                .AsEnumerable()
                .Where(x => x.Name.ToUpper().Contains(searchString.ToUpper()))
                .OrderByDescending(x => x.Value)
                .Select(selector)
                .Reverse()
                .ToList();
        }

    }
}
