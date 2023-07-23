using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Entity.Enum.User;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Services.SendMailServices;
using PreOrderPlatform.Service.Services.UserServices;
using PreOrderPlatform.Service.Utility.CustomAuthorizeAttribute;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.User.Request;
using PreOrderPlatform.Service.ViewModels.User.Response;

namespace PreOrderPlatform.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UsersController(IUserService userService, IMapper mapper, IEmailService emailService, IWebHostEnvironment hostingEnvironment)
        {
            _userService = userService;
            _mapper = mapper;
            _emailService = emailService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [CustomAuthorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] PaginationParam<UserEnum.UserSort> paginationModel,
            [FromQuery] UserSearchRequest searchModel
        )
        {
            try
            {
                var start = DateTime.Now;
                var (users, totalItems) = await _userService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<UserResponse>>(
                    users,
                    "Users fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching users: {ex.Message}", false, null));
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyDetails()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Guid userIdGuid = Guid.Parse(userId);

                var user = await _userService.GetUserByIdAsync(userIdGuid);
             
                // var userFullDetails = _mapper.Map<UserResponse>(user);

                return Ok(new ApiResponse<object>(user, $"Get user info successfully", false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching user details: {ex.Message}", false, null));
            }
        }


        [HttpGet("{id}")]
        [CustomAuthorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(new ApiResponse<UserResponse>(user, "User fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching user: {ex.Message}", false, null));
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token, string actiontype)
        {
            try
            {
                if (!Enum.TryParse(actiontype, out ActionType actionType))
                {
                    throw new ArgumentException("Invalid action type", nameof(actiontype));
                }

                if (actionType.Equals(ActionType.AccountActivation))
                {
                    var result = await _userService.ConfirmEmail(token, actionType, null);

                    if (result.Status.GetValueOrDefault().Equals(UserStatus.Active))
                    {
                        return Ok(new ApiResponse<UserResponse>(result, "User already active!", true, null));
                    } else if (result.Status.GetValueOrDefault().Equals(UserStatus.Suspended))
                    {
                        return Ok(new ApiResponse<UserResponse>(result, "User suspended!", true, null));
                    }               

                    string templateName = "EmailTemplateWelcome.html";
                    string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                    string templatePath = Path.Combine(rootPath, "Templates", templateName);
                    string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
                    string emailBody = htmlTemplate
                        .Replace("{username}", result.Email);
                    
                    await _emailService.SendEmailAsync(email, "Welcome to Pre Order Platform", emailBody);

                    return Ok(new ApiResponse<UserResponse>(result, "User active succesfully!", true, null));
                } else if (actionType.Equals(ActionType.PasswordReset))
                {
                    var rawPass = Guid.NewGuid().ToString().Replace("-", "");
                    var newPassword = BCrypt.Net.BCrypt.HashPassword(rawPass);

                    var result = await _userService.ConfirmEmail(token, actionType, newPassword);

                    string templateName = "EmailTemplatePasswordSend.html";
                    string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                    string templatePath = Path.Combine(rootPath, "Templates", templateName);
                    string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
                    string emailBody = htmlTemplate
                        .Replace("{username}", result.Email)
                        .Replace("{newPassword}", rawPass);

                    await _emailService.SendEmailAsync(email, "Password Reset Successfully Pre Order Platform", emailBody);

                    return Ok(new ApiResponse<UserResponse>(result, "User reset password succesfully!", true, null));
                }
                else
                {
                    throw new Exception("Action not found");
                }
               
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       new ApiResponse<object>(null, $"Error confirm email: {ex.Message}", false, null));
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequest model)
        {
            try
            {
                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var user = await _userService.RegisterUser(model);


                var token = user.ActionToken;

                if (token == null)
                {
                    throw new Exception("Token is null");
                }

              
                var actionString = user.ActionTokenType.ToString();

                var callbackUrl = Url.Action("ConfirmEmail",
                                     "Users",
                                     new { email = user.Email, token = token, actiontype = actionString },
                                     protocol: HttpContext.Request.Scheme);

                Console.WriteLine(callbackUrl);
                // Path to the email template
                // Template name
                string templateName = "EmailTemplateRegister.html";

                // Path to the email template
                string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                string templatePath = Path.Combine(rootPath, "Templates", templateName);

                // Load the content of the email template
                string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
                // Load the content of the email template

                // Replace placeholders with actual values
                string emailBody = htmlTemplate
                    .Replace("{username}", model.Email)
                    .Replace("{activationLink}", callbackUrl);

                // Call your email service, passing in the HTML template
                await _emailService.SendEmailAsync(user.Email, "Actice Account - Pre Order Platform", emailBody);

                var userResponse = _mapper.Map<UserResponse>(user);

                return CreatedAtAction(nameof(GetUserById),
                                       new { id = user.Id },
                                       new ApiResponse<UserResponse>(userResponse, "User created successfully. You will revice link to active account", true, null));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating user: {ex.Message}", false, null));
            }
        }



        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          
            var roleName = User.FindFirst(ClaimTypes.Role)?.Value;

            Guid userIdGuid = Guid.Parse(userId);

            if (roleName != "ADMIN" || userIdGuid != model.Id)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<object>(null, "User {}", true, null));
            }


            try
            {
                await _userService.UpdateUserAsync(model);
                return Ok(new ApiResponse<object>(null, "User updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating user: {ex.Message}", false, null));
            }
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordRequest model)
        {
            try
            {
                var result = await _userService.ForgotPassword(model);
                var user = _mapper.Map<User>(result);
                var token = user.ActionToken;

                if (token == null)
                {
                    throw new Exception("Token is null");
                }
                var actionString = user.ActionTokenType.ToString();

                var callbackUrl = Url.Action("ConfirmEmail",
                                     "Users",
                                     new { email = user.Email, token = token, actiontype = actionString },
                                     protocol: HttpContext.Request.Scheme);

                string templateName = "EmailTemplatePasswordReset.html";
                string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                string templatePath = Path.Combine(rootPath, "Templates", templateName);
                string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
              

                string emailBody = htmlTemplate
                      .Replace("{username}", user.Email)
                      .Replace("{resetPasswordLink}", callbackUrl);


                await _emailService.SendEmailAsync(user.Email, "Pass reset to Pre Order Platform", emailBody);

                return Ok(new ApiResponse<object>(null, "Send mail reset pass successfully.", true, null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("send-mail-again")]
        public async Task<IActionResult> SendMailAgain(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email);
                var token = user.ActionToken;

                if (token == null)
                {
                    throw new Exception("Token is null");
                }

                if (user.ActionTokenType.Equals(ActionType.AccountActivation))
                {

                    var actionString = user.ActionTokenType.ToString();
                   
                    
                    var callbackUrl = Url.Action("ConfirmEmail",
                                         "Users",
                                         new { email = user.Email, token = token, actiontype = actionString },
                                         protocol: HttpContext.Request.Scheme);
                 

                    string templateName = "EmailTemplateRegister.html";
                    string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                    string templatePath = Path.Combine(rootPath, "Templates", templateName);
                    string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
                    string emailBody = htmlTemplate
                        .Replace("{username}", email)
                            .Replace("{activationLink}", callbackUrl);
                    await _emailService.SendEmailAsync(email, "Active account to Pre Order Platform", emailBody);

                    return Ok(new ApiResponse<object>(null, "Send mail active succesfully!", true, null));
                 }
                 else if (user.ActionTokenType.Equals(ActionType.PasswordReset))
                 {
                    var actionString = user.ActionTokenType.ToString();

                    var callbackUrl = Url.Action("ConfirmEmail",
                                         "Users",
                                         new { email = user.Email, token = token, actiontype = actionString },
                                         protocol: HttpContext.Request.Scheme);


                    string templateName = "EmailTemplatePasswordReset.html";
                    string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                    string templatePath = Path.Combine(rootPath, "Templates", templateName);
                    string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);


                    string emailBody = htmlTemplate
                          .Replace("{username}", user.Email)
                          .Replace("{resetPasswordLink}", callbackUrl);


                    await _emailService.SendEmailAsync(user.Email, "Pass reset to Pre Order Platform", emailBody);

                    return Ok(new ApiResponse<object>(null, "Send mail reset pass succesfully!", true, null));

                }
                else
                {
                    throw new Exception("Action not found");
                }

            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       new ApiResponse<object>(null, $"Error confirm email: {ex.Message}", false, null));
            }
        }
    }
}