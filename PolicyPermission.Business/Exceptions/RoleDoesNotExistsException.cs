using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class RoleDoesNotExistsException : BaseException
    {
        public RoleDoesNotExistsException(Exception? innerException = null) : base("Role does not exists!", innerException)
        {
        }
    }
}