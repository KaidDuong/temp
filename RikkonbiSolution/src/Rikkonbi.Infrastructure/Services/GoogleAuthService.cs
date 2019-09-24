using Google.Apis.Auth;
using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Extensions;
using Rikkonbi.Core.Interfaces;
using System;

namespace Rikkonbi.Infrastructure.Services
{
    public class GoogleAuthService : ISocialAuthService
    {
        private GoogleJsonWebSignature.Payload _jwtPayload;
        public bool IsAuthenticated { get; set; }

        public void ValidateToken(string token)
        {
            Guard.Against.NullOrWhiteSpace(token, nameof(token));

            try
            {
                _jwtPayload = GoogleJsonWebSignature.ValidateAsync(token).Result;
                IsAuthenticated = true;
            }
            catch (Exception ex)
            {
                IsAuthenticated = false;
            }
        }

        public ISocialUserProfile GetUserProfile()
        {
            var userProfile = new SocialUserProfile
            {
                Name = _jwtPayload.Name,
                Email = _jwtPayload.Email,
                Avatar = _jwtPayload.Picture
            };

            return userProfile;
        }
    }
}