using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using WorkerModels.Model;
using WorkerRepositories.Mappings;

namespace WorkerRepositories.Data;

public class AppDbContext : DbContext, ISecurityKeyContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public DbSet<Phones> Phones { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<User> Users { get; set; }
    public virtual DbSet<KeyMaterial> SecurityKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WorkerMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new PhonesMap());
        modelBuilder.Entity<KeyMaterial>(entity =>
        {
            entity.ToTable("securitykeys");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate).HasColumnName("creationdate");
            entity.Property(e => e.ExpiredAt).HasColumnName("expiredat");
            entity.Property(e => e.RevokedReason).HasColumnName("revokedreason");
            entity.Property(e => e.IsRevoked).HasColumnName("isrevoked");
            entity.Property(e => e.KeyId).HasColumnName("keyid");
            entity.Property(e => e.Parameters).HasColumnName("parameters");
            entity.Property(e => e.Type).HasColumnName("type");
        });
    }
}
