using Rikkonbi.Core.Interfaces;

namespace Rikkonbi.Core.Entities
{
    public class SocialUserProfile : ISocialUserProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}