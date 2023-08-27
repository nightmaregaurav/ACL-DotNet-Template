using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class InvalidPermissionException : BaseException
    {
        public InvalidPermissionException(IEnumerable<string> invalidPermissions, string message = "Selected permission does not exists!", Exception? innerException = null) : base(message + $"Invalid Permissions: {string.Join(',', invalidPermissions.ToArray())}", innerException)
        {
        }
    }
}