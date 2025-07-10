using MarketPrice.Domain.Entities.Asset;
using MarketPrice.Domain.Interfaces;
using MarketPrice.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace MarketPrice.Infrastructure.Repositories
{
    public class AssetRepository(
        AssetsDbContext context
        ) : IAssetRepository {
        public Task<List<AssetEntity>> GetAllAsync(CancellationToken cancellationToken) => 
            context.Assets
                .Include(a => a.Profile)
                .Include(a => a.Mappings)
                .ToListAsync(cancellationToken);

        public async Task AddRangeAsync(IEnumerable<AssetEntity> assets, CancellationToken cancellationToken) =>
            await context.Assets.AddRangeAsync(assets, cancellationToken);

        public async Task AddOrUpdateRangeAsync(IEnumerable<AssetEntity> assets, CancellationToken cancellationToken) {
            foreach (var asset in assets) {
                var existing = await context.Assets
                    .Include(a => a.Mappings)
                    .Include(b => b.Profile)
                    .FirstOrDefaultAsync(a => a.Id == asset.Id, cancellationToken);

                if (existing is not null) {
                    if (existing.Mappings != null && existing.Mappings.Any()) {
                        context.Mappings.RemoveRange(existing.Mappings);
                    }

                    if (existing.Profile != null) {
                        context.Profiles.Remove(existing.Profile);
                    }

                    context.Assets.Remove(existing);
                }

                await context.Assets.AddAsync(asset, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
        public Task SaveChangesAsync(CancellationToken cancellationToken) =>
            context.SaveChangesAsync(cancellationToken);
    }
}
