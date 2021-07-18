using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlightsSystem.DBGenerator.Services
{
    public class AirlineService
    {
        public async Task<IEnumerable<string>> GetAirlineNames()
        {
            const string jsonPath = "LocalData/airline_names.json";
            return await Task.Run(() => 
                               JToken.ReadFrom(new JsonTextReader(File.OpenText(jsonPath)))
                                     .Select(airline => airline.ToString()));
        }

    }
}