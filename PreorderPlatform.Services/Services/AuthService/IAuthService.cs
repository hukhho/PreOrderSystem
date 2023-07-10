using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.ViewModels.User.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> LoginService(LoginRequest loginViewModel);
        // Add other method signatures related to authentication and login
    }
}
