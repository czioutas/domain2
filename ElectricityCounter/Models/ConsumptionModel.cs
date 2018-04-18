using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElectricityCounter.Models
{
    public class ConsumptionModel : BaseModel
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("counter_id")]
        [FromQuery(Name = "counter_id")]
        [FromForm(Name = "counter_id")]
        public int? CounterId { get; set; }

        public CounterModel Counter { get; set; }
    }
}