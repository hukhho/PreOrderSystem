using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Enum.User;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.UserRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly PreOrderSystemContext _context;

        public UserRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
        }

        // Add any additional methods specific to the user entity if needed

        public async Task<User> ValidateUserCredentials(string email, string password)
        {
            // Thực hiện kiểm tra tên đăng nhập và mật khẩu
            // Trả về đối tượng User nếu thông tin đăng nhập hợp lệ
            // Trả về null nếu thông tin đăng nhập không hợp lệ

            var user = await _context.Users
                .Include(u => u.Role) // Include the Role entity when querying the User
                .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                return null;
            }


            return user;
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Thực hiện kiểm tra mật khẩu và mã hóa mật khẩu
            // Trả về true nếu mật khẩu hợp lệ, ngược lại trả về false

            // Ví dụ sử dụng BCrypt
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public async Task<User> GetUserWithRoleAndBusinessByIdAsync(Guid id)
        {
            return await GetWithIncludeAsync(u => u.Id == id, u => u.Include(c => c.Role), u => u.Include(c => c.Business));
        }
        public async Task<User> GetUserWithFullDetailsByIdAsync(Guid id)
        {
            return await GetWithIncludeAsync(u => u.Id == id,
                u => u.Include(c => c.Role),
                u => u.Include(c => c.Business),
                u => u.Include(c => c.Payments),
                u => u.Include(c => c.Orders)

                );
        }
        public async Task<IEnumerable<User>> GetAllUsersWithRoleAndBusinessAsync()
        {
            return await GetAllWithIncludeAsync(u => true, u => u.Role, u => u.Business);
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            // Check if the email is unique in the database
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user == null;
        }
        public async Task<bool> IsPhoneUnique(string phone)
        {
            // Check if the phone number is unique in the database
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Phone == phone);
            return user == null;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByActionTokenAsync(string token, ActionType actionType)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ActionToken == token && u.ActionTokenType == actionType);
        }
    }
}