using System.Text.Json.Serialization;

namespace MoneyHeist2.Entities.DTOs.Heist
{
    public class HeistRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("startTime")]
        public DateTime? StartTime { get; set; }
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }
        
        [JsonPropertyName("skills")]
        public List<HeistSkillRequest>? Skills { get; set; }
    }
}
