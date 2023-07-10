using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.ViewModels.User.Request;
using PreorderPlatform.Service.ViewModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.UserServices
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(UserCreateRequest model);
        Task DeleteUserAsync(int id);
        Task<List<UserResponse>> GetAllUsersWithRoleAndBusinessAsync();
        Task<UserResponse> GetUserByIdAsync(int id);
        Task<List<UserResponse>> GetUsersAsync();
        Task<UserResponse> GetUserWithRoleAndBusinessByIdAsync(int id);
        Task UpdateUserAsync(UserUpdateRequest model);
    }
}
