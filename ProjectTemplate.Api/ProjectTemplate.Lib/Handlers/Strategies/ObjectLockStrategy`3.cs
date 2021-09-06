using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Lib
{
    public class ObjectLockStrategy<THandler, TRequest, TResponse> : ObjectLockStrategy
        where THandler : ObjectLockHandler<TRequest, TResponse>
    {
        public async Task<TResponse> Invoke(Guid id, TRequest request)
        {
            using var scope = ServiceProvider.CreateScope();
            
            var lockHandler = (ObjectLockHandler<TRequest, TResponse>)scope.ServiceProvider.GetService(typeof(THandler));
            var dLock = scope.ServiceProvider.GetRequiredService<IDistributedObjectLock>();

            lockHandler.Id = id;
            string lockKey = $"${lockHandler.LockCategory}_{lockHandler.Id}";
            using (dLock.AcquireAsync(lockKey))
            {
                return await lockHandler.Handle(request);
            }
        }
    }
}