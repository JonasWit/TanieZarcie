using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.DataBase
{
    public class UserManager : IUserManager
    {
        private UserManager<IdentityUser> _userManager;

        public UserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateManagerUser(string userName, string password)
        {
            var managerUser = new IdentityUser()
            {
                UserName = userName
            };

            await _userManager.CreateAsync(managerUser, password);

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(managerUser, managerClaim);
        }
    }
}
