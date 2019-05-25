using GYM.Context;
using GYM.Models;

namespace GYM.Repositories.Implementations
{
    public class FunctionRepository: BaseRepositoryImplementaion<Function>
    {
        public FunctionRepository(MyAppDbContext myAppDbContext) : base(myAppDbContext)
        {
        }

    }
}
