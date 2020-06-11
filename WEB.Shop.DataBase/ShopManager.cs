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
            throw new NotImplementedException();
        }

        public Task<int> AddUrl(PromoSheetUrl url)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteShop(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteUrl(int id)
        {
            throw new NotImplementedException();
        }
    }
}
