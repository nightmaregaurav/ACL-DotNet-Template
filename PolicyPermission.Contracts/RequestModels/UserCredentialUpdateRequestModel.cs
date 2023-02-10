namespace PolicyPermission.Contracts.RequestModels
{
    public class UserCredentialUpdateRequestModel
    {
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}