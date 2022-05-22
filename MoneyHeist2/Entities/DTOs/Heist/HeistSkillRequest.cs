using System.Text.Json.Serialization;

namespace MoneyHeist2.Entities.DTOs.Heist
{
    public class HeistSkillRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; }
        [JsonPropertyName("members")]
        public int? Members { get; set; }
    }
}
