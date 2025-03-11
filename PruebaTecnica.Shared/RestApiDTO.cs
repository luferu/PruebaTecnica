using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Shared
{
    public class RestApiDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
