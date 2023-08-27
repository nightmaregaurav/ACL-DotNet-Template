using Api.Exceptions.Base;

namespace Api.Exceptions
{
    internal class JwtOptionsNotConfiguredException : BaseException
    {
        public JwtOptionsNotConfiguredException(string message = "JWT options are not configured!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}