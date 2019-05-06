using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYM.Models
{
    public class UserInfoForSignUp
    {
        #region USER PROPERTIES FOR SIGNING UP
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        #endregion
    }
}
