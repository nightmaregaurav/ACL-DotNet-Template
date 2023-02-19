namespace ACL.Contracts.ResponseModels
{
    public class RoleResponseModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}