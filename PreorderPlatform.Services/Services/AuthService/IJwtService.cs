namespace PreOrderPlatform.Service.Services.AuthService
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string userName, string role);
    }
}
