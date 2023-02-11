using PolicyPermission.Exceptions.Base;

namespace PolicyPermission.Exceptions
{
    internal class UserNotAuthenticatedException : BaseException
    {
        public UserNotAuthenticatedException(string message = "User is not authenticated!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}