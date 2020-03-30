using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Session
{
    [TransientService]
    public class GetSelectedShop
    {
        private ISessionManager _sessionManager;

        public GetSelectedShop(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public bool Do(out string output)
        {
            if (_sessionManager.GetSelectedShop(out output))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
