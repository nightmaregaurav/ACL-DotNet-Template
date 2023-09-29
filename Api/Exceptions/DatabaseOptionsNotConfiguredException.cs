using Api.Exceptions.Base;

namespace Api.Exceptions
{
    internal class DatabaseOptionsNotConfiguredException : BaseException
    {
        public DatabaseOptionsNotConfiguredException(string message = "Database options are not configured!") : base(message, 500)
        {
        }
    }
}
