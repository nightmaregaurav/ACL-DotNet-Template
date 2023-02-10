using PolicyPermission.Business.Exceptions.Base;

namespace PolicyPermission.Business.Exceptions
{
    internal class CredentialDoesNotExistsException : BaseException
    {
        public CredentialDoesNotExistsException(Exception? innerException = null) : base("Credential Does Not Exists!", innerException)
        {
        }
    }
}