using MarketPrice.Application.Interfaces;
using MarketPrice.Infrastructure.Services.Interfaces;
using System.Text.Json;
using System.Net.Http.Headers;
using MarketPrice.Application.DTOs.Asset;
using MarketPrice.Application.DTOs.Bar;

namespace MarketPrice.Infrastructure.Services
{
    public class FintachartsService(
        IHttpClientFactory httpClient,
        ITokenService tokenService
        ) : IFintachartsService {
        public async Task<List<AssetDto>> GetSupportedAssetsAsync() {
            var client = httpClient.CreateClient("Fintacharts");
            var accessToken = await tokenService.GetAccessTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var response = await client.GetAsync("/api/instruments/v1/instruments?provider=oanda&kind=forex");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(json);
            var dataElement = jsonDoc.RootElement.GetProperty("data");

            return JsonSerializer.Deserialize<List<AssetDto>>(dataElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<List<BarDto>> GetHistoricalPricesAsync(string instrumentId, DateTime startDate, string provider = "oanda") {
            var client = httpClient.CreateClient("Fintacharts");
            var accessToken = await tokenService.GetAccessTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync($"/api/bars/v1/bars/date-range?instrumentId={instrumentId}&provider={provider}&interval=1&periodicity=minute&startDate={startDate:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<BarDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }
    }
}
