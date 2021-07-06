using System;
using System.Linq;
using System.Threading.Tasks;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using aduaba.api.Extensions;
using aduaba.api.Interface;
using aduaba.api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aduaba.api.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserInterface _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserInterface userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/[controller]/RegisterUser")]
        public async Task<ActionResult> RegisterAsync(RegisterUser model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/[controller]/UserLogin")]
        public async Task<IActionResult> UserLoginAsync(TokenRequestModel model)
        {
            var result = await _userService.UserLoginAsync(model);
            if (result.IsAuthenticated == true)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("/api/[controller]/UpdateUserRole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("/api/[controller]/RefreshUserToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [Authorize]
        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("/api/[controller]/GetUserRefreshToken")]
        public IActionResult GetRefreshTokens([FromQuery] string id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("/api/[controller]/RevokeUserToken")]
        public IActionResult RevokeToken([FromBody] RevokeUserTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });
            var response = _userService.RevokeToken(token);
            if (!response)
                return NotFound(new { message = "Token not found" });
            return Ok(new { message = "Token revoked" });
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        [Route("/api/[controller]/UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromQuery] string Id, [FromBody] UpdateUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            var ApplicationUser = new ApplicationUser()
            {

                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
            if (!(string.IsNullOrEmpty(model.ProfileImageFilePath)))
            {
                ApplicationUser.ProfileImageUrl = ImageUpload.ImageUploads(model.ProfileImageFilePath);
            };

            var result = await _userService.UpdateUserAsync(Id, ApplicationUser);

            return Ok($"Your Details have been Updated Successfully : {model}");

        }

        [HttpGet]
        [Authorize(Roles ="Administrator")]
        [Route("api/[controller]/GetAllUsers")]
        public async Task<IActionResult> GetAllRegisteredUsers()
        {
            var users = (await _userManager.GetUsersInRoleAsync("User")).ToArray();

            return Ok(users);
        }

        [HttpGet]
        [Authorize(Roles ="Administrator")]
        [Route("api/[controller]/GetAllAdministrators")]
        public async Task<IActionResult> GetAllRegisteredAdministrators()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Administrator")).ToArray();

            return Ok(admins);
        }
    }
}