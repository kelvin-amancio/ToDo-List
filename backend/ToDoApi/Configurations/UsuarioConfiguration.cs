using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApi.Models;

namespace ToDoApi.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.UserName)
                    .HasMaxLength(100)
                    .IsRequired();

            builder.Property(u => u.Password)
                    .HasMaxLength(200)
                    .IsRequired();            
        }
    }
}
