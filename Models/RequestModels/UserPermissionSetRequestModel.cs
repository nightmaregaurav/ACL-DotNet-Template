namespace Models.RequestModels
{
    public record UserPermissionSetRequestModel
    {
        public string Guid { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
