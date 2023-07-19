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

namespace PreorderPlatform.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet("roleandbusiness/{id}")]
        public async Task<IActionResult> GetUserWithRoleAndBusinessById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserWithRoleAndBusinessByIdAsync(id);
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

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequest model)
        {
            try
            {
                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                model.Status = true;

                var user = await _userService.CreateUserAsync(model);

                return CreatedAtAction(nameof(GetUserById),
                                       new { id = user.Id },
                                       new ApiResponse<UserResponse>(user, "User created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating user: {ex.Message}", false, null));
            }
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test(UserCreateRequest model)
        {

            return Ok(new ApiResponse<object>(null, "tét.", true, null));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest model)
        {
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
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>(null, "Invalid request data.", false, null));
                }
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new ApiResponse<string>(null, "User not found.", false, null));
                }
                await _userService.UpdateUserPasswordAsync(user, model.NewPassword);

                return Ok(new ApiResponse<object>(null, "Password reset successful.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error resetting password: {ex.Message}", false, null));
            }
        }
        [HttpPost("get-reset-token")]
        public async Task<IActionResult> GetResetTokenAsync(GetResetPasswordToken model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<object>(null, "Invalid request data.", false, null));
                }
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new ApiResponse<string>(null, "User not found.", false, null));
                }
                var resetToken = _userService.GeneratePasswordResetToken();
                await SendPasswordResetEmail(user.Email, resetToken);

                return Ok(new ApiResponse<object>(null, "Password reset token sent successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error sending password reset token: {ex.Message}", false, null));
            }
        }

        // Method to send the password reset email using your email service
        private async Task SendPasswordResetEmail(string userEmail, string resetToken)
        {
            try
            {
                string emailFrom = "myworkemail110@gmail.com";
                string emailPassword = "Sonaco160911";

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailFrom, emailPassword);

                    var emailMessage = new MailMessage();
                    emailMessage.From = new MailAddress(emailFrom, "Pre-Order System Dev Team");
                    emailMessage.To.Add(userEmail);
                    emailMessage.Subject = "Pre-Order System :: Get Your Reset Key";
                    emailMessage.Body = $"Hello, you have requested to reset your password. This is your reset key: {resetToken}. If you did not request a password reset, you can ignore this email.";

                    await client.SendMailAsync(emailMessage);
                Console.WriteLine(1);
                }

                // Add any additional error handling or logging if needed
            }
            catch (Exception ex)
            {
                // Handle the email sending error or log the exception
                throw new Exception("Failed to send the email.", ex);
            }
        }
    }
}