using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.OpsPilot.Domain.Entities.Projects
{
    [Table("PROJECT")]
    public class Project : BaseEntity
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("NAME")]
        [StringLength(150)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("CODE")]
        [StringLength(50)]
        [Required]
        public string Code { get; set; } = string.Empty;

        [Column("DESCRIPTION")]
        public string? Description { get; set; }
    }
}
