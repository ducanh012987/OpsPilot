namespace OpsPilot.OpsPilot.Domain.Entities
{
    public interface ISoftDeleteEntity
    {
        public bool? IsDeleted { get; set; }
        public string? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
