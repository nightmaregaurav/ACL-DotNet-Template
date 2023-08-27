namespace Business.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        internal BaseException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}