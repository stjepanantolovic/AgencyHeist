using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    [Index(nameof(Skill.Name), IsUnique = true)]
    public class Heist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual ICollection<HeistSkillLevel>? HeistSkillLevels { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public Guid? StatusID { get; set; }
        public HeistStatus Status { get; set; }
    }
}
