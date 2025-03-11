using PruebaTecnica.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BLL.Interfaces
{
    public interface IRandomUserService
    {
        Task<bool> GetRandomUserAndSendWebhookAsync();
        Task<RandomUserDTO> GetRandomUserAsync();
        Task<bool> SendWebhookAsync(RandomUserDTO payload);
    }
}
