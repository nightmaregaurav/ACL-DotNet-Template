namespace Data.Models.Entities.Base
{
    public class AuditEntry : EntityBase
    {
        public string? UserGuid { get; private set; }
        public string AuditType { get; private set; }

        public string EntityName { get; private set; }
        public long? EntityPrimaryKey { get; private set; }
        public Dictionary<string, object?>? OldValues { get; private set; }
        public Dictionary<string, object?>? NewValues { get; private set; }

        protected AuditEntry()
        {
        }

        public AuditEntry(string? userGuid, string auditType, string entityName, long? entityPrimaryKey, Dictionary<string, object?>? oldValues, Dictionary<string, object?>? newValues)
        {
            UserGuid = userGuid;
            AuditType = auditType;
            EntityName = entityName;
            EntityPrimaryKey = entityPrimaryKey;

            OldValues = oldValues;
            NewValues = newValues;
        }
    }
}
