using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    [Index(nameof(Member.Email), IsUnique = true)]
    public class Member
    {        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public virtual ICollection<SkillLevel> SkillLevels { get; set; }
        public virtual MemberStatus? Status { get; set; }
        public virtual Skill? MainSkill { get; set; }
        public virtual Sex? Sex { get; set; }
        public Guid? MainSkillID { get; set; }
        public Guid? SexID { get; set; }
        public Guid? StatusID { get; set; }
    }
}
