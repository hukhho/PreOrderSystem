using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PreorderPlatform.Service.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException()
        {
        }

        public AuthorizationException(string message)
            : base(message)
        {
        }

        public AuthorizationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}