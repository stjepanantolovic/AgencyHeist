using System.Text.Json.Serialization;

namespace MoneyHeist2.Entities.DTOs
{
    public class UpdateMemberSkillsRequest
    {
        [JsonPropertyName("skills")]
        public List<SkillRequest> Skills { get; set; }
        [JsonPropertyName("mainSkill")]
        public string? MainSkill { get; set; }
    }

    public class MemberSkillsResponse
    {
        [JsonPropertyName("skills")]
        public List<SkillResponse> Skills { get; set; }
        [JsonPropertyName("mainSkill")]
        public string? MainSkill { get; set; }
    }
}
