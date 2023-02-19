using ACL.Business.Exceptions.Base;

namespace ACL.Business.Exceptions
{
    internal class CredentialDoesNotExistsException : BaseException
    {
        public CredentialDoesNotExistsException(string message = "Credential Does Not Exists!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}