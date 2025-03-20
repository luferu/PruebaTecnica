using PruebaTecnica.BLL.Interfaces;
using PruebaTecnica.DAL.Models;
using PruebaTecnica.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BLL.Classes
{
    public class LogApiService : ILogApiService
    {
        private readonly IApiLogRepository _apiLogRepository;

        public LogApiService(IApiLogRepository apiLogRepository)
        {
            _apiLogRepository = apiLogRepository;
        }

        public async Task CreateAsync(ApiLog newData)
        {
            if (newData == null)
                throw new ArgumentNullException(nameof(newData));

            newData.Timestamp = DateTime.UtcNow;

            await _apiLogRepository.AddAsync(newData);
        }
    }
}
