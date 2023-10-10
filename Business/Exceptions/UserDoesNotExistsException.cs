using Business.Exceptions.Base;

namespace Business.Exceptions
{
    internal class UserDoesNotExistsException(string message = "User does not exist!") : BaseException(message, 500);
}
