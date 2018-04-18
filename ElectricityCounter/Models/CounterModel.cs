using Newtonsoft.Json;

namespace ElectricityCounter.Models
{
    public class CounterModel : BaseModel
    {
        [JsonProperty("village_name")]
        public string VillageName { get; set; }
    }
}