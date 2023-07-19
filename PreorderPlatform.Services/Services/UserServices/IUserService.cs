using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.User.Request;
using PreorderPlatform.Service.ViewModels.User.Response;

namespace PreorderPlatform.Service.Services.UserServices
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(UserCreateRequest model);

        Task DeleteUserAsync(Guid id);

        Task<List<UserResponse>> GetAllUsersWithRoleAndBusinessAsync();

        Task<UserResponse> GetUserByIdAsync(Guid id);

        Task<List<UserResponse>> GetUsersAsync();

        Task<UserResponse> GetUserWithRoleAndBusinessByIdAsync(Guid id);

        Task UpdateUserAsync(UserUpdateRequest model);

        Task<(IList<UserResponse> users, int totalItems)>
        GetAsync(

                PaginationParam<UserEnum.UserSort> paginationModel,
                UserSearchRequest filterModel

        );

        Task<bool> IsEmailUniqueAsync(string email);

        Task<bool> IsPhoneUniqueAsync(string phone);
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateUserPasswordAsync(User user, string newPassword);
        string GeneratePasswordResetToken();
    }
}
