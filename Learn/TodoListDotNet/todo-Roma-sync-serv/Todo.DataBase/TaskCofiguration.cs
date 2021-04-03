using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Entities;

namespace Todo.DataBase
{
    class TaskCofiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            var typeBuilder = builder.ToTable("Task");
            typeBuilder.HasKey(x => x.ID);
            typeBuilder.Property(x => x.ID).HasColumnName("ID");
            typeBuilder.Property(x=>x.Name).HasColumnName("Name");
            typeBuilder.Property(x => x.Priority).HasColumnName("Priority");
            typeBuilder.Property(x => x.Start).HasColumnName("Start");
            typeBuilder.Property(x => x.Due).HasColumnName("Due");
            typeBuilder.Property(x => x.Status).HasColumnName("Status");
        }
    }
}
