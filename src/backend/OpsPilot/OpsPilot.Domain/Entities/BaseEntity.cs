using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.OpsPilot.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Column("IS_ACTIVE")]
        public bool? IsActive { get; set; }

        [Column("CREATE_BY")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CreateBy { get; set; }

        [Column("UPDATE_BY")]
        [StringLength(50)]
        [Unicode(false)]
        public string? UpdateBy { get; set; }

        [Column("CREATE_DATE")]
        public DateTime? CreateDate { get; set; }

        [Column("UPDATE_DATE")]
        public DateTime? UpdateDate { get; set; }
    }
}
