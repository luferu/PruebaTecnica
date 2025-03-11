using Microsoft.Extensions.Logging;
using PruebaTecnica.BLL.Interfaces;
using PruebaTecnica.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;


namespace PruebaTecnica.BLL.Classes
{
    public class RandomUserService : IRandomUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RandomUserService> _logger;

        public RandomUserService(HttpClient httpClient, ILogger<RandomUserService> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> GetRandomUserAndSendWebhookAsync()
        {
            var randomUser = await GetRandomUserAsync();
            return await SendWebhookAsync(randomUser);
        }

        public async Task<RandomUserDTO> GetRandomUserAsync()
        {
            _logger.LogInformation("get randomuser...");

            var response = await _httpClient.GetAsync("api");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("json response: {Json}", json);

            var jsonDoc = JsonDocument.Parse(json);
            var user = jsonDoc.RootElement.GetProperty("results")[0];

            // Mapeo from result to dto
            var payload = new RandomUserDTO
            {
                User = new UserDTO
                {
                    Title = user.GetProperty("name").GetProperty("title").GetString(),
                    First = user.GetProperty("name").GetProperty("first").GetString(),
                    Last = user.GetProperty("name").GetProperty("last").GetString()
                },
                Login = new LoginDTO
                {
                    Uuid = user.GetProperty("login").GetProperty("uuid").GetString(),
                    Username = user.GetProperty("login").GetProperty("username").GetString(),
                    Password = user.GetProperty("login").GetProperty("password").GetString(),
                    Salt = user.GetProperty("login").GetProperty("salt").GetString(),
                    Md5 = user.GetProperty("login").GetProperty("md5").GetString(),
                    Sha1 = user.GetProperty("login").GetProperty("sha1").GetString(),
                    Sha256 = user.GetProperty("login").GetProperty("sha256").GetString()
                }
            };

            payload.User.Title = payload.User.Title + "1";
            _logger.LogInformation("Mapped payload: {@Payload}", payload);

            return payload;
        }

        public async Task <bool> SendWebhookAsync(RandomUserDTO payload)
        {
            _logger.LogInformation("Sending to webhook: {@Payload}", payload);

            var webhookClient = _httpClientFactory.CreateClient("WebhookClient");

            var response = await webhookClient.PostAsJsonAsync(webhookClient.BaseAddress,payload);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Webhook responseStatusCode  : {StatusCode}", response.StatusCode);

            return true;
        }
    }
}
