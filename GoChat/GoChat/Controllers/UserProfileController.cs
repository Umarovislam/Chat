using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoChat.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoChat.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetUserProfile(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return new
            {
                user.Name,
                user.Email,
                user.UserName,
                user.PhoneNumber,
                user.PictureUrl
            };
        }
    }
}