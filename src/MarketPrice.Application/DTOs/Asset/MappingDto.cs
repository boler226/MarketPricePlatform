namespace MarketPrice.Application.DTOs.Asset {
    public class MappingDto {
        public string Symbol { get; set; } = null!;
        public string Exchange { get; set; } = null!;
        public int DefaultOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }
        public TradingHoursDto TradingHours { get; set; } = null!;

    }
}
