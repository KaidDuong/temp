using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.Infrastructure.Identity;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rikkonbi.WebAPI.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult Get()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = _mapper.Map<UserViewModel>(user);
                userViewModel.Roles = _userManager.GetRolesAsync(user).Result;
                userViewModels.Add(userViewModel);
            }

            return Ok(userViewModels);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.Roles = _userManager.GetRolesAsync(user).Result;

            return Ok(userViewModel);
        }

        [HttpGet("{userName}")]
        public IActionResult GetByUserName(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.Roles = _userManager.GetRolesAsync(user).Result;

            return Ok(userViewModel);
        }

        [HttpPut("role")]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult Put(EditUserRoleViewModel userRoleViewModel)
        {
            try
            {
                var user = _userManager.FindByIdAsync(userRoleViewModel.UserId).Result;

                if (user == null) return BadRequest($"UserId ({userRoleViewModel.UserId}) does not exists!");

                string oldRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

                if (!string.IsNullOrEmpty(oldRole))
                {
                    _userManager.RemoveFromRoleAsync(user, oldRole).Wait();
                }
                
                _userManager.AddToRoleAsync(user, userRoleViewModel.Role).Wait();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult Delete(string id)
        {
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;

                if (user == null) return BadRequest($"UserId ({id}) does not exists!");

                _userManager.DeleteAsync(user).Wait();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}