namespace Data.Exceptions.Base
{
    internal abstract class BaseException : Exception
    {
        public int? ReferenceStatusCode { get; private set; }

        public BaseException(string message, int referenceStatusCode, Exception innerException) : base(message, innerException) => ReferenceStatusCode = referenceStatusCode;
        public BaseException(string message, int referenceStatusCode) : base(message) => ReferenceStatusCode = referenceStatusCode;
        public BaseException(string message, Exception innerException) : base(message, innerException) => ReferenceStatusCode = 500;
        public BaseException(string message) : base(message) => ReferenceStatusCode = 500;
    }
}
