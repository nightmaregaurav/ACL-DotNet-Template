namespace ACL.Contracts.RequestModels
{
    public class UserPermissionSetRequestModel
    {
        public Guid Guid { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}