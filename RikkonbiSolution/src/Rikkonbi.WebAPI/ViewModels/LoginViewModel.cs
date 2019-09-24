using System.ComponentModel.DataAnnotations;

namespace Rikkonbi.WebAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}