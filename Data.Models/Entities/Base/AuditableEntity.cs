namespace Data.Models.Entities.Base
{
    public abstract class AuditableEntity : EntityBase
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        public virtual User? CreatedBy { get; private set; }
        public virtual User? LastUpdatedBy { get; private set; }

        public void SaveCreationTime(DateTime createdAt) => CreatedAt = createdAt;
        public void SaveUpdateTime(DateTime updatedAt) => LastUpdatedAt = updatedAt;
        public void SetAuditUserDetails(User? lastUpdatedBy, User? createdBy = null)
        {
            if (createdBy != null) CreatedBy = createdBy;
            LastUpdatedBy = lastUpdatedBy;
        }
    }
}
