using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Value> Value { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Sample> Sample { get; set; }
        public DbSet<User> User { get; set; }

    }
}