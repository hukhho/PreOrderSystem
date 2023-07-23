using PreOrderPlatform.Entity.Enum.User;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.UserRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllUsersWithRoleAndBusinessAsync();
        Task<User> GetUserWithRoleAndBusinessByIdAsync(Guid id);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsPhoneUnique(string phone);
        Task<User> ValidateUserCredentials(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByActionTokenAsync(string token, ActionType actionType);
        Task<User> GetUserWithFullDetailsByIdAsync(Guid id);
    }
}
