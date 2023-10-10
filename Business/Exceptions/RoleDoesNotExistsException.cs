using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class RoleDoesNotExistsException(string message = "Role does not exists!") : BaseException(message, 500);
}
