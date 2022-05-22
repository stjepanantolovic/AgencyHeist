using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    public class HeistSkillLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public Guid? SkillLevelID { get; set; }
        //public Heist? Heist { get; set; }
        //public Guid? HeistID { get; set; }
        public int? Members { get; set; }
    }
}
