using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerModels.Model;

namespace WorkerRepositories.Mappings
{
    public class WorkerMap : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> entity)
        {
            entity.ToTable("workers");
            entity.HasKey(e => e.Id).HasName("workers_pkey");
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");

            entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(50)
                  .HasColumnName("firstname");
            entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(50)
                  .HasColumnName("lastname");
            entity.Property(e => e.CorporateEmail)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("corporateemail");
            entity.HasIndex(e => e.CorporateEmail)
                  .IsUnique();
            entity.Property(e => e.LeaderName)
                  .HasMaxLength(20)
                  .HasColumnName("leadername");
            entity.HasIndex(e => e.LeaderName);                  
            entity.Property(e => e.WorkerNumber)
                  .IsRequired()
                  .HasMaxLength(20)
                  .HasColumnName("workernumber");
            entity.HasIndex(e => e.WorkerNumber)
                  .IsUnique();
            entity.Property(e => e.PasswordHash)
                  .IsRequired()
                  .HasColumnName("passwordhash");
        }
    }
}