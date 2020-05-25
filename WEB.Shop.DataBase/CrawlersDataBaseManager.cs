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
        private readonly ApplicationDbContext _context;

        public CrawlersDataBaseManager(ApplicationDbContext context) => _context = context;

        public async Task<int> ClearDataBaseAsync()
        {
            _context.Products.Clear();
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CleanUpDataBaseAsync()
        {
            var categoriesToClean = _context.Categories.Where(x => !_context.Products.Any(y => y.CategoryId == x.Id)).ToList();
            _context.Categories.RemoveRange(categoriesToClean);

            var deistributorsToClean = _context.Distributors.Where(x => !_context.Products.Any(y => y.DistributorId == x.Id)).ToList();
            _context.Distributors.RemoveRange(deistributorsToClean);

            var producersToClean = _context.Producers.Where(x => !_context.Products.Any(y => y.ProducerId == x.Id)).ToList();
            _context.Producers.RemoveRange(producersToClean);

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
                    if (!_context.Distributors.Any(x => x.DistributorName == product.Distributor.DistributorName))
                    {
                        _context.Distributors.Add(product.Distributor);
                        await _context.SaveChangesAsync();
                    }

                    if (!_context.Categories.Any(x => x.CategoryName == product.Category.CategoryName))
                    {
                        _context.Categories.Add(product.Category);
                        await _context.SaveChangesAsync();
                    }

                    if (!_context.Producers.Any(x => x.ProducerName == product.Producer.ProducerName))
                    {
                        _context.Producers.Add(product.Producer);
                        await _context.SaveChangesAsync();
                    }
                }

                foreach (var product in products)
                {
                    product.Producer = _context.Producers.FirstOrDefault(x => x.ProducerName == product.Producer.ProducerName);
                    product.ProducerId = product.Producer.Id;

                    product.Distributor = _context.Distributors.FirstOrDefault(x => x.DistributorName == product.Distributor.DistributorName);
                    product.DistributorId = product.Distributor.Id;

                    product.Category = _context.Categories.FirstOrDefault(x => x.CategoryName == product.Category.CategoryName);
                    product.CategoryId = product.Category.Id;
                }

                foreach (var product in products)
                {
                    product.Stock = new List<Stock>
                    {
                        new Stock { Description = product.Distributor.DistributorName, Quantity = 100 }
                    };

                    _context.Products.Add(product);
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

            var shops = _context.Products.Select(s => s.Distributor.DistributorName).Distinct().ToList();

            foreach (var shop in shops)
            {
                result.Add(
                    (shop.ToString(),
                    _context.Products.Where(p => p.Distributor.DistributorName == shop).Count(),
                    _context.Products.Where(p => p.Distributor.DistributorName == shop && p.OnSale).Count(),
                    _context.Products.Where(p => p.Distributor.DistributorName == shop)
                        .OrderByDescending(p => p.TimeStamp)
                        .Select(p => p.TimeStamp)
                        .FirstOrDefault()));
            }

            return result;
        }
    }
}
