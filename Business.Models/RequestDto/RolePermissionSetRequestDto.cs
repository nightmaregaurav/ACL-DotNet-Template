namespace Business.Models.RequestDto
{
    public record RolePermissionSetRequestDto
    {
        public string Guid { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
