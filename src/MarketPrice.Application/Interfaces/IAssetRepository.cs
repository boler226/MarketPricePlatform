using MarketPrice.Domain.Entities.Asset;

namespace MarketPrice.Domain.Interfaces
{
    public interface IAssetRepository {
        Task<List<AssetEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<AssetEntity> assets, CancellationToken cancellationToken);
        Task AddOrUpdateRangeAsync(IEnumerable<AssetEntity> assets, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
