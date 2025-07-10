namespace MarketPrice.Domain.Entities.Asset
{
    public class ProfileEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public Guid AssetId { get; set; }
        public AssetEntity Asset { get; set; } = null!;
    }
}
