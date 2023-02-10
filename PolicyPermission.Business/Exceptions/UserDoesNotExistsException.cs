using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class UserDoesNotExistsException : BaseException
    {
        public UserDoesNotExistsException(Exception? innerException = null) : base("User does not exist!", innerException)
        {
        }
    }
}