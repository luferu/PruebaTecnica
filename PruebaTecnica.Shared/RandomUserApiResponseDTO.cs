using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Shared
{
    public class RandomUserApiResponseDTO
    {
        [JsonProperty("results")]
        public List<ResultDTO> Results { get; set; }

        [JsonProperty("info")]
        public InfoDTO Info { get; set; }

    }
}
