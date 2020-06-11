using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IShopManager
    {
        IEnumerable<TResult> GetShops<TResult>(Func<ShopData, TResult> selector);

        Task<int> AddUrl(PromoSheetUrl url);
        Task<int> RemoveUrl(int id);

        Task<int> AddShop(ShopData shopData);
        Task<int> RemoveShop(int id);
    }
}
