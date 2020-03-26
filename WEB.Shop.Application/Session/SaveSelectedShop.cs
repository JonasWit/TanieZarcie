using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Session
{
    [Service]
    public class SaveSelectedShop
    {
        private ISessionManager _sessionManager;

        public SaveSelectedShop(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Do(string input)
        {
            _sessionManager.SaveSelectedShop(input);
        }
    }
}
