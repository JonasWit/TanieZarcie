using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WEB.Shop.UI.ViewModels.Admin;

namespace WEB.Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel createUserViewModel)
        {
            var managerUser = new IdentityUser()
            {
                UserName = createUserViewModel.Username
            };

            await _userManager.CreateAsync(managerUser, "Jon@sz32167");
            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(managerUser, managerClaim);

            return Ok();
        }
    }
}