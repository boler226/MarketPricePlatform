using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarketPrice.Domain.Entities.Asset
{
    public class AssetEntity
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Kind { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal TickSize { get; set; }
        public string Currency { get; set; } = null!;
        public string BaseCurrency { get; set; } = null!;
        public ProfileEntity Profile { get; set; } = null!;
        public ICollection<MappingEntity> Mappings { get; set; } = new List<MappingEntity>();
    }
}
