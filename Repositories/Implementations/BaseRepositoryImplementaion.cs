using GYM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GYM.Repositories.Implementations
{
    public class BaseRepositoryImplementaion<GymEntity> : BaseRepository<GymEntity> where GymEntity : class
    {

        protected readonly MyAppDbContext myAppDbContext;

        public BaseRepositoryImplementaion(MyAppDbContext myAppDbContext)
        {
            this.myAppDbContext = myAppDbContext;
        }

        public void Add(GymEntity gymEntity)
        {
            this.myAppDbContext.Set<GymEntity>().Add(gymEntity);
           
        }

        public void AddRange(IEnumerable<GymEntity> entities)
        {
            this.myAppDbContext.Set<GymEntity>().AddRange(entities);
        }

        public IEnumerable<GymEntity> Find(Expression<Func<GymEntity, bool>> predicate)
        {
            return this.myAppDbContext.Set<GymEntity>().Where(predicate);
        }

        public GymEntity Get(int id)
        {
            return this.myAppDbContext.Set<GymEntity>().Find(id);
        }

        public IEnumerable<GymEntity> GetAll()
        {
            return this.myAppDbContext.Set<GymEntity>().ToList();
        }

        public void Remove(GymEntity gymEntity)
        {
            this.myAppDbContext.Set<GymEntity>().Remove(gymEntity);
        }

        public void RemoveRange(IEnumerable<GymEntity> gymEntities)
        {
            this.myAppDbContext.Set<GymEntity>().RemoveRange(gymEntities);
        }

    }
}
