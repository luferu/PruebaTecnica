using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Shared
{
    public class RegisteredDTO
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
