namespace Business.Contracts.RequestModels
{
    public class UserAddRequestModel
    {
        public string FullName { get; set; }
        public Guid Role { get; set; }
    }
}