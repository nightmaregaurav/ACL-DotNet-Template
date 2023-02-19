using ACL.Business.Exceptions.Base;

namespace ACL.Business.Exceptions
{
    internal class CredentialAlreadyExistsException : BaseException
    {
        public CredentialAlreadyExistsException(string message = "Credential Already Exists!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}