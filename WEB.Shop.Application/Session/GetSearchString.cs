using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Session
{
    [TransientService]
    public class GetSearchString
    {
        private ISessionManager _sessionManager;

        public GetSearchString(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public bool Do(out string output)
        {
            if (_sessionManager.GetSearchString(out output))
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
