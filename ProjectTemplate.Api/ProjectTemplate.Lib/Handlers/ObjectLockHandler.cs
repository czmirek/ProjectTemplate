using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Lib
{
    public abstract class ObjectLockHandler<TRequest, TResponse> : ObjectLockHandler
    {
        public abstract Task<TResponse> Handle(TRequest request);
    }

    public abstract class ObjectLockHandler<TRequest> : ObjectLockHandler
    {
        public abstract Task Handle(TRequest request);
    }

    public abstract class ObjectLockHandler
    {
        public abstract string LockCategory { get; }
        public Guid Id { get; set; }
    }
}