using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.UsersAdmin
{
    public class CreateUser
    {
        private IUserManager _userManager;

        public CreateUser(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public class Request
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            await _userManager.CreateManagerUser(request.UserName, request.Password);
            return true;
        }
    }
}
