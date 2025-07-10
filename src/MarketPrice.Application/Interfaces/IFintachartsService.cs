using MarketPrice.Application.DTOs.Asset;
using MarketPrice.Application.DTOs.Bar;

namespace MarketPrice.Infrastructure.Services.Interfaces
{
    public interface IFintachartsService {
        Task<List<AssetDto>> GetSupportedAssetsAsync();
        Task<List<BarDto>> GetHistoricalPricesAsync(string instrumentId, DateTime startDate, string provider = "oanda");
    }
}
