using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.Services.AuthService;
using PreorderPlatform.Service.Services.UserServices;
using System.Drawing.Text;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using PreorderPlatform.Service.Utility.CustomAuthorizeAttribute;
using PreorderPlatform.Service.ViewModels.User.Request;

namespace PreorderPlatform.API.Controllers
{
    //TEST

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;


        public AuthController(IUserService userService, IAuthService authService, IJwtService jwtService)
        {
            _userService = userService;
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("admin-only")]
        [CustomAuthorize(Roles = "ADMIN")]
        public IActionResult AdminOnly()
        {
            return Ok("This endpoint is accessible only to users with the Admin role.");
        }

        [HttpGet("bussiness-owner-only")]
        [CustomAuthorize(Roles = "BUSSINESS_OWNER")]
        public IActionResult UserOnly()
        {
            return Ok("This endpoint is accessible only to users with the User role.");
        }

        [HttpGet("bussiness-staff-only")]
        [CustomAuthorize(Roles = "BUSSINESS_STAFF")]
        public IActionResult TestOnly()
        {
            return Ok("This endpoint is accessible only to users with the Test and Admin role.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            // Validate the user's credentials and retrieve the user
            // (replace this with your own logic)
            var user = await _authService.LoginService(model);

            if (user != null)
            {
                if (user?.Role == null)
                {
                    return BadRequest(new ApiResponse<object>(null, "User don't have any role!", false, null));
                } else
                {
                    try
                    {
                        string token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, user.Role.Name);
                        JwtTokenViewModel jwtTokenViewModel = new JwtTokenViewModel(token);
                        return Ok(new ApiResponse<JwtTokenViewModel>(jwtTokenViewModel, "Login successful", true, null));

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new ApiResponse<object>(null, "Some error occur!", false, null));

                    }
                }
            } else {
                return BadRequest(new ApiResponse<object>(null, "Invalid username or password", false, null));
            }
        }
    }
}
