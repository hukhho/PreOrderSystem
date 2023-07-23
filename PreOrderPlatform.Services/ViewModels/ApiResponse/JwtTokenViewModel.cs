namespace PreOrderPlatform.Service.ViewModels.ApiResponse
{
    public class JwtTokenViewModel
    {
        public string JwtToken { get; set; }

        public JwtTokenViewModel(string jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
