namespace MarketPrice.Application.DTOs.Asset {
    public class TradingHoursDto {
        public TimeSpan RegularStart { get; set; }
        public TimeSpan RegularEnd { get; set; }
        public TimeSpan ElectronicStart { get; set; }
        public TimeSpan ElectronicEnd { get; set; }
    }
}
