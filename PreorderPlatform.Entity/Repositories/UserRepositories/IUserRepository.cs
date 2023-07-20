using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.Enum.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.UserRepositories
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
    }
}
