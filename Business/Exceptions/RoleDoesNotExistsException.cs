using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class RoleDoesNotExistsException : BaseException
    {
        public RoleDoesNotExistsException(string message = "Role does not exists!") : base(message, 500)
        {
        }
    }
}
