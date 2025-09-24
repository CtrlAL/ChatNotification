using System.Text;
using System.Text.Json;
using KeycloakAuth;
using Microsoft.Extensions.Options;
using RegistrationService.Constants;

namespace RegistrationService.Services
{
    public class KeycloakManager : IKeyCloakManager
    {
        private readonly KeyCloakSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;

        public KeycloakManager(
            IOptions<KeyCloakSettings> settings,
            IHttpClientFactory httpClientFactory)
        {
            _settings = settings.Value;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<string> GetAdminAccessTokenAsync()
        {
            var client = _httpClientFactory.CreateClient(KeycloakConstants.HttpClientName);

            var tokenRequest = new Dictionary<string, string>
            {
                ["client_id"] = _settings.Audience,
                ["client_secret"] = _settings.Secret,
                ["grant_type"] = "client_credentials"
            };

            var response = await client.PostAsync(
                $"realms/master/protocol/openid-connect/token",
                new FormUrlEncodedContent(tokenRequest));

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("access_token").GetString()
                   ?? throw new InvalidOperationException("Access token is missing");
        }

        public async Task RegisterUserAsync(string email, string username, string password)
        {
            var accessToken = await GetAdminAccessTokenAsync();
            var client = _httpClientFactory.CreateClient(KeycloakConstants.HttpClientName);
            client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);

            var user = new
            {
                email,
                username,
                enabled = true,
                emailVerified = true,
                credentials = new[]
                {
                new { type = "password", value = password, temporary = false }
            }
            };

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                $"admin/realms/{_settings.Realm}/users",
                content);

            response.EnsureSuccessStatusCode();
        }

        public async Task ResetPasswordAsync(string username, string newPassword)
        {
            var accessToken = await GetAdminAccessTokenAsync();
            var client = _httpClientFactory.CreateClient(KeycloakConstants.HttpClientName);
            client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);

            var searchResponse = await client.GetAsync(
                $"admin/realms/{_settings.Realm}/users?username={Uri.EscapeDataString(username)}");
            searchResponse.EnsureSuccessStatusCode();

            var usersJson = await searchResponse.Content.ReadAsStringAsync();
            using var usersDoc = JsonDocument.Parse(usersJson);
            var users = usersDoc.RootElement;

            if (users.GetArrayLength() == 0)
                throw new InvalidOperationException($"User '{username}' not found");

            var userId = users[0].GetProperty("id").GetString();

            var credential = new
            {
                type = "password",
                value = newPassword,
                temporary = false
            };

            var json = JsonSerializer.Serialize(credential);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resetResponse = await client.PutAsync(
                $"admin/realms/{_settings.Realm}/users/{userId}/reset-password",
                content);

            resetResponse.EnsureSuccessStatusCode();
        }
    }
}
