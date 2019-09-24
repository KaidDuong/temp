using Rikkonbi.WebAPI.ViewModels;

namespace Rikkonbi.WebAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(UserViewModel user);
    }
}