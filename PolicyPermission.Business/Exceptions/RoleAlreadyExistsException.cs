using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class RoleAlreadyExistsException : BaseException
    {
        public RoleAlreadyExistsException(Exception? innerException = null) : base("Role Already Exists!", innerException)
        {
        }
    }
}