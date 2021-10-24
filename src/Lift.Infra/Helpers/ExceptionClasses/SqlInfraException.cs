using Lift.Domain.Helpers;
using System.Runtime.Serialization;

namespace Lift.Infra.Helpers.ExceptionClasses {
    [System.Serializable]
    public class SqlInfraException : System.Exception, IExceptionFields
    {
        private const bool logValue = true;

        public bool logSentry { get; }

        public SqlInfraException(string message)
            : base(message) => this.logSentry = logValue;

        public SqlInfraException(string message, bool logSentry)
            : base(message) => this.logSentry = logSentry;

        public SqlInfraException(System.Exception innerException)
            : this(string.Empty, innerException) => this.logSentry = logValue;

        public SqlInfraException(string message, System.Exception innerException)
            : base(message, innerException) => this.logSentry = logValue;

        public SqlInfraException()
            : base() => this.logSentry = logValue;

        protected SqlInfraException(SerializationInfo info, StreamingContext context)
            : base(info, context) => this.logSentry = logValue;
    }
}
