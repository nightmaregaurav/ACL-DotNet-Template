namespace ACL.Contracts.RequestModels
{
    public class UserCredentialAddRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid User { get; set; }
    }
}