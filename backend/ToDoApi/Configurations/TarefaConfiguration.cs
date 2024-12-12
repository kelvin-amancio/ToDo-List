using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApi.Models;

namespace ToDoApi.Configurations
{
    public class TarefaConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.Property(t => t.Title)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(t => t.Description)
                   .HasMaxLength(200);

            builder.Property(t => t.Completed)
                   .IsRequired();        

            builder.HasOne(t => t.User)
                   .WithMany()              
                   .HasForeignKey(t => t.UserId);
        }
    }
}
