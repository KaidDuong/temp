using System.Security.Claims;

namespace Rikkonbi.WebAPI.Extensions
{
    /// <summary>
    /// User's authentication info extentions
    /// </summary>
    public static class UserAuthInfoExtentions
    {
        /// <summary>
        /// Get UserId from ClaimsPrincipal of current User
        /// </summary>
        /// <param name="user">The ClaimsPrincipal of user associated with the executing action.</param>
        /// <returns>The Id of current User</returns>
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}