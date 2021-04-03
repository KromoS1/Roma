using Microsoft.EntityFrameworkCore;
using Todo.Entities;

namespace Todo.DataBase
{
    public class TaskContext : DbContext,ITaskContext
    {
        public virtual DbSet<Tasks> Tasks { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-KCIBM7T\SQLEXPRESS;Initial Catalog=TaskDB;
                Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskCofiguration()) ;
        }

        public void Save()
        {
            base.SaveChanges();
        }

    }
}
