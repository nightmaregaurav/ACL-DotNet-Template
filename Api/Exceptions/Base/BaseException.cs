namespace Api.Exceptions.Base
{
    internal abstract class BaseException : Exception
    {
        public BaseException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}