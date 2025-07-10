using MarketPrice.Domain.Entities.Asset;

namespace MarketPrice.Domain.Entities.Bar
{
    public class BarEntity
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
    }
}
