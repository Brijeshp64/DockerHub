using Microsoft.EntityFrameworkCore;
using TaskTodo.Model.Entity;

namespace TaskTodo.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        //public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskData> TaskData { get; set; }
        public DbSet<User> users { get; set; }

    }
}
