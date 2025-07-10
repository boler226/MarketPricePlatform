using MarketPrice.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MarketPrice.Infrastructure.Services {
    public class TokenService(
        IHttpClientFactory httpClient,
        IConfiguration configuration
        ) : ITokenService {
        private string? accessToken;
        private DateTime tokenExpiration = DateTime.MinValue;

        public async Task<string> GetAccessTokenAsync() {
            if (!string.IsNullOrEmpty(accessToken) && DateTime.UtcNow < tokenExpiration)
                return accessToken;

            var client = httpClient.CreateClient("Fintacharts");

            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", "app-cli" },
                { "username", configuration["Fintacharts:Username"] ?? "" },
                { "password", configuration["Fintacharts:Password"] ?? "" }
            };

            var content = new FormUrlEncodedContent(requestData);

            var tokenUrl = $"{configuration["Fintacharts:Url"]}/identity/realms/{configuration["Fintacharts:Realm"]}/protocol/openid-connect/token";
            var response = await client.PostAsync(tokenUrl, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<TokenResponse>(json, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            if (token is null || string.IsNullOrWhiteSpace(token.AccessToken))
                throw new Exception("Failed to retrieve access token.");

            accessToken = token.AccessToken;
            tokenExpiration = DateTime.UtcNow.AddSeconds(token.ExpiresIn - 60);

            return accessToken;
        }

        private class TokenResponse {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; } = "";

            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }
        }
    }
}
