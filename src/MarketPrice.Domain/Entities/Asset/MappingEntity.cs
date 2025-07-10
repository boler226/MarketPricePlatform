namespace MarketPrice.Domain.Entities.Asset
{
    public class MappingEntity
    {
        public Guid Id { get; set; }

        public string Provider { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string Exchange { get; set; } = null!;
        public int DefaultOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }

        public TimeSpan RegularStart { get; set; }
        public TimeSpan RegularEnd { get; set; }
        public TimeSpan ElectronicStart { get; set; }
        public TimeSpan ElectronicEnd { get; set; }

        public Guid AssetId { get; set; }
        public AssetEntity Asset { get; set; } = null!;
    }
}
