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

            await SaveApiLogAsync("GET", "/objects", response, result);


            _logger.LogInformation("Objects from API: {@result}", result);

            return result!;
        }


        public async Task<RestApiDTO> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get by Id from API - ID: {Id}", id);

            var response = await _httpClient.GetAsync($"/objects/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            await SaveApiLogAsync("GET", $"/objects/{id}", response, result);
            _logger.LogInformation("Object from API: {@Result}", result);

            return result!;
        }

        public async Task<RestApiDTO> CreateAsync(RestApiDTO newData)
        {
            _logger.LogInformation("Create new data: {@NewData}", newData);

            var response = await _httpClient.PostAsJsonAsync("/objects", newData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            await SaveApiLogAsync("POST", "/objects", response, result, newData);
            _logger.LogInformation("Object from API (new data): {@Result}", result);

            return result!;
        }

        public async Task<RestApiDTO> UpdateAsync(string id, RestApiDTO updatedData)
        {
            _logger.LogInformation("Update data from API - ID: {Id}: {@UpdatedData}", id, updatedData);

            var response = await _httpClient.PutAsJsonAsync($"/objects/{id}", updatedData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            await SaveApiLogAsync("PUT", $"/objects/{id}", response, result, updatedData);
            _logger.LogInformation("Object from API updated: {@Result}", result);

            return result!;
        }



        private async Task SaveApiLogAsync(string httpMethod, string url, HttpResponseMessage response, object? result, object? requestBody = null)
        {
            var resultAsString = JsonConvert.SerializeObject(result, Formatting.Indented);
            var requestBodyAsString = requestBody != null ? JsonConvert.SerializeObject(requestBody, Formatting.Indented) : string.Empty;

            var apiLog = new ApiLog
            {
                HttpMethod = httpMethod,
                Url = url,
                StatusCode = (int)response.StatusCode,
                RequestHeaders = _httpClient.DefaultRequestHeaders.ToString(),
                RequestBody = requestBodyAsString,
                ResponseBody = resultAsString
            };

            await _logApiService.CreateAsync(apiLog);
        }
    }
}
