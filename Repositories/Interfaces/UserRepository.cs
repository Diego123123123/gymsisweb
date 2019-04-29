using GYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYM.Repositories.Interfaces
{
    interface UserRepository
    {
        void Update(int id, User user);
    }
}
