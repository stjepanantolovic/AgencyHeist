using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    //[Index(nameof(Skill.Name), IsUnique = true)]
    public class Skill
    {
        //public Skill()
        //{
        //    this.Members = new HashSet<Member>();
        //    this.SkillLevels = new HashSet<SkillLevel>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [RegularExpression(@"([*])")]
        [MaxLength(10)]
        public string Level { get; set; }
        public ICollection<Member> Members { get; set; }
        //public ICollection<SkillLevel> SkillLevels { get; set; }
    }
}
