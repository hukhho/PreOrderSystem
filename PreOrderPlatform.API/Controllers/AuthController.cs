using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Service.Services.AuthService;
using PreOrderPlatform.Service.Services.UserServices;
using PreOrderPlatform.Service.Utility.CustomAuthorizeAttribute;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.User.Request;

namespace PreOrderPlatform.API.Controllers
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

        //[HttpGet]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var users = await _userService.GetUsersAsync();
        //    return Ok(users);
        //}

        //[HttpGet("admin-only")]
        //[CustomAuthorize(Roles = "ADMIN")]
        //public IActionResult AdminOnly()
        //{
        //    return Ok("This endpoint is accessible only to users with the Admin role.");
        //}

        //[HttpGet("test-change-post-man")]
        //[CustomAuthorize(Roles = "ADMIN")]
        //public IActionResult TestChange()
        //{
        //    return Ok("This endpoint is accessible only to users with the Admin role.");
        //}


        //[HttpGet("bussiness-owner-only")]
        //[CustomAuthorize(Roles = "BUSSINESS_OWNER")]
        //public IActionResult UserOnly()
        //{
        //    return Ok("This endpoint is accessible only to users with the User role.");
        //}

        //[HttpGet("bussiness-staff-only")]
        //[CustomAuthorize(Roles = "BUSSINESS_STAFF")]
        //public IActionResult TestOnly()
        //{
        //    return Ok("This endpoint is accessible only to users with the Test and Admin role.");
        //}

        //[HttpGet("test-nha-only")]
        //public IActionResult TestOnlyOk()
        //{
        //    return Ok("Test Nha Only");
        //}

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
