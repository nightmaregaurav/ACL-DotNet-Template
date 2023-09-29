namespace Models.RequestModels
{
    public record RolePermissionSetRequestModel
    {
        public string Guid { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
