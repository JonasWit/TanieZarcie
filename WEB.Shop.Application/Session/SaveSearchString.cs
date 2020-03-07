using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Session
{
    [Service]
    public class SaveSearchString
    {
        private ISessionManager _sessionManager;

        public SaveSearchString(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Do(string input)
        {
            _sessionManager.SaveSearchString(input);
        }
    }
}
