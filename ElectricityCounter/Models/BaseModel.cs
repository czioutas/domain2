using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElectricityCounter.Models
{
    public class BaseModel
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [JsonProperty("deleted")]
        public bool Deleted { get; set; } = false;
    }
}