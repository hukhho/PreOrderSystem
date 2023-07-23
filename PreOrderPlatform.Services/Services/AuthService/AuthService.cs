using System.Security.Authentication;
using Microsoft.Extensions.Configuration;
using PreOrderPlatform.Entity.Enum.User;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Entity.Repositories.UserRepositories;
using PreOrderPlatform.Service.ViewModels.User.Request;

namespace PreOrderPlatform.Service.Services.AuthService
{
    internal class AuthService : IAuthService
    {
        private readonly string _jwtSecret;
        // Thêm DataContext nếu bạn sử dụng Entity Framework Core
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _jwtSecret = configuration.GetSection("Jwt:Secret").Value;
            _userRepository = userRepository;
        }

        // Các phương thức khác liên quan đến xác thực và đăng nhập

        public async Task<User> LoginService(LoginRequest loginViewModel)
        {
            // Thực hiện kiểm tra tên đăng nhập và mật khẩu
            // Trả về đối tượng User nếu thông tin đăng nhập hợp lệ
            // Trả về null nếu thông tin đăng nhập không hợp lệ

            var user = await _userRepository.ValidateUserCredentials(loginViewModel.Email, loginViewModel.Password);
            if (user == null)
            {
                throw new AuthenticationException("Invalid username or password.");
            }

            // Check the user's status
            if (user.Status != UserStatus.Active)
            {
                throw new AuthenticationException("User status is not Active. User cannot be authenticated.");
            }
            return user;
        }

        
    }
}
