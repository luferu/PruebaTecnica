using PruebaTecnica.BLL.Interfaces;
using PruebaTecnica.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;


namespace PruebaTecnica.BLL.Classes
{
    public class RestApiService : IRestApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RestApiService> _logger;

        public RestApiService(HttpClient httpClient, ILogger<RestApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IEnumerable<RestApiDTO>> GetAllAsync()
        {
            _logger.LogInformation("Get all from API");

            var response = await _httpClient.GetAsync("/objects");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<RestApiDTO>>();
            _logger.LogInformation("Objects from API: {@Result}", result);

            return result!;
        }

        public async Task<RestApiDTO> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get by Id from API - ID: {Id}", id);

            var response = await _httpClient.GetAsync($"/objects/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            _logger.LogInformation("Object from API: {@Result}", result);

            return result!;
        }

        public async Task<RestApiDTO> CreateAsync(RestApiDTO newData)
        {
            _logger.LogInformation("Create new data: {@NewData}", newData);

            var response = await _httpClient.PostAsJsonAsync("/objects", newData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            _logger.LogInformation("Object from API (new data): {@Result}", result);

            return result!;
        }

        public async Task<RestApiDTO> UpdateAsync(string id, RestApiDTO updatedData)
        {
            _logger.LogInformation("Update data from API - ID: {Id}: {@UpdatedData}", id, updatedData);

            var response = await _httpClient.PutAsJsonAsync($"/objects/{id}", updatedData);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<RestApiDTO>();
            _logger.LogInformation("Object from API updated: {@Result}", result);

            return result!;
        }
    }
}
