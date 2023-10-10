using Api.Exceptions.Base;

namespace Api.Exceptions
{
    internal class DatabaseOptionsNotConfiguredException(string message = "Database options are not configured!") : BaseException(message, 500);
}
