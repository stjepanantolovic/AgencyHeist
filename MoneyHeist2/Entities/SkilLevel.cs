using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MoneyHeist2.Entities
{
    [Index(nameof(SkillLevel.Value), IsUnique = true)]
    public class SkillLevel
    {
        public SkillLevel()
        {            
            this.Skills = new HashSet<Skill>();
        }
        public Guid ID { get; set; }
        [Required]
        public string? Value { get; set; }
        public ICollection<Skill> Skills { get; set; }  
    }
}
