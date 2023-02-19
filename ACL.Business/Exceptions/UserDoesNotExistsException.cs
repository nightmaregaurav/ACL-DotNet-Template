using ACL.Business.Exceptions.Base;

namespace ACL.Business.Exceptions
{
    internal class UserDoesNotExistsException : BaseException
    {
        public UserDoesNotExistsException(string message = "User does not exist!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}