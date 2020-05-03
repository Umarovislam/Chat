using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GoChat.Email;
using GoChat.Enities;
using GoChat.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GoChat.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<object> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            var user = await _userManager.FindByNameAsync(model.Email);
            if (result.Succeeded && await _userManager.CheckPasswordAsync(user, model.Password))
            { 
                return  GenerateJwtToken(model.Email, user);
            }

            return BadRequest(new {message = "UserName or password incorrect"});
        }
        [HttpPost]
        public async Task<object> Register( RegisterDto model)
         {
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Name = model.FullName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail",
                    "Account",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                //SendGridEmailSender emailService = new SendGridEmailSender();
                //await emailService.SendEmailAsync(model.Email, "Confirm your account",
                //    $"Confirm Registration : <a href='http://localhost:4200/user/login'>link</a>");
                await _signInManager.SignInAsync(user, false);
                return Content("Confirm Registration by email");
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<object> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return "model null";
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return GenerateJwtToken(userId, user);
            else
                return null;
        }
        /*public IActionResult SignInWithGoogle()
        {
            var authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action(nameof(HandleExternalLogin)));
            return Challenge(authenticationProperties, "Google");
        }*/
        [ApiExplorerSettings(IgnoreApi = true)]
        private object GenerateJwtToken(string email, ApplicationUser  user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var tokens = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                Token = tokens,
                ExpiresIn = token.ValidTo,
                Id = user.Id
            });
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("http://localhost:4200/user/login");
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> HandleExternalLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (!result.Succeeded) //user does not exist yet
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var newUser = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var createResult = await _userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                    throw new Exception(createResult.Errors.Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));

                await _userManager.AddLoginAsync(newUser, info);
                var newUserClaims = info.Principal.Claims.Append(new Claim("userId", newUser.Id));
                await _userManager.AddClaimsAsync(newUser, newUserClaims);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }

            return Redirect("http://localhost:4200");
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ExternalLoginConfirmation(ApplicationUser model)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            await _userManager.CreateAsync(user);
            
            await _userManager.AddLoginAsync(user, info);

            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok();
        }
    }
}