using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class CrawlersDataBaseManager : ICrawlersDataBaseManager
    {
        private ApplicationDbContext _context;

        public CrawlersDataBaseManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> ClearDataBaseAsync()
        {
            _context.Products.Clear();
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProductFromShops(string shop)
        {
            _context.Products.DeleteProductsFromShop(shop);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateDatabaseAsync(List<Product> products)
        {
            int response;
            try
            {
                foreach (var product in products)
                {
                    product.Stock = new List<Stock>
                    {
                        new Stock { Description = product.Seller, Quantity = 100 }
                    };

                    _context.Products.Add(product);
                    //await _context.SaveChangesAsync();

                    //_context.Stock.Add(new Stock
                    //{
                    //    Description = product.Seller,
                    //    Quantity = 100,
                    //    ProductId = product.Id
                    //});
                }

                response = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public List<(string, int, int, DateTime)> CheckDataBase()
        {
            var result = new List<(string, int, int, DateTime)>();

            var shops = _context.Products.Select(s => s.Seller).Distinct().ToList();

            foreach (var shop in shops)
            {
                result.Add(
                    (shop.ToString(),
                    _context.Products.Where(p => p.Seller == shop).Count(), 
                    _context.Products.Where(p => p.Seller == shop && p.OnSale).Count(), 
                    _context.Products.Where(p => p.Seller == shop)
                        .OrderByDescending(p => p.TimeStamp)
                        .Select(p => p.TimeStamp)
                        .FirstOrDefault()));
            }

            return result;
        }
    }
}
