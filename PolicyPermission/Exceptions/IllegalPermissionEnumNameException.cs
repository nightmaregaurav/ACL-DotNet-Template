using PolicyPermission.Exceptions.Base;

namespace PolicyPermission.Exceptions
{
    internal class IllegalPermissionEnumNameException : BaseException
    {
        public IllegalPermissionEnumNameException(IEnumerable<string> illegalChars,
            string message = "Permission enum are expected to be in format [Scope__PermissionName]!",
            Exception? innerException = null) : base(message + $"Found: {string.Join(",", illegalChars.ToArray())}", innerException)
        {
        }
    }
}