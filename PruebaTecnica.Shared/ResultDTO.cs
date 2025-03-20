using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Shared
{
    public class ResultDTO
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("name")]
        public NameDTO Name { get; set; }

        [JsonProperty("location")]
        public LocationDTO Location { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("login")]
        public LoginDTO Login { get; set; }

        [JsonProperty("dob")]
        public DateOfBirthDTO Dob { get; set; }

        [JsonProperty("registered")]
        public RegisteredDTO Registered { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("cell")]
        public string Cell { get; set; }

        [JsonProperty("id")]
        public IdDTO Id { get; set; }

        [JsonProperty("picture")]
        public PictureDTO Picture { get; set; }

        [JsonProperty("nat")]
        public string Nat { get; set; }
    }
}
