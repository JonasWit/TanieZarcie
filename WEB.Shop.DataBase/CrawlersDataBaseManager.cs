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

        private bool ClearDataBase()
        {


            return true;
        }

        public Task<int> RefreshDatabase(List<Product> products)
        {

            //public async Task<IActionResult> DeleteAll()
            //{
            //    var list = await _context.YourClass.ToListAsync();
            //    _context.YourClass.RemoveRange(list);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //context.Database.ExecuteSqlRaw("TRUNCATE TABLE [TableName]");



            //public Task<int> Do(int id)
            //{
            //    return _productManager.DeleteProduct(id);
            //}

            foreach (var product in products)
            {
                _context.Products.Add(product);
                _context.Stock.Add(new Stock
                {
                    Description = product.Seller,
                    Quantity = 100,
                    ProductId = product.Id
                });
            }

            //public Task<int> CreateProduct(Product product)
            //{
            //    _context.Products.Add(product);
            //    return _context.SaveChangesAsync();
            //}


            //public Task<int> CreateStock(Stock stock)
            //{
            //    _context.Stock.Add(stock);
            //    return _context.SaveChangesAsync();
            //}



            //var stock = new Stock
            //{
            //    Description = request.Description,
            //    Quantity = request.Quantity,
            //    ProductId = request.ProductId
            //};

            //await _stockManager.CreateStock(stock);







            return _context.SaveChangesAsync();
        }





    }
}
