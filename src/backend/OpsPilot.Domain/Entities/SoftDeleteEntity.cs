using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsPilot.OpsPilot.Domain.Entities
{
    public class SoftDeleteEntity : BaseEntity, ISoftDeleteEntity
    {
        [Column("IS_DELETED")]
        public bool? IsDeleted { get; set; }

        [Column("DELETE_BY")]
        [StringLength(50)]
        [Unicode(false)]
        public string? DeleteBy { get; set; }

        [Column("DELETE_DATE")]
        public DateTime? DeleteDate { get; set; }
    }
}
