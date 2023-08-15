using DemoWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoWebApi
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Tags> Tags { get; set; }
        public DbSet<UserTags> UserTags { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<ScopeTags> ScopeTags { get; set; }
    }
}
