namespace Models.ResponseModels
{
    public record UserPermissionResponseModel
    {
        public ICollection<string> DirectPermissions { get; set; }
        public ICollection<InheritedPermissionModel> InheritedPermissions { get; set; }
    }

    public record InheritedPermissionModel
    {
        public ICollection<string> Permissions { get; set; }
        public string InheritedFromRoleGuid { get; set; }
    }
}
