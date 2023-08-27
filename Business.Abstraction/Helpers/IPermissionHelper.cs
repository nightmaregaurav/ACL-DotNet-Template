namespace Business.Abstraction.Helpers
{
    public interface IPermissionHelper
    {
        IEnumerable<string> Permissions { get; }
        IEnumerable<string> ListPermissionsWithDependencies(params string[] modelPermissions);
    }
}
