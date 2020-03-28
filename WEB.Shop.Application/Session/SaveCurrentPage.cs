using System;
using System.Collections.Generic;
using System.Text;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Session
{
    [Service]
    public class SaveCurrentPage
    {
        private ISessionManager _sessionManager;

        public SaveCurrentPage(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Do(int input)
        {
            _sessionManager.SaveCurrentPage(input);
        }
    }
}
