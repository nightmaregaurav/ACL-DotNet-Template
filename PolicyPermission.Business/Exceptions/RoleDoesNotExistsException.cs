using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class RoleDoesNotExistsException : BaseException
    {
        public RoleDoesNotExistsException(string message = "Role does not exists!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}