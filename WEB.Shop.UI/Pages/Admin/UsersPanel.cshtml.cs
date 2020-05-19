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

        public void OnGet()
        {
            var users = _userManager.Users.ToList();

            var userfilter = users.Where(u => u.Claims.Any(t => t.ClaimType == "ArticleId" && t.ClaimValue == "1"));


            AllUsers = _userManager.Users.Select(x => new RegisteredUserViewModel {  Username = x.UserName }).ToList();

            foreach (var user in AllUsers)
            {
                if (true)
                {

                }



            }  


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

    public class RegisteredUserViewModel
    {
        public string Username { get; set; }
        public string Claim { get; set; }
    }
}
