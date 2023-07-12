using PreorderPlatform.Entity.Models;
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
        Task<User> GetUserWithRoleAndBusinessByIdAsync(int id);
        Task<User> ValidateUserCredentials(string email, string password);
        Task<User> GetUserByIdAsync(int id);
    }
}
