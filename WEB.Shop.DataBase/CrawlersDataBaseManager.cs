using System;
using System.Collections.Generic;
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

        public async Task<int> RefreshDatabaseAsync(List<Product> products)
        {
            int response;
            try
            {
                foreach (var product in products)
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    _context.Stock.Add(new Stock
                    {
                        Description = product.Seller,
                        Quantity = 100,
                        ProductId = product.Id
                    });

                }

                response = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }





    }
}
