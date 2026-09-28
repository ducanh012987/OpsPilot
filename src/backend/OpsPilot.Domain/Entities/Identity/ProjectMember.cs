using OpsPilot.OpsPilot.Domain.Entities;
using OpsPilot.OpsPilot.Domain.Entities.Projects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.Domain.Entities.Identity
{
    [Table("PROJECT_MEMBER")]
    public class ProjectMember : BaseEntity
    {
        [Column("PROJECT_ID")]
        public Guid ProjectId { get; set; }

        [Column("USER_ID")]
        public Guid UserId { get; set; }

        [Column("ROLE")]
        [StringLength(50)]
        [Required]
        public string Role { get; set; } = "Viewer";

        [Column("JOINED_AT")]
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Project Project { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
