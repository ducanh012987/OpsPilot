using OpsPilot.OpsPilot.Domain.Entities;
using OpsPilot.OpsPilot.Domain.Entities.Projects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.Domain.Entities.Identity
{
    [Table("USER_ROLE")]
    public class UserRole : BaseEntity
    {
        [Column("USER_ID")]
        public Guid UserId { get; set; }

        [Column("ROLE_ID")]
        public Guid RoleId { get; set; }

        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
