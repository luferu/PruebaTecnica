using PruebaTecnica.DAL.Models;
using PruebaTecnica.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BLL.Interfaces
{
    public interface ILogApiService
    {
        Task CreateAsync(ApiLog newData);
    }
}
