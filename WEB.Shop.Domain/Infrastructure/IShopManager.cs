using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IShopManager
    {
        IEnumerable<TResult> GetShops<TResult>(Func<ShopData, TResult> selector);
        TResult GetShop<TResult>(int id, Func<ShopData, TResult> selector);
        TResult GetUrl<TResult>(int id, Func<PromoSheetUrl, TResult> selector);

        Task<int> AddUrl(PromoSheetUrl url);
        Task<int> DeleteUrl(int id);

        Task<int> AddShop(ShopData shopData);
        Task<int> DeleteShop(int id);
    }
}
