using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MoneyHeist2.Entities
{
    [Index(nameof(Sex.Name), IsUnique = true)]
    public class Sex
    {
        [Key]
        public Guid ID { get; set; }
        public string? Name { get; set; }
    }
}
