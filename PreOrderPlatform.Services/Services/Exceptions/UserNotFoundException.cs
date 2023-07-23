namespace PreOrderPlatform.Service.Services.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found")
        { }
    }

}
