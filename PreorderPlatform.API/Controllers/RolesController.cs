using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.Services.RoleServices;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.ViewModels.Role;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService roleService, IMapper mapper, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpPost("/createRole")]
        //public IActionResult CreateRoleTest(Role role)
        //{

        //    return Ok("Oke");
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetRolesAsync();
                return Ok(new ApiResponse<List<RoleDetailViewModel>>(roles, "Roles fetched successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching roles: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(id);
                return Ok(new ApiResponse<RoleDetailViewModel>(role, "Role fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching role: {ex.Message}", false, null));
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRole(RoleCreateViewModel model)
        //{
        //    try
        //    {
        //        var role = await _roleService.CreateRoleAsync(model);
        //        return CreatedAtAction(nameof(GetRoleById),
        //                               new { id = role.Id },
        //                               new ApiResponse<RoleDetailViewModel>(role, "Role created successfully.", true, null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponse<object>(null, $"Error creating role: {ex.Message}", false, null));
        //    }
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateRole(RoleDetailViewModel model)
        //{
        //    try
        //    {
        //        await _roleService.UpdateRoleAsync(model);
        //        return Ok(new ApiResponse<object>(null, "Role updated successfully.", true, null));
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponse<object>(null, $"Error updating role: {ex.Message}", false, null));
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRole(Guid id)
        //{
        //    try
        //    {
        //        await _roleService.DeleteRoleAsync(id);
        //        return Ok(new ApiResponse<object>(null, "Role deleted successfully.", true, null));
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponse<object>(null, $"Error deleting role: {ex.Message}", false, null));
        //    }
        //}
    }
}