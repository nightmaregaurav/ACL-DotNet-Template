using ACL.Business.Exceptions.Base;

namespace ACL.Business.Exceptions
{
    internal class InvalidCredentialsException : BaseException
    {
        public InvalidCredentialsException(string message = "Credential Provided Are Invalid!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}