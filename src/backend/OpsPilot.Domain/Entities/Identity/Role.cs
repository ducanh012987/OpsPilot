using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.Domain.Entities.Identity
{
    [Table("ROLE")]
    public class Role : IdentityRole<Guid>
    {
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
