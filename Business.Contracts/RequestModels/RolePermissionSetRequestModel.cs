namespace Business.Contracts.RequestModels
{
    public class RolePermissionSetRequestModel
    {
        public Guid Guid { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}