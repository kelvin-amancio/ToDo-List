using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoApi.Models;

namespace ToDoApi.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> opt) : DbContext(opt)
    {
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<User> User { get; set; }
        public DbSet<TaskItem> TaskItem { get; set; }
    }
}
