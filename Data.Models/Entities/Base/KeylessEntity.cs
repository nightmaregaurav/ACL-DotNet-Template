namespace Data.Models.Entities.Base
{
    public abstract class KeylessEntity : IEntity
    {
        public bool IsDeleted { get; private set; }
        public void MarkAsDeleted() => IsDeleted = true;
    }
}
