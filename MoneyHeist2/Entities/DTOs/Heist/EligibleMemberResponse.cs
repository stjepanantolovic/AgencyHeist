using System.Text.Json.Serialization;

namespace MoneyHeist2.Entities.DTOs.Heist
{
    public class EligibleMembersResponse
    {
        [JsonPropertyName("skills")]
        public List<HeistSkillResponse> Skills { get; set; }
    }
}
