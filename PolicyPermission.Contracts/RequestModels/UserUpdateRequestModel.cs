namespace PolicyPermission.Contracts.RequestModels
{
    public class UserUpdateRequestModel
    {
        public Guid Guid { get; set; }
        public string FullName { get; set; }
        public Guid Role { get; set; }
    }
}