using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.AuthorizationService.Payment
{
    public class MustBePaymentAccessRequirement : IAuthorizationRequirement
    {
    }
}
