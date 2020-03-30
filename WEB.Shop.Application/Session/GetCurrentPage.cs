using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Session
{
    [TransientService]
    public class GetCurrentPage
    {
        private ISessionManager _sessionManager;

        public GetCurrentPage(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public bool Do(out int output)
        {
            if (_sessionManager.GetCurrentPage(out output))
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
