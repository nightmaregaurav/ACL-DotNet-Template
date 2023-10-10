namespace Data.Models.Entities.Base
{
    public abstract class EntityBase : IEntity
    {
        private long Id { get; set; }
        public string Guid { get; init; } = System.Guid.NewGuid().ToString();
        public bool IsDeleted { get; private set; }
        public void MarkAsDeleted() => IsDeleted = true;
    }
}
