using Api.Exceptions.Base;

namespace Api.Exceptions
{
    internal class JwtOptionsNotConfiguredException : BaseException
    {
        public JwtOptionsNotConfiguredException(string message = "JWT options are not configured!") : base(message, 500)
        {
        }
    }
}
