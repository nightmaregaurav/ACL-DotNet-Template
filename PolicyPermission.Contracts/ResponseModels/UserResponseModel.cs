namespace PolicyPermission.Contracts.ResponseModels
{
    public class UserResponseModel
    {
        public Guid Guid { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}