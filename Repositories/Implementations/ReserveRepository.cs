using GYM.Context;
using GYM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GYM.Repositories.Implementations
{
    public class ReserveRepository : BaseRepositoryImplementaion<Reserva>
    {
        public ReserveRepository(MyAppDbContext myAppDbContext) : base(myAppDbContext)
        {
        }

        public IEnumerable<UserReserves> GetReservasByUserId(string userId)
        {
            var items =  this.myAppDbContext.Set<Reserva>().Where(x => x.User == userId).ToList();
            var resp = new List<UserReserves>();
            for (int i = 0; i < items.Count(); i++)
            {
                var reserve = new UserReserves();
                reserve.ReserveId = items[i].ReserveId;
                reserve.User = userId;
                reserve.AmountOfPeople = items[i].AmountOfPeople;
                reserve.Function = myAppDbContext.Set<Function>().Where(x => x.FunctionId == items[i].FunctionId).FirstOrDefault();
                resp.Add(reserve);
            }
            return resp;
        }
        public void Update(int id, Reserva reserva)
        {
            reserva.ReserveId = id;
            myAppDbContext.Entry(reserva).State = EntityState.Modified;
            this.myAppDbContext.SaveChanges();
        }
    }
}
