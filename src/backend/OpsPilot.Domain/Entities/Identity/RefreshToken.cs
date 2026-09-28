using OpsPilot.OpsPilot.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.Domain.Entities.Identity
{
    [Table("REFRESH_TOKEN")]
    public class RefreshToken : BaseEntity
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("USER_ID")]
        public Guid UserId { get; set; }

        [Column("TOKEN")]
        [StringLength(500)]
        [Required]
        public string Token { get; set; } = string.Empty;
        
        [Column("EXPIRES_AT")]
        public DateTime ExpiresAt { get; set; }

        [Column("REVOKED_AT")]
        public DateTime? RevokedAt { get; set; }

        [Column("IS_REVOKED")]
        public bool IsRevoked => RevokedAt.HasValue;

        [Column("IS_EXPIRED")]
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

        public User User { get; set; } = null!;
    }
}
