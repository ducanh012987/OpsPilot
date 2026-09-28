using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.Domain.Entities.Identity
{
    [Table("USER")]
    public class User : IdentityUser<Guid>
    {
        [Column("FULL_NAME")]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; } = true;

        [Column("LAST_LOGIN_AT")]
        public DateTime? LastLoginAt { get; set; }

        [Column("CREATE_BY")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CreateBy { get; set; }

        [Column("UPDATE_BY")]
        [StringLength(50)]
        [Unicode(false)]
        public string? UpdateBy { get; set; }

        [Column("CREATE_DATE")]
        public DateTime? CreateDate { get; set; } = DateTime.UtcNow;

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }
    }
}
