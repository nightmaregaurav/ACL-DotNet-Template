using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class RoleAlreadyExistsException : BaseException
    {
        public RoleAlreadyExistsException(string message = "Role Already Exists!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}