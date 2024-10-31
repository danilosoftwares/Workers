using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkerRepositories.Mappings
{
    public class SecurityKeys : IEntityTypeConfiguration<SecurityKeys>
    {
        public void Configure(EntityTypeBuilder<SecurityKeys> entity)
        {
            entity.ToTable("securitykeys");
        }
    }
}