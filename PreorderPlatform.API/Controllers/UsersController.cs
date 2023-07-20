using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.Services.AuthService;
using PreorderPlatform.Service.Services.UserServices;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using PreorderPlatform.Service.Services.Exceptions;
using PreorderPlatform.Service.Exceptions;
using BCrypt;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PreorderPlatform.Service.ViewModels.User.Request;
using PreorderPlatform.Service.ViewModels.User.Response;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using System.Net.Mail;
using System.Net;
using NuGet.Protocol;
using PreorderPlatform.Service.Utility.CustomAuthorizeAttribute;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MailKit;
using PreorderPlatform.Service.Services.SendMailServices;
using NuGet.Common;
using Newtonsoft.Json.Linq;

namespace PreorderPlatform.API.Controllers
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
        public async Task<IActionResult> ConfirmEmail(string email, string token, Entity.Repositories.Enum.User.ActionType action)
        {
            try
            {
                if(action.Equals(Entity.Repositories.Enum.User.ActionType.AccountActivation))
                {
                    var result = await _userService.ConfirmEmail(token, action);

                    string templateName = "EmailTemplateWelcome.html";
                    string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                    string templatePath = Path.Combine(rootPath, "Templates", templateName);
                    string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
                    string emailBody = htmlTemplate
                        .Replace("{username}", result.Email);

                    await _emailService.SendEmailAsync(email, "Welcome to Pre Order Platform", emailBody);

                    return Ok(new ApiResponse<UserResponse>(result, "User active succesfully!", true, null));
                } else if (action.Equals(Entity.Repositories.Enum.User.ActionType.PasswordReset))
                {
                    var rawPass = Guid.NewGuid().ToString().Replace("-", "");
                    var newPassword = BCrypt.Net.BCrypt.HashPassword(rawPass);

                    var result = await _userService.ConfirmEmail(token, action);

                    string templateName = "EmailTemplatePasswordSend.html";
                    string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                    string templatePath = Path.Combine(rootPath, "Templates", templateName);
                    string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
                    string emailBody = htmlTemplate
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

                // Generate the email confirmation link
                var callbackUrl = Url.Action("confirm-email",
                                             "Users",
                                              new { email = user.Email, token = token, action = user.ActionTokenType },
                                              protocol: HttpContext.Request.Scheme);


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
                var callbackUrl = Url.Action("confirm-email",
                                       "Users",
                                        new { email = user.Email, token = token, action = user.ActionTokenType },
                                        protocol: HttpContext.Request.Scheme);

                string templateName = "EmailTemplatePasswordReset.html";
                string rootPath = _hostingEnvironment.WebRootPath ?? _hostingEnvironment.ContentRootPath;
                string templatePath = Path.Combine(rootPath, "Templates", templateName);
                string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
              

                string emailBody = htmlTemplate
                      .Replace("{username}", user.Email)
                      .Replace("{activationLink}", callbackUrl);


                await _emailService.SendEmailAsync(user.Email, "Pass reset to Pre Order Platform", emailBody);

                return Ok(result);
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

                if (user.ActionToken.Equals(Entity.Repositories.Enum.User.ActionType.AccountActivation))
                {
                    var callbackUrl = Url.Action("confirm-email",                     
                                                 "Users",
                                                 new { email = user.Email, token = token, action = user.ActionTokenType },
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
                 else if (user.ActionToken.Equals(Entity.Repositories.Enum.User.ActionType.PasswordReset))
                 {   
                    var callbackUrl = Url.Action("confirm-email",
                                               "Users",
                                                new { email = user.Email, token = token, action = user.ActionTokenType },
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