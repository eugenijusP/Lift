using System.Runtime.Serialization;

namespace Lift.Domain.Helpers
{
    [System.Serializable]
    public class DomainException : System.Exception, IExceptionFields
    {
        private const bool logValue = true;

        public bool logSentry { get; }

        public DomainException(string message)
            : base(message) => this.logSentry = logValue;

        public DomainException(string message, bool logSentry)
            : base(message) => this.logSentry = logSentry;

        public DomainException(System.Exception innerException)
            : this(string.Empty, innerException) => this.logSentry = logValue;

        public DomainException(string message, System.Exception innerException)
            : base(message, innerException) => this.logSentry = logValue;

        public DomainException()
            : base() => this.logSentry = logValue;

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context) => this.logSentry = logValue;
    }
}
