using AutoMapper;
using MarketPrice.Domain.Entities.Asset;
using MarketPrice.Domain.Entities.Bar;
using Microsoft.EntityFrameworkCore;

namespace MarketPrice.Infrastructure.DbContext
{
    public class AssetsDbContext : Microsoft.EntityFrameworkCore.DbContext {
        public AssetsDbContext(DbContextOptions<AssetsDbContext> options) 
            : base(options) { }

        public DbSet<AssetEntity> Assets => Set<AssetEntity>();
        public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
        public DbSet<MappingEntity> Mappings => Set<MappingEntity>();
        public DbSet<BarEntity> Bars => Set<BarEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssetEntity>(entity => {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Symbol).IsRequired();
                entity.Property(a => a.Kind).IsRequired();
                entity.Property(a => a.Description).IsRequired();
                entity.Property(a => a.Currency).IsRequired();
                entity.Property(a => a.BaseCurrency).IsRequired();

                entity.HasOne(a => a.Profile)
                      .WithOne(p => p.Asset)
                      .HasForeignKey<ProfileEntity>(p => p.AssetId);
            });

            modelBuilder.Entity<ProfileEntity>(entity => {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<MappingEntity>(entity => {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Provider).IsRequired();
                entity.Property(m => m.Symbol).IsRequired();
                entity.Property(m => m.Exchange).IsRequired();
                entity.Property(m => m.DefaultOrderSize).IsRequired();
                entity.Property(m => m.RegularStart).IsRequired();
                entity.Property(m => m.RegularEnd).IsRequired();
                entity.Property(m => m.ElectronicStart).IsRequired();
                entity.Property(m => m.ElectronicEnd).IsRequired();

                entity.HasOne(m => m.Asset)
                      .WithMany(a => a.Mappings)
                      .HasForeignKey(m => m.AssetId);
            });

            modelBuilder.Entity<BarEntity>(entity => {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Time).IsRequired();
                entity.Property(b => b.Open).IsRequired();
                entity.Property(b => b.High).IsRequired();
                entity.Property(b => b.Low).IsRequired();
                entity.Property(b => b.Close).IsRequired();
                entity.Property(b => b.Volume).IsRequired();
            });
        }
    }
}
