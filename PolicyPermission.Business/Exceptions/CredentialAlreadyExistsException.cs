using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class CredentialAlreadyExistsException : BaseException
    {
        public CredentialAlreadyExistsException(string message = "Credential Already Exists!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}