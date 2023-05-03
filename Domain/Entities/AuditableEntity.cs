namespace Domain.Entities
{
    public class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? Updated { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? Deleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? Archived { get; set; }
    }
}
