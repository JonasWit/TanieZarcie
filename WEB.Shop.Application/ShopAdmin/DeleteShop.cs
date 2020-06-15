using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.ShopAdmin
{
    [TransientService]
    public class DeleteShop
    {
        private readonly IShopManager _shopManager;
        public DeleteShop(IShopManager shopManager) => _shopManager = shopManager;

        public async Task<int> Do(int id)
        {
            var news = _shopManager.GetShop(id, x => x);

            if (await _shopManager.DeleteShop(id) > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
