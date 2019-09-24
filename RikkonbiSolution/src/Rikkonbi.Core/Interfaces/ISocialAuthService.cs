namespace Rikkonbi.Core.Interfaces
{
    public interface ISocialAuthService
    {
        bool IsAuthenticated { get; set; }
        void ValidateToken(string token);
        ISocialUserProfile GetUserProfile();
    }
}