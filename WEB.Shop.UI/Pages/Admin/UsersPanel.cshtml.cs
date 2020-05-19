using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WEB.Shop.UI.Pages.Admin
{
    public class UsersPanelModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersPanelModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public UserViewModel Input { get; set; }

        public List<RegisteredUserViewModel> AllUsers { get; set; }

        public string MyProperty { get; set; }

        public void OnGet()
        {
            AllUsers = _userManager.Users.Select(x => new RegisteredUserViewModel { Username = x.UserName }).ToList();

        }

        public void OnGetDeleteUser(string userName)
        {
            AllUsers = _userManager.Users.Select(x => new RegisteredUserViewModel { Username = x.UserName }).ToList();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Username) || string.IsNullOrEmpty(Input.Password))
            {
                return Page();
            }

            AllUsers = _userManager.Users.Select(x => new RegisteredUserViewModel { Username = x.UserName }).ToList();

            if (AllUsers.Any(x => x.Username == Input.Username))
            {
                return Page();
            }

            var managerUser = new IdentityUser()
            {
                UserName = Input.Username
            };

            await _userManager.CreateAsync(managerUser, "Jon@sz32167");
            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(managerUser, managerClaim);

            AllUsers = _userManager.Users.Select(x => new RegisteredUserViewModel { Username = x.UserName }).ToList();

            return Page();
        }
    }

    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisteredUserViewModel
    {
        public string Username { get; set; }
        public string Claim { get; set; }
    }

    public class MessageViewModel
    { 
    
    
    
    }
}
