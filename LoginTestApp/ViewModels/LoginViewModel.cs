using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LoginTestApp.Crosscutting.Contracts.Attributes;

namespace LoginTestApp.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("User Name")]
        [Required]
        public string Alias { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
    }
}