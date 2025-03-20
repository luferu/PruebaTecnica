using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Shared
{
    public class LocationDTO
    {
        [JsonProperty("street")]
        public StreetDTO Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("postcode")]
        public object Postcode { get; set; }

        [JsonProperty("coordinates")]
        public CoordinatesDTO Coordinates { get; set; }

        [JsonProperty("timezone")]
        public TimezoneDTO Timezone { get; set; }
    }
}
