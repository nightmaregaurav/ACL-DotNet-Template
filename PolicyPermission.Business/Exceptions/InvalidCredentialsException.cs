using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class InvalidCredentialsException : BaseException
    {
        public InvalidCredentialsException(Exception? innerException = null) : base("Credential Provided Are Invalid!", innerException)
        {
        }
    }
}