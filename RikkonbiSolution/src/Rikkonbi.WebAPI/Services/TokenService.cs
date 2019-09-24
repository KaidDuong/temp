using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.Interfaces;
using Rikkonbi.WebAPI.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rikkonbi.WebAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(UserViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            int tokenTimeLifeMinutes = _configuration.GetSection("AppSettings").Get<AppSettings>().TokenTimeLifeMinutes;
            var secret = _configuration.GetSection("AppSettings").Get<AppSettings>().Secret;
            var key = Encoding.ASCII.GetBytes(secret);

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in user.Roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Rikkonbi.API",
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddMinutes(tokenTimeLifeMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}