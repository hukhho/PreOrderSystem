using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Enum
{
    public static class UserEnum
    {
        public enum UserSort
        {
           Id, 
           FirstName,
           LastName,
           Phone,
           Email,
           Password,
           Address,
           Ward,
           District,
           Province,
           Status,
           RoleId,
           BusinessId
        }

        public enum UserActive
        {
            /// <summary>
            /// Status for deleted or blocked
            /// </summary>
            Inactive = 0,

            /// <summary>
            /// Status for active
            /// </summary>
            Active = 1,
        }
    }
}
