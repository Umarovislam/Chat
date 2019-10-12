using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoChat.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoChat.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserProfileController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _appEnvironment;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetUserProfile(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return new
            {
                user.Name,
                user.Email,
                user.UserName,
                user.PhoneNumber,
                user.PictureUrl
            };
        }

        [HttpPut]
        [Authorize (AuthenticationSchemes = "Bearer")]
        public async Task<object> UpdateUser(UserInfo user)
        {
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + $"/Images/{user.PictureUrl.FileName}", FileMode.Create))
            {
                await user.PictureUrl.CopyToAsync(fileStream);
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _user = db.Users.FindAsync(user.Email).Result;
                _user.Name = user.Name;
                _user.PictureUrl = user.PictureUrl.FileName;
                _user.UserName = user.UserName;
                db.Entry(_user).State = EntityState.Modified;
            }

            return Ok();
        }
    }
}