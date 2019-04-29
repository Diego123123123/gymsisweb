using GYM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GYM.Context
{
    public class MyAppDbContext : IdentityDbContext<User>
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

    }
}
