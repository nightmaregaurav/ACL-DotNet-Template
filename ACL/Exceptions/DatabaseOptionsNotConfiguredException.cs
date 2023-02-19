using ACL.Exceptions.Base;

namespace ACL.Exceptions
{
    internal class DatabaseOptionsNotConfiguredException : BaseException
    {
        public DatabaseOptionsNotConfiguredException(string message = "Database options are not configured!", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}