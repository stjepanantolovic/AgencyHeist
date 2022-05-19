using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyHeist2.Entities
{
    [Index(nameof(Sex.Name), IsUnique = true)]
    public class Sex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string? Name { get; set; }
    }
}
