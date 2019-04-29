using GYM.Context;
using GYM.Models;
using GYM.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GYM.Repositories.Implementations
{
    public class UserRepositoryImpl : BaseRepositoryImplementaion<User>, UserRepository
    {
        public UserRepositoryImpl(MyAppDbContext myAppDbContext) : base(myAppDbContext)
        {
        }

        public void Update(int id, User user)
        {
            user.UserId = id;
            myAppDbContext.Entry(user).State = EntityState.Modified;
            this.myAppDbContext.SaveChanges();
        }
    }
}
