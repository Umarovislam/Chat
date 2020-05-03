using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GoChat.Entities;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ApplicationDbContext _db;

        public UserProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            this._userManager = userManager;
            this._db = db;
        }

        [HttpGet]
        //[Authorize]
        [AllowAnonymous]
        public async Task<object> GetUser(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user != null)
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
        [Authorize]
        public async Task<IActionResult> UpdateAvatar(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found");
            var file = HttpContext.Request.Form.Files?.First();
            var folderName = Path.Combine("Resources", "Images");
            
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                user.Avatar = dbPath;
                await _userManager.UpdateAsync(user);
                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfo user)
        {
            var _user = await _db.Users.FindAsync(user.Email);
            if (_user != null)
            {
                _db.Entry(user).State = EntityState.Modified;
                return Ok();
            }
            return BadRequest("User not found");
        }
    }
}