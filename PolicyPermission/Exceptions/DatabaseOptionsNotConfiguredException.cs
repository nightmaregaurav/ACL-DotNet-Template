using PolicyPermission.Exceptions.Base;

namespace PolicyPermission.Exceptions
{
    internal class DatabaseOptionsNotConfiguredException : BaseException
    {
        public DatabaseOptionsNotConfiguredException(string message = "Database options are not configured!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}