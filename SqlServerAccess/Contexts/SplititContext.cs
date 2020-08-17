using EFCache.POC.Configurations;
using EFCache.POC.SqlServer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCache.POC.SqlServer.Contexts
{
    public partial class SplititContext : DbContext
    {
        public SplititContext(DbContextOptions<SplititContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Currency> Currencies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configuration = this.GetService<IDatabaseConfiguration>();
            modelBuilder.HasDefaultSchema(configuration.ApplicationDbSchema);
            
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate")
                    .HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate")
                    .HasColumnType("datetime").IsRequired(false);
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasColumnType("nvarchar(max)").IsRequired(false);
                entity.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy").HasColumnType("nvarchar(max)").IsRequired(false);
                entity.Property(e => e.CurrencyCode).HasColumnName("CurrencyCode").HasColumnType("nvarchar(max)");
                entity.Property(e => e.CurrencyName).HasColumnName("CurrencyName").HasColumnType("nvarchar(max)");
                entity.Property(e => e.CurrencySymbol).HasColumnName("CurrencySymbol").HasColumnType("nvarchar(max)");
                entity.Property(e => e.CurrencyIsoNumber).HasColumnName("CurrencyIsoNumber").HasColumnType("nvarchar").HasMaxLength(10);
                entity.ToTable("Currencies");
            });
        }
    }
}
