using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rikkonbi.WebAPI.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
    }

    public class EditUserRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}