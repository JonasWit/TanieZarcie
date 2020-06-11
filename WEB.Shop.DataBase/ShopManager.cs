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

        public Task<int> AddShop(ShopData shopData)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddUrl(PromoSheetUrl url)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveShop(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveUrl(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
