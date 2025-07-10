namespace MarketPrice.Application.DTOs.Asset
{
    public class AssetDto {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Kind { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal TickSize { get; set; }
        public string Currency { get; set; } = null!;
        public string BaseCurrency { get; set; } = null!;
        public ProfileDto Profile { get; set; } = null!;
        public Dictionary<string, MappingDto> Mappings { get; set; } = new();
    }
}
