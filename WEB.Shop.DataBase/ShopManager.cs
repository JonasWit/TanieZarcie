using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class ShopManager : IShopManager
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ShopManager(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public IEnumerable<TResult> GetShops<TResult>(Func<ShopData, TResult> selector) =>
            _applicationDbContext.ShopsData
                .Include(x => x.PromoSheets)
                .Select(selector)
                .ToList();

        public TResult GetShop<TResult>(int id, Func<ShopData, TResult> selector) =>
            _applicationDbContext.ShopsData
                .Include(x => x.PromoSheets)
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public TResult GetUrl<TResult>(int id, Func<PromoSheetUrl, TResult> selector) =>
            _applicationDbContext.PromoSheetsUrls
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public Task<int> AddShop(ShopData shopData)
        {
            var newShop = new ShopData
            {
                Name = shopData.Name,
                PromoSheets = shopData.PromoSheets
            };

            _applicationDbContext.ShopsData.Add(newShop);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> AddUrl(PromoSheetUrl url)
        {
            _applicationDbContext.PromoSheetsUrls.Add(url);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> DeleteShop(int id)
        {
            var shop = _applicationDbContext.ShopsData.FirstOrDefault(x => x.Id == id);
            _applicationDbContext.ShopsData.Remove(shop);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> DeleteUrl(int id)
        {
            var url = _applicationDbContext.PromoSheetsUrls.FirstOrDefault(x => x.Id == id);
            _applicationDbContext.PromoSheetsUrls.Remove(url);
            return _applicationDbContext.SaveChangesAsync();
        }
    }
}
