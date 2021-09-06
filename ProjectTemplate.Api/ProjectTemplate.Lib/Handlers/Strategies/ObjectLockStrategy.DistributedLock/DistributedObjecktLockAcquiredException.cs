using System;
using System.Runtime.Serialization;

namespace ProjectTemplate.Lib
{
    [Serializable]
    public class DistributedObjecktLockAcquiredException : Exception
    {
        public DistributedObjecktLockAcquiredException()
        {
        }

        public DistributedObjecktLockAcquiredException(string message) : base(message)
        {
        }

        public DistributedObjecktLockAcquiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DistributedObjecktLockAcquiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}