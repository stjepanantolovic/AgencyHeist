using System.Text.Json.Serialization;

namespace MoneyHeist2.Entities.DTOs
{
    public class MemberRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("skills")]
        public virtual ICollection<SkillRequest> Skills { get; set; } = new List<SkillRequest>();
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("mainSkill")]
        public string? MainSkill { get; set; }
        [JsonPropertyName("sex")]
        public string? Sex { get; set; }
    }
}
