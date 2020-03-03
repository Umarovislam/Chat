using System.IO;
using System.Threading.Tasks;
using GoChat.Entities;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser,UserInfo>())..CreateMapper();
            var userInfo = mapper.Map<ApplicationUser, UserInfo>(user);
                                                                
            return 
            //    new
            //{
            //    user.Name,
            //    user.Email,
            //    user.UserName,
            //    user.PhoneNumber,
            //    user.Avatar
            //};
        }

        [HttpGet]
        [Authorize]
        public async Task<object> GetUser(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if(user != null)
            {
                return new
                {
                    user.Name,
                    user.Email,
                    user.UserName,
                    user.PhoneNumber,
                    user.Avatar
                };
            }

            return user;
        }

        [HttpPut]
        [Authorize (AuthenticationSchemes = "Bearer")]
        public async Task<object> UpdateUser(UserInfo user)
        {

            user.PictureUrl = Request.Form.Files[0];
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + $"/Images/{user.PictureUrl.FileName}", FileMode.Create))
            {
                await user.PictureUrl.CopyToAsync(fileStream);
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var _user = db.Users.FindAsync(user.Email).Result;
                _user.Name = user.Name;
                _user.Avatar = user.PictureUrl.FileName;
                _user.UserName = user.UserName;
                db.Entry(_user).State = EntityState.Modified;
            }

            return Ok();
        }
    }
}