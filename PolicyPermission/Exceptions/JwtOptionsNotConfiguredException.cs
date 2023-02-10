using PolicyPermission.Exceptions.Base;

namespace PolicyPermission.Exceptions
{
    internal class JwtOptionsNotConfiguredException : BaseException
    {
        public JwtOptionsNotConfiguredException(string message = "JWT options are not configured!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}