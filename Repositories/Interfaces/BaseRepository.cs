using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GYM.Repositories
{
    interface BaseRepository<GymEntity> where GymEntity : class
    {

        #region GETS
        GymEntity Get(int id);
        IEnumerable<GymEntity> GetAll();
        IEnumerable<GymEntity> Find(Expression<Func<GymEntity, bool>> predicate);
        #endregion

        #region POST OR ADDING
        void Add(GymEntity gymEntity);
        void AddRange(IEnumerable<GymEntity> entities);
        #endregion

        #region REMOVING OR DELETING
        void Remove(GymEntity gymEntity);
        void RemoveRange(IEnumerable<GymEntity> gymEntities);
        #endregion

    }
}
