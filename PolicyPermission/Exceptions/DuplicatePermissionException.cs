using PolicyPermission.Exceptions.Base;

namespace PolicyPermission.Exceptions
{
    internal class DuplicatePermissionException : BaseException
    {
        public DuplicatePermissionException(IEnumerable<string> duplicates,
            string message = "Permission enum are expected to have unique [PermissionName]s even if their [Scope] are different!",
            Exception? innerException = null) : base(message + $"Found: {string.Join(",", duplicates.ToArray())}", innerException)
        {
        }
    }
}