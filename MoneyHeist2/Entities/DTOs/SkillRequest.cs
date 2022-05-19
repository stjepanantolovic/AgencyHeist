using System.Text.Json.Serialization;

namespace MoneyHeist2.Entities.DTOs
{
    public class SkillRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("level")]
        public string? Level { get; set; }
    }
}
