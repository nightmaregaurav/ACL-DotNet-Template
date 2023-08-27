using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class CredentialAlreadyExistsException : BaseException
    {
        public CredentialAlreadyExistsException(string message = "Credential Already Exists!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}