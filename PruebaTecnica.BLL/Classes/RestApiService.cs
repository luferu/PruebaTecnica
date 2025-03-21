using PruebaTecnica.BLL.Interfaces;
using PruebaTecnica.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using PruebaTecnica.DAL.Models;
using Newtonsoft.Json;


namespace PruebaTecnica.BLL.Classes
{
    public class RestApiService : IRestApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RestApiService> _logger;
        private readonly ILogApiService _logApiService;

        public RestApiService(HttpClient httpClient, ILogger<RestApiService> logger, ILogApiService logApiService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _logApiService = logApiService;
        }
        public async Task<IEnumerable<RestApiDTO>> GetAllAsync()
        {
            _logger.LogInformation("Get all from API");

            var response = await _httpClient.GetAsync("/objects");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<RestApiDTO>>();

            await SaveApiLogAsync("GET", "/objects", response, null);


            _logger.LogInformation("Objects from API: {result}", result?.Count());

            return result!;
        }


        public async Task<RestApiDTO> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get by Id from API - ID: {Id}", id);

            var response = await _httpClient.GetAsync($"/objects/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            await SaveApiLogAsync("GET", $"/objects/{id}", response, null);
            _logger.LogInformation("Object from API: {result}", result);

            return result!;
        }

        public async Task<RestApiDTO> CreateAsync(RestApiDTO newData)
        {
            _logger.LogInformation("Create new data: {@NewData}", newData);

            var response = await _httpClient.PostAsJsonAsync("/objects", newData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            await SaveApiLogAsync("POST", "/objects", response, newData);
            _logger.LogInformation("Object from API (new data): {result}", result);

            return result!;
        }

        public async Task<RestApiDTO> UpdateAsync(string id, RestApiDTO updatedData)
        {
            _logger.LogInformation("Update data from API - ID: {Id}: {@UpdatedData}", id, updatedData);

            var response = await _httpClient.PutAsJsonAsync($"/objects/{id}", updatedData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            await SaveApiLogAsync("PUT", $"/objects/{id}", response, updatedData);
            _logger.LogInformation("Object from API updated: {result}", result);

            return result!;
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
