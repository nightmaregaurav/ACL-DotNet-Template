namespace ACL.Contracts.RequestModels
{
    public class RoleUpdateRequestModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}