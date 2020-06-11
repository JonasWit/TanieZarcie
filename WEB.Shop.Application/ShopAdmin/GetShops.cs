using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.ShopAdmin
{
    [TransientService]
    public class GetShops
    {
        private readonly IShopManager _shopManager;
        public GetShops(IShopManager shopManager) => _shopManager = shopManager;

        public IEnumerable<Response> Do() =>
            _shopManager.GetShops(singleShop => new Response
            {
                Id = singleShop.Id,
                Name = singleShop.Name,
                PromoSheets = singleShop.PromoSheets.ToList()
            });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<PromoSheetUrl> PromoSheets { get; set; }
        }
    }
}
