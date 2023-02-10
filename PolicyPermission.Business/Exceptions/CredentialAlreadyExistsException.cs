using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class CredentialAlreadyExistsException : BaseException
    {
        public CredentialAlreadyExistsException(Exception? innerException = null) : base("Credential Already Exists!", innerException)
        {
        }
    }
}