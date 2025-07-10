namespace MarketPrice.Application.Interfaces {
    public interface ITokenService {
        Task<string> GetAccessTokenAsync();
    }
}
