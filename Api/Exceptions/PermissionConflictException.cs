using Api.Exceptions.Base;

namespace Api.Exceptions
{
    internal class PermissionConflictException(IEnumerable<string> permissions) : BaseException($"Some dependencies require these permissions while other repels them! Permissions: {string.Join(", ", permissions)}", 500);
}
