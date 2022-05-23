using System.Text.Json.Serialization;

namespace MoneyHeist2.Entities.DTOs.Heist
{
    public class UpdateHeistSkillRequest
    {
        [JsonPropertyName("skills")]
        public List<HeistSkillRequest> Skills { get; set; }
    }

    public class UpdateHeistSkillResponse
    {
        [JsonPropertyName("skills")]
        public List<HeistSkillResponse> Skills { get; set; }
    }
}
