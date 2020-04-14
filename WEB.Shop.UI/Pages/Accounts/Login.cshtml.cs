using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Shop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.UserName) || string.IsNullOrEmpty(Input.Password))
            {
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else 
            {
                return Page();
            }
        }
    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}