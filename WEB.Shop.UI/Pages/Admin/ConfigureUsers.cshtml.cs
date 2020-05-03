using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WEB.Shop.UI.Pages.Admin
{
    public class ConfigureUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ConfigureUsersModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public UserViewModel Input { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Username) || string.IsNullOrEmpty(Input.Password))
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

            //if (result.Succeeded)
            //{
            //    return RedirectToPage("/Index");
            //}
            //else
            //{
            //    return Page();
            //}

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
}
