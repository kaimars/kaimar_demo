using System;

namespace Bacchus.Core.Exceptions
{
    public class BusinessRuleViolatedException: ApplicationException
    {
        protected BusinessRuleViolatedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public BusinessRuleViolatedException(string message) : base(message)
        {
        }

        public BusinessRuleViolatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
    }
}
