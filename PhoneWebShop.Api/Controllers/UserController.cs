using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneWebShop.Api.Models;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhoneWebShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserInputModel userInfo)
        {
            try
            {
                var result = await _userManager.CreateAsync(new IdentityUser 
                { 
                    Email = userInfo.Email, 
                    UserName = userInfo.Email
                }, password: userInfo.Password);
                
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(new { Success = true });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserInputModel userInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest("Bad model");

            var result = await _signInManager.PasswordSignInAsync(
                userInfo.UserName, userInfo.Password, userInfo.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                if (await _userManager.FindByEmailAsync(userInfo.UserName) == null)
                    return BadRequest("Login Failed: User does not exist.");
                return BadRequest("Login Failed");
            }
            var user = await _userManager.FindByEmailAsync(userInfo.UserName);

            List<Claim> userClaims = new List<Claim>
            {
                new Claim("UserId", user.Id)
            };

            string jwt = _tokenService.Generate(userClaims);
            return Ok(new
            {
                Success = true,
                Token = jwt
            });
        }
    }
}
