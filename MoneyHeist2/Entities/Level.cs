using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    [Index(nameof(Level.Value), IsUnique = true)]
    public class Level
    {
        //public Level()
        //{            
        //    this.Skills = new HashSet<Skill>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [Required]
        public string? Value { get; set; }
        //public ICollection<Skill> Skills { get; set; }  
    }
}
