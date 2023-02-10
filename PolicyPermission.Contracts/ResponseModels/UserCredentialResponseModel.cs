namespace PolicyPermission.Contracts.ResponseModels
{
    public class UserCredentialResponseModel
    {
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Guid User { get; set; }
    }
}