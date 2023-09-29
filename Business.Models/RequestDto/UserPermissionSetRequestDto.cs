namespace Business.Models.RequestDto
{
    public record UserPermissionSetRequestDto
    {
        public string Guid { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
