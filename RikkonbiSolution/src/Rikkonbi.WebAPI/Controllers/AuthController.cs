using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Infrastructure.Identity;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.Interfaces;
using Rikkonbi.WebAPI.ViewModels;
using System;

namespace Rikkonbi.WebAPI.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly ISocialAuthService _socialAuthService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AuthController(ISocialAuthService socialAuthService, ITokenService tokenService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _socialAuthService = socialAuthService;
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
        }

        // POST api/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                // Validate Google token
                _socialAuthService.ValidateToken(loginViewModel.Token);

                if (!_socialAuthService.IsAuthenticated)
                {
                    return BadRequest("The Google token is invalid or has expired!");
                }

                ISocialUserProfile userProfile = _socialAuthService.GetUserProfile();

                var user = _userManager.FindByEmailAsync(userProfile.Email).Result;

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = userProfile.Email.Replace("@rikkeisoft.com", ""),
                        FullName = userProfile.Name,
                        Email = userProfile.Email,
                        Avatar = userProfile.Avatar,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "API_AUTO"
                    };

                    _userManager.CreateAsync(user, "Default@password123").Wait();
                    _userManager.AddToRoleAsync(user, Roles.USER).Wait();
                }

                var userViewModel = _mapper.Map<UserViewModel>(user);
                userViewModel.Roles = _userManager.GetRolesAsync(user).Result;
                userViewModel.Token = _tokenService.CreateAccessToken(userViewModel);

                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/auth/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}