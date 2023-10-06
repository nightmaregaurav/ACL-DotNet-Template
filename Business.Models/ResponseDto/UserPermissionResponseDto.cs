namespace Business.Models.ResponseDto
{
    public record UserPermissionResponseDto
    {
        public ICollection<string> DirectPermissions { get; set; }
        public ICollection<InheritedPermissionDto> InheritedPermissions { get; set; }
    }

    public record InheritedPermissionDto
    {
        public ICollection<string> Permissions { get; set; }
        public string InheritedFromRoleGuid { get; set; }
    }
}
