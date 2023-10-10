using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class InvalidPermissionException(IEnumerable<string> invalidPermissions, string message = "Selected permission does not exists!") : BaseException(message + $"Invalid Permissions: {string.Join(',', invalidPermissions.ToArray())}", 500);
}
