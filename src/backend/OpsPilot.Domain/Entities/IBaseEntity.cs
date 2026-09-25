namespace OpsPilot.OpsPilot.Domain.Entities
{
    public interface IBaseEntity
    {
        bool? IsActive { get; set; }
        string? CreateBy { get; set; }
        string? UpdateBy { get; set; }
        DateTime? CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
