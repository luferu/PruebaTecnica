using PruebaTecnica.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BLL.Interfaces
{
    public interface IRestApiService
    {
        Task<IEnumerable<RestApiDTO>> GetAllAsync();
        Task<RestApiDTO> GetByIdAsync(string id);
        Task<RestApiDTO> CreateAsync(RestApiDTO newData);
        Task<RestApiDTO> UpdateAsync(string id, RestApiDTO updatedData);
    }
}
