using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.ShopAdmin
{
    [TransientService]
    public class DeleteUrl
    {
        private readonly IShopManager _shopManager;
        public DeleteUrl(IShopManager shopManager) => _shopManager = shopManager;

        public async Task<int> Do(int id)
        {
            var news = _shopManager.GetUrl(id, x => x);

            if (await _shopManager.DeleteUrl(id) > 0)
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
