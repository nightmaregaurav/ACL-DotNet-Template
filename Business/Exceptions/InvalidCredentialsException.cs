using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class InvalidCredentialsException : BaseException
    {
        public InvalidCredentialsException(string message = "Credential Provided Are Invalid!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}