using GYM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GYM.Context
{
    public class MyAppDbContext : IdentityDbContext<IdentityClient>
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Reserva> Reserves { get; set; }
        public DbSet<Function> Functions { get; set; }

    }
}
