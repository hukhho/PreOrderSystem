using PreOrderPlatform.Entity.Enum.User;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.User.Request;
using PreOrderPlatform.Service.ViewModels.User.Response;

namespace PreOrderPlatform.Service.Services.UserServices
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
        Task<User> RegisterUser(UserCreateRequest model);
        Task<User> ForgotPassword(UserForgotPasswordRequest model);
        Task<bool> IsTokenValid(string token, ActionType actionType);
        Task<UserResponse> ConfirmEmail(string token, ActionType actionType, string? newHashPass);
        Task<UserResponse> ResendActiveMail(UserForgotPasswordRequest model);
    }
}
