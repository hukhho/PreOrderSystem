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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok(new ApiResponse<object>(null, "User deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting user: {ex.Message}", false, null));
            }
        }
    }
}