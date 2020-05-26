using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEB.Shop.UI.Pages.Shared.Components.Blazor.Models
{
    public class UserModel
    {
        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
