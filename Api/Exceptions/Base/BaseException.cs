namespace Api.Exceptions.Base
{
    internal abstract class BaseException : Exception
    {
        public int? ReferenceStatusCode { get; private set; }

        protected BaseException(string message, int referenceStatusCode, Exception innerException) : base(message, innerException)
        {
            ReferenceStatusCode = referenceStatusCode;
        }

        protected BaseException(string message, int referenceStatusCode) : base(message)
        {
            ReferenceStatusCode = referenceStatusCode;
        }

        protected BaseException(string message, Exception innerException) : base(message, innerException)
        {
            ReferenceStatusCode = 500;
        }

        protected BaseException(string message) : base(message)
        {
            ReferenceStatusCode = 500;
        }
    }
}
