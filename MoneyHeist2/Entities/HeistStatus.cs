using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    [Index(nameof(MemberStatus.Name), IsUnique = true)]
    public class HeistStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string? Name { get; set; }
    }
    
}
