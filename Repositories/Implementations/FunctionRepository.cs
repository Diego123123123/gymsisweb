using GYM.Context;
using GYM.Models;
using Microsoft.EntityFrameworkCore;

namespace GYM.Repositories.Implementations
{
    public class FunctionRepository: BaseRepositoryImplementaion<Function>
    {
        public FunctionRepository(MyAppDbContext myAppDbContext) : base(myAppDbContext)
        {
        }
        public void Update(int id, Function function)
        {
            function.FunctionId = id;
            myAppDbContext.Entry(function).State = EntityState.Modified;
            this.myAppDbContext.SaveChanges();
        }

    }
}
