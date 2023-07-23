using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.ViewModels.User.Request;

namespace PreOrderPlatform.Service.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> LoginService(LoginRequest loginViewModel);
        // Add other method signatures related to authentication and login
    }
}
