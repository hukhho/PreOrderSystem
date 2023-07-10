using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.ApiResponse
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
