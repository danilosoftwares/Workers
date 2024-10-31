using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerModels.Model;

namespace WorkerRepositories.Mappings
{
    public class PhonesMap : IEntityTypeConfiguration<Phones>
    {
        public void Configure(EntityTypeBuilder<Phones> entity)
        {
            entity.ToTable("phones");
            entity.HasKey(e => e.Id).HasName("phones_pkey");
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd()
                  .UseIdentityAlwaysColumn()
                  .HasColumnName("id");

            entity.Property(e => e.IdWorker)
                  .IsRequired()
                  .HasColumnName("id_worker");
            entity.Property(e => e.PhoneNumber)
                  .IsRequired()
                  .HasColumnName("phonenumber");
            
        }
    }
}