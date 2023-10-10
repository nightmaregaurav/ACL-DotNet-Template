using Api.Exceptions.Base;

namespace Api.Exceptions
{
    internal class JwtOptionsNotConfiguredException(string message = "JWT options are not configured!") : BaseException(message, 500);
}
