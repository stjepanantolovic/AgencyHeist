using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    public class SkillLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public Level? Level { get; set; }
        public Skill? Skill { get; set; }
        [Required]
        public Guid LevelID { get; set; }
        [Required]
        public Guid SkillID { get; set; }
        public virtual ICollection<Member> Members { get; set; }

    }
}
