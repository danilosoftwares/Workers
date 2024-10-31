using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerModels.Model;

namespace WorkerRepositories.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id).HasName("users_pkey");
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");

            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(50)
                  .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                  .IsRequired()
                  .HasMaxLength(256)
                  .HasColumnName("passwordhash");
        }
    }
}