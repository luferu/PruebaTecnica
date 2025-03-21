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
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using PruebaTecnica.DAL.Models;
using PruebaTecnica.Utility;


namespace PruebaTecnica.BLL.Classes
{
    public class RandomUserService : IRandomUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RandomUserService> _logger;
        private readonly Webhook _webhook;
        private readonly ILogApiService _logApiService;

        public RandomUserService(HttpClient httpClient, ILogger<RandomUserService> logger, IHttpClientFactory httpClientFactory, IOptions<Webhook> webhook, ILogApiService logApiService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _webhook = webhook.Value;
            _logApiService = logApiService;
        }
        public async Task<bool> GetRandomUserAndSendWebhookAsync()
        {
            var randomUser = await GetRandomUserAsync();
            return await SendWebhookAsync(randomUser);
        }

        public async Task<RandomUserDTO> GetRandomUserAsync()
        {
            _logger.LogInformation("get randomuser...");

            
            var url = "https://randomuser.me/api/";
            var response = await _httpClient.GetAsync(url);
            await SaveApiLogAsync("GET", url, response, null);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("json response: {json}", json);

            ////var jsonDoc = JsonDocument.Parse(json);
            ////var user = jsonDoc.RootElement.GetProperty("results")[0];

            var apiResponse = JsonConvert.DeserializeObject<RandomUserApiResponseDTO>(json);


            var user = apiResponse?.Results.FirstOrDefault();



            // Mapeo from result to dto
            var payload = new RandomUserDTO
            {
                User = new UserDTO
                {
                    Title = user.Name?.Title ?? string.Empty,
                    First = user.Name?.First ?? string.Empty,
                    Last = user.Name?.Last ?? string.Empty
                },
                Login = new LoginDTO
                {
                    Uuid = user.Login.Uuid,
                    Username = user.Login.Username,
                    Password = user.Login.Password,
                    Salt = user.Login.Salt,
                    Md5 = user.Login.Md5,
                    Sha1 = user.Login.Sha1,
                    Sha256 = user.Login.Sha256
                }
            };

            payload.User.Title = payload.User.Title + "1";
            _logger.LogInformation("Mapped payload: {payload}", payload);
            await SaveApiLogAsync("GET", url, response, payload);

            return payload;
        }



        public async Task<bool> SendWebhookAsync(RandomUserDTO payload)
        {
            _logger.LogInformation("Sending to webhook: {payload}", payload);


            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            _logger.LogInformation("payload json: {jsonPayload}", jsonPayload);


            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Remove("Surtechnology");
            _httpClient.DefaultRequestHeaders.Add("Surtechnology", _webhook.HeaderSurtechnology);

            var response = await _httpClient.PostAsync(_webhook.Url, content);
            await SaveApiLogAsync("POST", _webhook.Url, response, payload);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Webhook responseStatusCode: {StatusCode}", response.StatusCode);

            return true;
        }

        private async Task SaveApiLogAsync(string httpMethod, string url, HttpResponseMessage response, object? requestBody = null)
        {
            var requestAsString = requestBody != null ? JsonConvert.SerializeObject(requestBody, Formatting.Indented) : string.Empty;
            var responseBodyAsString = response != null ? JsonConvert.SerializeObject(response, Formatting.Indented) : string.Empty;

            var apiLog = new ApiLog
            {
                HttpMethod = httpMethod,
                Url = url,
                StatusCode = response?.StatusCode != null ? (int)response.StatusCode : 0,
                RequestHeaders = _httpClient.DefaultRequestHeaders.ToString(),
                RequestBody = requestAsString,
                ResponseBody = responseBodyAsString
            };

            await _logApiService.CreateAsync(apiLog);
        }

    }
}
