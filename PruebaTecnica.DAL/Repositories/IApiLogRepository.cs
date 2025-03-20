using PruebaTecnica.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DAL.Repositories
{
    public interface IApiLogRepository : IRepository<ApiLog>
    {
        public Task AddAsync(ApiLog entity);
    }
}
